namespace EasyNoteNS
{
  partial class EasyNote
  {
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasyNote));
      this.TabControl = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.saveTimer = new System.Windows.Forms.Timer(this.components);
      this.menu = new System.Windows.Forms.Panel();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.TabControl.SuspendLayout();
      this.menu.SuspendLayout();
      this.SuspendLayout();
      // 
      // TabControl
      // 
      this.TabControl.Controls.Add(this.tabPage1);
      this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TabControl.Location = new System.Drawing.Point(0, 0);
      this.TabControl.Margin = new System.Windows.Forms.Padding(0);
      this.TabControl.Multiline = true;
      this.TabControl.Name = "TabControl";
      this.TabControl.Padding = new System.Drawing.Point(0, 0);
      this.TabControl.SelectedIndex = 0;
      this.TabControl.Size = new System.Drawing.Size(299, 273);
      this.TabControl.TabIndex = 0;
      this.TabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
      this.TabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
      // 
      // tabPage1
      // 
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(291, 247);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "tabPage1";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // saveTimer
      // 
      this.saveTimer.Enabled = true;
      this.saveTimer.Interval = 3000;
      this.saveTimer.Tick += new System.EventHandler(this.saveTimer_Tick);
      // 
      // menu
      // 
      this.menu.Controls.Add(this.button2);
      this.menu.Controls.Add(this.button1);
      this.menu.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.menu.Location = new System.Drawing.Point(0, 273);
      this.menu.Name = "menu";
      this.menu.Size = new System.Drawing.Size(299, 30);
      this.menu.TabIndex = 1;
      this.menu.Visible = false;
      // 
      // button2
      // 
      this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.button2.Dock = System.Windows.Forms.DockStyle.Right;
      this.button2.Location = new System.Drawing.Point(239, 0);
      this.button2.Margin = new System.Windows.Forms.Padding(0);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(30, 30);
      this.button2.TabIndex = 1;
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.button1.Dock = System.Windows.Forms.DockStyle.Right;
      this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
      this.button1.Location = new System.Drawing.Point(269, 0);
      this.button1.Margin = new System.Windows.Forms.Padding(0);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(30, 30);
      this.button1.TabIndex = 0;
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // EasyNote
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(299, 303);
      this.Controls.Add(this.TabControl);
      this.Controls.Add(this.menu);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Name = "EasyNote";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "EasyNote";
      this.Load += new System.EventHandler(this.EasyNote_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EasyNote_KeyDown);
      this.TabControl.ResumeLayout(false);
      this.menu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl TabControl;
    private System.Windows.Forms.Timer saveTimer;
    private System.Windows.Forms.Panel menu;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.TabPage tabPage1;
  }
}

