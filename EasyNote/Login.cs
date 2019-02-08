using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyNote
{
    public class Login : Form
    {
        private static Control parent = null;

        public Login(Control control)
        {
            parent = control as TabPage;
            parent.Controls.Add(loginSection);
        }

        private static Panel loginSection = new Panel()
        {
            Parent = parent,
            Location = new Point(0,0),
            BackColor = Color.Aqua,
            Visible = true,
            AutoSize = true,
            Size = new Size(parent.Size.Width, parent.Size.Height)
        };

        //private static Label label = new Label()
        //{
        //    Parent = loginSection,
        //    AutoSize = false,
        //    Dock = DockStyle.Top,
        //    TextAlign = ContentAlignment.MiddleCenter,
        //    Size = new Size(0, 40),
        //    Text = "PASSWORD",
        //    Font = new Font("ARIAL", 20) // ARIAL ??
        //};

        //private TextBox textbox = new TextBox()
        //{
        //    Parent = loginSection,
        //    Dock = DockStyle.Bottom,
        //    TextAlign = HorizontalAlignment.Center,
        //    UseSystemPasswordChar = true,
        //    //Font = new Font(Font.FontFamily, 14),
        //    //KeyDown += (s, e) => {
        //    //    if (e.KeyCode == Keys.Enter)
        //    //    {
        //    //        login(textbox.Text);
        //    //    }
        //    //};
        //};
        ////Login.textbox.KeyDown +=
        ////};
    };
}
