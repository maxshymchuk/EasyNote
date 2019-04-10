using EasyNote;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EasyNoteNS
{
  public partial class EasyNote : Form
  {
    private string notesPath = $"{Environment.ExpandEnvironmentVariables("%appdata%")}\\EasyNote";
    private int savePeriod = 3000;
    private string activeTabName = "";

    private const string FILE_EXT = ".txt";
    private const char UNSAVED_MARK = '*';
    private const char SAVED_MARK = ' ';

    private string[] fileList = null;
    private List<TabPage> tabList = new List<TabPage>();
    private List<Memo> memoList = new List<Memo>();

    public EasyNote()
    {
      InitializeComponent();
      //setEnvVar();
      if (!RegControls.Init())
      {
        RegControls.Set("NotesPath", notesPath);
        RegControls.Set("ActiveTabName", "");
        RegControls.Set("FormX", Location.X);
        RegControls.Set("FormY", Location.Y);
        RegControls.Set("FormWidth", Width);
        RegControls.Set("FormHeight", Height);
        RegControls.Set("SavePeriod", savePeriod);
      }
      else
      {
        activeTabName = RegControls.Get("ActiveTabName");
        notesPath = RegControls.Get("NotesPath");
        savePeriod = RegControls.Get("SavePeriod");
        Location = new Point(
          RegControls.Get("FormX"),
          RegControls.Get("FormY")
        );
        Size = new Size(
          RegControls.Get("FormWidth"),
          RegControls.Get("FormHeight")
        );
      }

      if (!Directory.Exists(notesPath))
      {
        Directory.CreateDirectory(notesPath);
      }
    }

    private void OnPromptResult(string tabName, ResultMode resultMode)
    {
      switch (resultMode)
      {
        case ResultMode.Confirm:
          File.CreateText($"{notesPath}\\{tabName}{FILE_EXT}").Dispose();
          CreateTab(tabName);
          break;
        case ResultMode.Cancel:
          if (tabList.Count == 1)
          {
            Close();
            break;
          }
          TabControl.SelectedTab = tabList.Find(t => extractFileName(t.Text) == activeTabName) ?? TabControl.TabPages[0];
          break;
      }
    }

    private void CreateTab(string tabName)
    {
      TabPage tab = null;
      if (TabControl.TabCount > 0)
      {
        tab = TabControl.TabPages[TabControl.TabCount - 1];
      }
      else
      {
        tab = new TabPage()
        {
          Parent = TabControl
        };
      }
      tab.Padding = new Padding(3);
      tab.BackColor = Color.White;
      tab.Text = $"{tabName}{SAVED_MARK}";

      tabList.Add(tab);

      TabPage newTab = new TabPage();
      newTab.Parent = TabControl;
      //newTab.BackColor = Color.Blue;
      newTab.Text = "+ new";

      TabControl.SelectedTab = tab;

      Memo memo = new Memo()
      {
        Parent = tab,
        Dock = DockStyle.Fill,
        BorderStyle = BorderStyle.None,
        Name = "memo"
      };
      memo.TextChanged += memo_TextChanged;
      memo.LoadFile($"{notesPath}\\{extractFileName(tabName)}{FILE_EXT}", RichTextBoxStreamType.UnicodePlainText);
      memo.Focus();

      memoList.Add(memo);
    }

    private void setEnvVar()
    {
      const string name = "Path";
      string location = Environment.CurrentDirectory;
      string pathvar = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User);
      if (!pathvar.Split(';').Contains(location))
      {
        string value = pathvar + $";{location}";
        Environment.SetEnvironmentVariable(name, value, EnvironmentVariableTarget.User);
      }
    }

    private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
    {
      if (TabControl.SelectedIndex == TabControl.TabCount - 1)
      {
        new Prompt("New tab name", new PromptResult(OnPromptResult));
      }
      else
      {
        RegControls.Set("ActiveTabName", extractFileName(TabControl.SelectedTab.Text));
      }
    }

    private void memo_TextChanged(object sender, EventArgs e)
    {
      Memo memo = (sender as Memo);
      if (memo.isSaved)
      {
        (memo.Parent as TabPage).Text = $"{extractFileName((memo.Parent as TabPage).Text)}{UNSAVED_MARK}";
      }
      memo.isSaved = false;
    }

    private string extractFileName(string s)
    {
      return s[s.Length - 1] == SAVED_MARK || s[s.Length - 1] == UNSAVED_MARK ? s.Substring(0, s.Length - 1) : s;
    }

    private void saveTimer_Tick(object sender, EventArgs e)
    {

      foreach (Memo memo in memoList)
      {
        if (!memo.isSaved)
        {
          string fileName = extractFileName(memo.Parent.Text);
          memo.SaveFile($"{notesPath}\\{fileName}{FILE_EXT}", RichTextBoxStreamType.UnicodePlainText);
          (memo.Parent as TabPage).Text = $"{fileName}{SAVED_MARK}";
          memo.isSaved = true;
        }
      }
      if (WindowState != FormWindowState.Minimized)
      {
        SaveSettings();
      }
    }

    private void SaveSettings()
    {
      //RegControls.Set("NotesPath", notesPath);
      //RegControls.Set("ActiveTabName", TabControl.SelectedTab.Text);
      RegControls.Set("FormX", Location.X);
      RegControls.Set("FormY", Location.Y);
      RegControls.Set("FormWidth", Width);
      RegControls.Set("FormHeight", Height);
      //RegControls.Set("SavePeriod", savePeriod);
    }

    private void tabControl1_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Middle)
      {
        TabControl tabControl = sender as TabControl;
        var tabs = tabControl.TabPages;
        TabPage tab = tabs.Cast<TabPage>()
          .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
          .First(); // clicked tab
        if (ModifierKeys == Keys.Shift)
        {
          File.Delete($"{notesPath}\\{extractFileName(tab.Text)}{FILE_EXT}");
        }
        tabs.Remove(tabs.Cast<TabPage>()
          .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
          .First());
        Memo memo = tab.Controls.Find("memo", false).FirstOrDefault() as Memo;
        memoList.Remove(memo);
        tabList.Remove(tab);
      }
    }

    private void EasyNote_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          break;
        case Keys.F2:
          break;
      }
    }

    private void EasyNote_Load(object sender, EventArgs e)
    {
      fileList = Directory.GetFiles(notesPath, $"*{FILE_EXT}");
      if (fileList == null)
      {
        new Prompt("Tab name", new PromptResult(OnPromptResult));
      }
      else
      {
        foreach (string filePath in fileList)
        {
          CreateTab(Path.GetFileNameWithoutExtension(filePath));
        }
      }

      TabControl.SelectedTab = tabList.Find(t => extractFileName(t.Text) == activeTabName) ?? TabControl.TabPages[0];
    }
  }
}
