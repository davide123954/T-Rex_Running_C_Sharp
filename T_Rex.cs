using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Endless_Runner_MOO_ICT
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        int jumpSpeed;
        int score = 0;
        int force = 12;
        int obstacleSpeed = 10;
        Random rnd = new Random();
        int position;
        bool isGameover = false;
        public Form1()
        {
            InitializeComponent();
            GameReset();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            trex.Top += jumpSpeed;
            txtScore.Text = "Score: " + score;
            if(jumping == true && force < 0)
            {
                jumping = false;
            }
            if(jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }
            if (trex.Top > 292 && jumping == false)
            {
                force = 12;
                trex.Top = 293;
                jumpSpeed = 0;
            }

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;
                    if (x.Left < -100)// 50
                    {
                        x.Left = this.ClientSize.Width + rnd.Next(200, 500) + (x.Width * 15);
                        score++;
                    }
                    if (trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        txtScore.Text += "Press R to restart the game!";
                        isGameover = true;
                    }
                }
            }
            if(score > 5)
            {
                obstacleSpeed = 15;
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Space && jumping== false)
            {
                jumping = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if(jumping == true)
            {
                jumping = false;
            }
            if (e.KeyCode== Keys.R && isGameover == true)
            {
                GameReset();
            }
        }
        private void GameReset()
        {
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            txtScore.Text = "Score: " + score;
            trex.Image = Properties.Resources.running;
            isGameover = false;
            trex.Top = 293;// לשחק עם מידות אחרת לא תראה אותו 


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {                                                 //500 800
                    position = this.ClientSize.Width + rnd.Next(50, 100) + (x.Width * 10);
                    x.Left = position;
                }
            }
            gameTimer.Start();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
