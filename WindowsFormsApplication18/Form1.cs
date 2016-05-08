using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication18
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int step = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            Moving.Enabled = true;
            step = 1;
        }

        private void Moving_Tick(object sender, EventArgs e)
        {
            if(step == 1 && OptionsPanel.Location.X < 10)
            {
                OptionsPanel.Location = new Point(OptionsPanel.Location.X + 10, OptionsPanel.Location.Y);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
