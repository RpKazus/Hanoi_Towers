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
        Dictionary<int, int> answers = new Dictionary<int,int>();
        public Form1()
        {
            InitializeComponent();
            answers.Add(2, 3);
            answers.Add(3, 7);
            answers.Add(4, 15);
            answers.Add(5, 31);
            answers.Add(6, 63);
            answers.Add(7, 127);
            answers.Add(8, 255);
        }
        PictureBox[] pictureList;
        int step = 0;
        int time;
        private void button1_Click(object sender, EventArgs e)
        {
            Moving.Enabled = true;
            step = 1;
            button1.Text = "Настройте игру и нажмите на Кнопку";
            button1.BackColor = Color.Silver;
            button1.FlatAppearance.MouseDownBackColor = Color.DarkGray;
            button1.FlatAppearance.MouseOverBackColor = Color.LightGray;
            button1.Click += Beginning;
            button1.Click -= button1_Click;
        }
        private void Beginning(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gold;
            step = 2;
            switch (domainUpDown1.Text)
            {
                case "10 сек":
                    time = 10;
                    break;
                case "20 сек":
                    time = 20;
                    break;
                case "30 сек":
                    time = 30;
                    break;
                case "1 мин":
                    time = 60;
                    break;
                case "1 мин 30 сек":
                    time = 90;
                    break;
                case "2 мин":
                    time = 120;
                    break;
                case "5 мин":
                    time = 300;
                    break;
            }
            timer.Enabled = true;
            button1.Click -= Beginning;
            button1.Click += Ending;
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave; 
        }
        private void Ending(object sender, EventArgs e)
        {
            if(MessageBox.Show("Сдаться", "Вы точно хотите сдаться?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                button1.MouseEnter -= button1_MouseEnter;
                button1.MouseLeave -= button1_MouseLeave;
                button1.Click -= Ending;
                button1.Click += button1_Click;
                time = 0;
                timer.Enabled = false;
                step = 228;
                this.button1.Text = "Н А Ч А Т Ь  И Г Р У ";
                this.button1.BackColor = System.Drawing.Color.Yellow;
                this.button1.FlatAppearance.MouseDownBackColor = Color.Yellow;
                this.button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 255, 128);
            }
        }
        public void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Text = " ";
            timer_Tick(timer, e);
        }

        void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Text = "С Д А Т Ь С Я";
        }
        private void Moving_Tick(object sender, EventArgs e)
        {
            if(step == 1 && OptionsPanel.Location.X < 10)
            {
                OptionsPanel.Location = new Point(OptionsPanel.Location.X + 10, OptionsPanel.Location.Y);
            }
            else if (step == 2 && OptionsPanel.Location.X > -320)
            {
                OptionsPanel.Location = new Point(OptionsPanel.Location.X - 10, OptionsPanel.Location.Y);
                panel1.Location = new Point(panel1.Location.X - 1, panel1.Location.Y);
            }
            else if(step == 228 && panel1.Location.X < 331)
            {
                panel1.Location = new Point(panel1.Location.X + 1, panel1.Location.Y);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureList = new PictureBox[8] {pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8};
        }

        private void domainUpDown1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            foreach(PictureBox pic in pictureList)
            {
                pic.Visible = false;
            }
            for (int i = 0; i < numericUpDown1.Value; i++ )
            {
                pictureList[i].Visible = true;
            }
            label4.Text = answers[(int)numericUpDown1.Value].ToString();
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                if(time >= 60 && button1.Text != "С Д А Т Ь С Я")
                    button1.Text = "Осталась " + (time / 60) + " мин " + (time % 60) + " сек";
                else if(button1.Text != "С Д А Т Ь С Я")
                    button1.Text = "Осталось " + time + " сек";
                time--;
                if (button1.BackColor == Color.Gold)
                    button1.BackColor = Color.Gray;
                else if (button1.BackColor == Color.Gray)
                    button1.BackColor = Color.Gold;

            }
        }
    }
}
