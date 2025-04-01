using System;
using System.Windows.Forms;

namespace StickyNotesApp
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button1.Enabled = checkBox1.Checked;
                button1.Text = "Check box Checked";
            }
            else
            {
                button1.Text = "Check box unChecked";
                button1.Enabled = false;

            }
        }
    }
}
