using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MancalaGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
        }

        private void instructions_Click(object sender, EventArgs e)
        {
            Form2 rulesForm = new Form2();
            rulesForm.Location = new Point(this.Location.X, this.Location.Y);
            rulesForm.Show();
        }

        private void quit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void play_Click(object sender, EventArgs e)
        {
            Form3 playForm = new Form3();
            playForm.RefToMainForm = this;
            playForm.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
            playForm.Show();
        }
    }
}
