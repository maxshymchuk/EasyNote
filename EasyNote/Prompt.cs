using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public enum ResultMode
{
  Confirm, Cancel
}

namespace EasyNoteNS
{
  public partial class Prompt : Form
  {
    private ResultMode resultMode = ResultMode.Cancel;
    private PromptResult _delegate;
    public Prompt(string title, PromptResult sender)
    {
      InitializeComponent();
      _delegate = sender;

      Text = title;
      ClientSize = new Size(260, 50);
      TextBox.AutoSize = false;
      TextBox.Size = new Size(150, 30);
      TextBox.Location = new Point(10, 10);
      TextBox.Font = new Font(TextBox.Font.FontFamily, 14);
      ConfirmButton.Size = new Size(80, 30);
      ConfirmButton.Location = new Point(170, 10);
      ConfirmButton.Click += new EventHandler(GetNewTabName);
      ShowDialog();
    }

    private void GetNewTabName(object sender, EventArgs e)
    {
      if (TextBox.Text != "")
      {
        resultMode = ResultMode.Confirm;
        Close();
      }
    }

    private void Prompt_FormClosed(object sender, FormClosedEventArgs e)
    {
      _delegate(TextBox.Text, resultMode);
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
      if (Regex.Match(TextBox.Text, @"\p{IsCyrillic}|\p{IsCyrillicSupplement}").Success)
      {
        ToolTip tt = new ToolTip();
        tt.ToolTipTitle = "Warning";
        tt.Show("Only english letters allowed", TextBox, 0, TextBox.Size.Height, 1000);
        ConfirmButton.Enabled = false;
      }
      else
      {
        ConfirmButton.Enabled = true;
      }
    }
  }
}
