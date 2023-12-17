using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_rex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BasicSettings();
            ResetGame();
            AddObstacles();
            MainGameTimer.Start();
        }

        private Character Player =  new Character();
        private List<Obstacle> obstacles = new List<Obstacle>();


        private void BasicSettings()
        {
            this.Size = new Size(1000, 500);
            this.Text = "T-rex Runner";
            MaximizeBox = false;
            picturebox.Location = new Point(0, 0);
            picturebox.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
        }

       

      
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //MainGameTimer.Stop();
                ResetGame();
                AddObstacles();
                MainGameTimer.Start();
            }

            /*if (e.KeyCode == Keys.Space && Player.OnGround())
            {
                Player.Jump = true;
            }*/

        }

        

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Player.Jump = false;
            }
        }

        private void MainDrawingEvent(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics ;
            Brush brush = new SolidBrush(Color.Black);
            graphics.FillRectangle(brush, 0, this.ClientSize.Height - 50, this.ClientSize.Width, 50); //the ground

            for(int i=0;  i<obstacles.Count(); i++)
            {
                graphics.DrawImage(obstacles[i].Appearance, new PointF(obstacles[i].LocationX, this.ClientSize.Height - 50 - obstacles[i].Size.Height));
            }
            graphics.DrawImage(Player.Appearance, new Point(350, Player.LocationY));
            graphics.DrawString("CHRISTIAN BARTE", new Font("Courier New", 14), brush, new Point(400, 50));
            graphics.DrawString("\n    TEA-REX", new Font("Courier New", 14), brush, new Point(400, 50));
           
            graphics.DrawString("Score: " + Player.Score, new Font("Courier New", 14), brush, new Point(100,125) );

        }

        private void MainGameLoop(object sender, EventArgs e)
        {
            for(int i = 0 ; i < obstacles.Count(); i++)
            {
                obstacles[i].LocationX -= Player.Speed; //moves obstacle towards player
                if (obstacles[i].LocationX < -100)      //if obstacle disappears, it will be replaced with a new one
                {
                    obstacles.Remove(obstacles[i]);
                    Player.Score += 1;
                    AddObstacles();
                }
            }
            Player.Update(obstacles);
            if (Player.GameOver)
            {
                MainGameTimer.Stop();
            }
            picturebox.Invalidate();
        }

        private void AddObstacles()
        {
            int a = 3 - obstacles.Count () - 1;
            Random random = new Random();
            for(int i = 0 ;i < a;i++)
            {
                int n = random.Next (50,100);
                Obstacle obstacle = new Obstacle(n);
                obstacle.LocationX = random.Next(1100, 2000);
                while(obstacles.Where(s => s.LocationX > obstacle.LocationX && s.LocationX < obstacle.LocationX + obstacle.Size.Width).Count() > 0|| obstacles.Where(s => s.LocationX < obstacle.LocationX && s.LocationX + Size.Width > obstacle.LocationX).Count() > 0)
                {
                    obstacle.LocationX = random.Next(1100, 3000);
                }
                obstacles.Add(obstacle);
            }

        }
        private void ResetGame()
        {
            obstacles.Clear();
            Player = new Character(5, 24, 10);
        }
    }
}
