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

    private string[] fileList = null;
    private List<TabPage> tabList = new List<TabPage>();
    private List<RichTextBox> memoList = new List<RichTextBox>();

    private Panel loginForm = null;

    public EasyNote()
    {
      InitializeComponent();
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

      //if (!Directory.Exists(notesPath)) Directory.CreateDirectory(notesPath);
      //Login login = new Login(tabControl1.TabPages[0]);
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
          TabControl.SelectedTab = TabControl.TabPages[TabControl.TabCount - 2];
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
      tab.Text = tabName;

      tabList.Add(tab);

      TabPage newTab = new TabPage();
      newTab.Parent = TabControl;
      newTab.Text = "+ new";

      TabControl.SelectedTab = tab;

      RichTextBox memo = new RichTextBox()
      {
        Parent = tab,
        Dock = DockStyle.Fill,
        BorderStyle = BorderStyle.None,
        Name = "memo"
      };
      memo.KeyDown += memo_KeyDown;
      memo.LoadFile($"{notesPath}\\{tabName}{FILE_EXT}", RichTextBoxStreamType.UnicodePlainText);
      memo.Focus();

      memoList.Add(memo);
    }

    private void createLoginForm()
    {

    }

    private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
    {
      if (TabControl.SelectedIndex == TabControl.TabCount - 1)
      {
        new Prompt("New tab name", new PromptResult(OnPromptResult));
        Text = "EasyNote *";
      }
    }

    private void memo_KeyDown(object sender, KeyEventArgs e)
    {
      Text = "EasyNote *";
    }

    private void saveTimer_Tick(object sender, EventArgs e)
    {
      foreach (RichTextBox memo in memoList)
      {
         memo.SaveFile($"{notesPath}\\{memo.Parent.Text}{FILE_EXT}", RichTextBoxStreamType.UnicodePlainText);
      }
      if (WindowState != FormWindowState.Minimized)
      {
        SaveSettings();
      }

      Text = "EasyNote";
    }

    private void SaveSettings()
    {
      RegControls.Set("NotesPath", notesPath);
      RegControls.Set("ActiveTabName", TabControl.SelectedTab.Text);
      RegControls.Set("FormX", Location.X);
      RegControls.Set("FormY", Location.Y);
      RegControls.Set("FormWidth", Width);
      RegControls.Set("FormHeight", Height);
      RegControls.Set("SavePeriod", savePeriod);
    }

    private void tabControl1_MouseClick(object sender, MouseEventArgs e)
    {
      TabControl tabControl = sender as TabControl;
      var tabs = tabControl.TabPages;
      TabPage tab = tabs.Cast<TabPage>()
        .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
        .First(); // clicked tab

      RichTextBox memo = tab.Controls.Find("memo", false).FirstOrDefault() as RichTextBox;
      memoList.Remove(memo);

      if (e.Button == MouseButtons.Middle && ModifierKeys == Keys.Shift)
      {
        File.Delete($"{notesPath}\\{tab.Text}{FILE_EXT}");
      }

      if (e.Button == MouseButtons.Middle)
      {
        tabs.Remove(tabs.Cast<TabPage>()
            .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
            .First());
      }

      tabList.Remove(tab);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      //Settings settings = new Settings(new EasyNote());
    }

    private void EasyNote_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F1)
      {
        menu.Visible = !menu.Visible;
      }
    }

    private void login(string password)
    {
      bool flag = false;
      if (password == "123")
      {
        flag = true;
        FormBorderStyle = FormBorderStyle.Sizable;

      }
      TabControl.Visible = flag;
      menu.Visible = flag;
      KeyPreview = flag;
      loginForm.Visible = !flag;
      loginForm.Location = new Point((ClientSize.Width - loginForm.Width) / 2, (ClientSize.Height - loginForm.Height) / 2);
      FormBorderStyle = FormBorderStyle.FixedSingle;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      login("");
    }

    private void EasyNote_Load(object sender, EventArgs e)
    {
      fileList = Directory.GetFiles(notesPath, $"*{FILE_EXT}");
      if (fileList == null || fileList.Length == 0)
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

      TabControl.SelectedTab = tabList.Find(t => t.Text == activeTabName) ?? TabControl.TabPages[0];
    }
  }
}
