using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication18
{
    public partial class Form1 : Form
    {
        int DvigPic = -1;
        Control lastParent;
        Point pOffset = new Point(-1, -1);
        Dictionary<int, int> answers = new Dictionary<int,int>();
        Dictionary<int, int> Nologics = new Dictionary<int, int>();
        Dictionary<int, int> Yeslogics = new Dictionary<int, int>();
        Dictionary<Control, int> Towers = new Dictionary<Control, int>();
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
            Nologics.Add(1, 2);
            Nologics.Add(2, 3);
            Nologics.Add(3, 1);
            Yeslogics.Add(1, 3);
            Yeslogics.Add(2, 1);
            Yeslogics.Add(3, 2);
            Towers.Add(st1, 1);
            Towers.Add(st2, 2);
            Towers.Add(st3, 3);
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
        private void Move(int pic)
        {
            DvigPic = pic;
        }
        private void SetPir(int count)
        {
            if (count == 1)
                Move((int)numericUpDown1.Value - count);
            else
            {
                SetPir(count - 1);
                Move((int)numericUpDown1.Value - count);
                SetPir(count - 1);
            }
            this.Update();
        }
        private void SomeButton_MouseDown(object sender, EventArgs e)
        {

        }
        private void Beginning(object sender, EventArgs e)
        {
            button1.FlatAppearance.MouseOverBackColor = Color.LightGray;
            label15.Text = "Ваше число ходов:";
            label14.Text = "0";
            panel1.Enabled = true;
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
                numericUpDown1_ValueChanged(sender, e);
                SetPir((int)numericUpDown1.Value);
                
            }
        }
        private void EndingEnd(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
                label15.Text = "Макс. число ходов:";
                label14.Text = "255";
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
                panel1.Enabled = false;
                numericUpDown1_ValueChanged(sender, e);
        }
        public void Ending()
        {
            System.Threading.Thread.Sleep(1000);
            label15.Text = "Макс. число ходов:";
            label14.Text = "255";
            button1.Click += button1_Click;
            time = 0;
            step = 228;
            this.button1.Text = "Н А Ч А Т Ь  И Г Р У ";
            this.button1.BackColor = System.Drawing.Color.Yellow;
            this.button1.FlatAppearance.MouseDownBackColor = Color.Yellow;
            this.button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 255, 128);
            panel1.Enabled = false;
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
                if(panel1.Location.X % 2 == 0)
                panel2.Location = new Point(panel2.Location.X - 1, panel2.Location.Y);
            }
            else if(step == 228 && panel1.Location.X < 331)
            {
                panel1.Location = new Point(panel1.Location.X + 1, panel1.Location.Y);
                if (panel1.Location.X % 2 == 0)
                    panel2.Location = new Point(panel2.Location.X + 1, panel2.Location.Y);
            }
            if (DvigPic != -1)
            {
                do
                {
                    pictureList[DvigPic].Location = new Point(pictureList[DvigPic].Location.X, pictureList[DvigPic].Location.Y - 1);
                    this.Update();
                    System.Threading.Thread.Sleep(1);
                }
                while (pictureList[DvigPic].Location.Y > -40);
                pictureList[DvigPic].Location = (DvigPic % 2 == 0) ? new Point(Yeslogics[Towers[pictureList[DvigPic].Parent]] * 350, pictureList[DvigPic].Location.Y) : new Point(Nologics[Towers[pictureList[DvigPic].Parent]] * 350, pictureList[DvigPic].Location.Y);
                Set(pictureList[DvigPic]);
                int height = pictureList[DvigPic].Location.Y;
                pictureList[DvigPic].Location = new Point(pictureList[DvigPic].Location.X, -40);
                do
                {
                    pictureList[DvigPic].Location = new Point(pictureList[DvigPic].Location.X, pictureList[DvigPic].Location.Y + 1);
                    this.Update();
                    System.Threading.Thread.Sleep(1);
                }
                while (pictureList[DvigPic].Location.Y < height);
                DvigPic = -1;
                EndingEnd(sender, e);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureList = new PictureBox[8] {pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8};
            foreach (PictureBox pic in pictureList)
                Set(pic);
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            foreach(PictureBox pic in pictureList)
            {
                pic.Visible = false;
                pic.Parent = this;
                pic.Enabled = false;
            }
            for (int i = 0; i < numericUpDown1.Value; i++ )
            {
                pictureList[i].Parent = st1;
                pictureList[i].Visible = true;
            }
            label4.Text = answers[(int)numericUpDown1.Value].ToString();
            pictureList[(int)numericUpDown1.Value - 1].Enabled = true;
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
            else
            {
                foreach (PictureBox pic in pictureList)
                    if (pic.Tag == "checked")
                    {
                        pic.Tag = "";
                        Set(pic);
                        pOffset = new Point(-1, -1);
                    }
                timer.Enabled = false;
                button1.MouseEnter -= button1_MouseEnter;
                button1.MouseLeave -= button1_MouseLeave;
                button1.Click -= Ending;
                button1.BackColor = Color.Red;
                button1.FlatAppearance.MouseDownBackColor = Color.Red;
                button1.FlatAppearance.MouseOverBackColor = Color.Red;
                button1.Text = "Вы поиграли - Повторить?";
                label15.Text = "Ваши очки:";
                label14.Text = "0";
                if (MessageBox.Show("Повторить", "Хотите ли вы повторить игру?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    Beginning(sender, e);
                else
                    Ending();
                numericUpDown1_ValueChanged(sender, e);
            }
        }
        public void Set(PictureBox pic)
        {
            label14.Text = (Convert.ToInt32(label14.Text) + 1).ToString();
            //if (pic.Parent == this)
                if (pic.Location.X < 634 && st1.Controls[st1.Controls.Count - 1].Width >= pic.Width)
                    pic.Parent = st1;
                else if (pic.Location.X >= 634 && pic.Location.X < 938 && st2.Controls[st2.Controls.Count - 1].Width >= pic.Width)
                    pic.Parent = st2;
                else if (pic.Location.X >= 938 && st3.Controls[st3.Controls.Count - 1].Width >= pic.Width)
                    pic.Parent = st3;
                else
                {
                    label14.Text = (Convert.ToInt32(label14.Text) - 1).ToString();
                    pic.Parent = lastParent;
                }
            pic.Location = new Point(159 - (pic.Width / 2), pic.Parent.Height - (pic.Parent.Controls.Count * pic.Height) - 214);
            foreach (Control pcb in pic.Parent.Controls)
                pcb.Enabled = false;
            pic.Enabled = true;
        }
        private void SomeButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (pOffset.X != -1)
                MessageBox.Show("");
            foreach( PictureBox pcb in pictureList)
                if(pcb == sender )
                {
                    lastParent = pcb.Parent;
                    pcb.Tag = "checked";
                    pcb.Location = new Point(pcb.Location.X + pcb.Parent.Location.X + panel1.Location.X, pcb.Location.Y + pcb.Parent.Location.Y + panel1.Location.Y);
                    pcb.Parent.Controls[pcb.Parent.Controls.Count - 2].Enabled = true;
                    pcb.Parent = this;
                    Point temp = MousePosition;
                    pOffset = new Point(temp.X - this.Location.X - pcb.Location.X - 9, temp.Y - this.Location.Y - pcb.Location.Y - 9);
                    pcb.BringToFront();
                    SomeButton_MouseMove(sender, e);
                }
        }
        private void SomeButton_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (PictureBox pcb in pictureList)
            {
                if (pcb == sender && pcb.Tag == "checked")
                    pcb.Location = new Point(MousePosition.X - pOffset.X, MousePosition.Y - pOffset.Y);
            }
            label9.Text = pOffset.ToString();
        }
        private void SomeButton_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (PictureBox pcb in pictureList)
                if (pcb == sender)
                {
                    pcb.Tag = "";
                    pOffset = new Point(-1, -1);
                    //Cursor.Position = pcb.Location;
                    //pcb.Location = new Point(1, 1);
                    Set(pcb);
                }
            if (st3.Controls.Count - 1 >= (int)numericUpDown1.Value && Convert.ToInt32(label14.Text) <= answers[(int)numericUpDown1.Value])
            {
                timer.Enabled = false;
                button1.MouseEnter -= button1_MouseEnter;
                button1.MouseLeave -= button1_MouseLeave;
                button1.Click -= Ending;
                button1.BackColor = Color.Lime;
                button1.FlatAppearance.MouseDownBackColor = Color.Lime;
                button1.FlatAppearance.MouseOverBackColor = Color.Lime;
                button1.Text = "Вы победили - Повторить?";
                label15.Text = "Ваши очки:";
                label14.Text = (time * (int)numericUpDown1.Value + 10).ToString();
                if (MessageBox.Show("Повторить", "Хотите ли вы повторить игру?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    Beginning(sender, e);
                else
                    Ending();
                numericUpDown1_ValueChanged(sender, e);
            }
            else if (st3.Controls.Count - 1 >= (int)numericUpDown1.Value && Convert.ToInt32(label14.Text) > answers[(int)numericUpDown1.Value])
            {
                timer.Enabled = false;
                button1.MouseEnter -= button1_MouseEnter;
                button1.MouseLeave -= button1_MouseLeave;
                button1.Click -= Ending;
                button1.BackColor = Color.Gold;
                button1.FlatAppearance.MouseDownBackColor = Color.Gold;
                button1.FlatAppearance.MouseOverBackColor = Color.Gold;
                button1.Text = "Вы победили, но превысили лимит ходов - Повторить?";
                label15.Text = "Ваши очки:";
                label14.Text = ((time * (int)numericUpDown1.Value + 10) / 2).ToString();
                if (MessageBox.Show("Повторить", "Хотите ли вы повторить игру?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    Beginning(sender, e);
                else
                    Ending();
                numericUpDown1_ValueChanged(sender, e);
            }
        }
    }
}
