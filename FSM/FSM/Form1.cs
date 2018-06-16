using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSM
{
    public partial class Form1 : Form
    {
        Brain brain = new Brain();
        public Form1()
        {
            InitializeComponent();
            brain.invoker = ShowInfo;
            display.Text = 0.ToString();
        }
        public void ShowInfo(string msg)
        {
            display.Text = msg;
        }
        public void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            char info = btn.Text[0];
            brain.Process(btn.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
