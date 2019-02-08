using System;
using System.Drawing;
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
  }
}
