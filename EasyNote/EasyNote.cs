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

    private const string DEFAULT_NAME = "Note";
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
        RegControls.Set("ActiveTabName", DEFAULT_NAME);
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
        File.CreateText($"{notesPath}\\{DEFAULT_NAME}{FILE_EXT}").Dispose();
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
          if (TabControl.TabCount == 1)
          {
            Close();
            break;
          }
          TabControl.SelectedTab = tabList.Find(t => ExtractFileName(t.Text) == activeTabName) ?? TabControl.TabPages[0];
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
        AcceptsTab = true,
        Parent = tab,
        Dock = DockStyle.Fill,
        BorderStyle = BorderStyle.None,
        Name = "memo"
      };
      memo.TextChanged += Memo_TextChanged;
      memo.LoadFile($"{notesPath}\\{ExtractFileName(tabName)}{FILE_EXT}", RichTextBoxStreamType.UnicodePlainText);
      memo.Focus();

      memoList.Add(memo);
    }

    private void SetEnvVar()
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

    private void TabControl1_Selecting(object sender, TabControlCancelEventArgs e)
    {
      if (TabControl.SelectedIndex == TabControl.TabCount - 1)
      {
        new Prompt("New tab name", tabList.Select(tab => tab.Text.Substring(0, tab.Text.Length - 1)).ToArray(), new PromptResult(OnPromptResult));
      }
      else
      {
        RegControls.Set("ActiveTabName", ExtractFileName(TabControl.SelectedTab.Text));
      }
    }

    private void Memo_TextChanged(object sender, EventArgs e)
    {
      Memo memo = (sender as Memo);
      if (memo.isSaved)
      {
        (memo.Parent as TabPage).Text = $"{ExtractFileName((memo.Parent as TabPage).Text)}{UNSAVED_MARK}";
      }
      memo.isSaved = false;
    }

    private string ExtractFileName(string s)
    {
      int c = s.Length - 1;
      return s[c] == SAVED_MARK || s[c] == UNSAVED_MARK ? s.Substring(0, c) : s;
    }

    private void SaveTimer_Tick(object sender, EventArgs e)
    {

      foreach (Memo memo in memoList)
      {
        if (!memo.isSaved)
        {
          string fileName = ExtractFileName(memo.Parent.Text);
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

    private void TabControl1_MouseClick(object sender, MouseEventArgs e)
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
          File.Delete($"{notesPath}\\{ExtractFileName(tab.Text)}{FILE_EXT}");
        }
        tabs.Remove(tabs.Cast<TabPage>()
          .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
          .First());
        Memo memo = tab.Controls.Find("memo", false).FirstOrDefault() as Memo;
        memoList.Remove(memo);
        tabList.Remove(tab);
      }
    }

    private void EasyNote_Load(object sender, EventArgs e)
    {
      fileList = Directory.GetFiles(notesPath, $"*{FILE_EXT}");
        if (fileList == null)
        {
            new Prompt("Tab name", tabList.Select(tab => tab.Text.Substring(0, tab.Text.Length - 1)).ToArray(), new PromptResult(OnPromptResult));
        }
        else
        {
        foreach (string filePath in fileList)
        {
          CreateTab(Path.GetFileNameWithoutExtension(filePath));
        }
      }

      TabControl.SelectedTab = tabList.Find(t => ExtractFileName(t.Text) == activeTabName) ?? TabControl.TabPages[0];
    }
  }
}
