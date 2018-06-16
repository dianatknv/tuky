using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class Form1 : Form
    {
        GameLogic gl = new GameLogic();
        public Form1()
        {
            InitializeComponent();

            this.Controls.Add(gl.p1);
            this.Controls.Add(gl.p2);
            gl.p1.Enabled = true;
            gl.p2.Enabled = false;
        }

        /* public void GameOver()
         {
             if (gl.p1.brain.k == 0)
             {
                 MessageBox.Show("Game over!");
             }

             if(gl.p2.brain.k == 0)
             {
                 MessageBox.Show("You won!");
             }
         }*/


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}