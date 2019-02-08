namespace EasyNoteNS
{
  partial class Prompt
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.TextBox = new System.Windows.Forms.TextBox();
      this.ConfirmButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // TextBox
      // 
      this.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
      this.TextBox.Location = new System.Drawing.Point(10, 10);
      this.TextBox.Margin = new System.Windows.Forms.Padding(0);
      this.TextBox.Name = "TextBox";
      this.TextBox.Size = new System.Drawing.Size(150, 20);
      this.TextBox.TabIndex = 0;
      // 
      // ConfirmButton
      // 
      this.ConfirmButton.Location = new System.Drawing.Point(170, 10);
      this.ConfirmButton.Margin = new System.Windows.Forms.Padding(0);
      this.ConfirmButton.Name = "ConfirmButton";
      this.ConfirmButton.Size = new System.Drawing.Size(60, 20);
      this.ConfirmButton.TabIndex = 1;
      this.ConfirmButton.Text = "OK";
      this.ConfirmButton.UseVisualStyleBackColor = true;
      // 
      // Prompt
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(241, 40);
      this.Controls.Add(this.ConfirmButton);
      this.Controls.Add(this.TextBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Prompt";
      this.Padding = new System.Windows.Forms.Padding(10);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Prompt";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Prompt_FormClosed);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox TextBox;
    private System.Windows.Forms.Button ConfirmButton;
  }
}