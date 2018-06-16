
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid
{
    public partial class Form1 : Form
    { 
        Graphics g;
        public Point[] asteroids;
        public Point[] spaceship;
        public Point[] bullet;
        public Point[] gun;
        public Point[] ofStars;

        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
            asteroids = new Point[12];
            spaceship = new Point[6];
            gun = new Point[7];
            bullet = new Point[8];
            ofStars = new Point[8];
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g.FillRectangle(Brushes.MidnightBlue, new Rectangle(0, 0, Width, Height));
            g.DrawRectangle(new Pen(Color.Black, 5), new Rectangle(0, 0, Width, Height));
            g.FillRectangle(Brushes.White, new Rectangle(480, 20, 120, 20));
            g.DrawRectangle(new Pen(Color.Yellow, 3), new Rectangle(480, 20, 120, 20));
            g.DrawString("level:1 Score:200 Live:***", new Font(FontFamily.GenericSansSerif, 7), Brushes.Black, 485, 22);


            DrawStar(40, 40);
            ofStars[0] = new Point(40, 40);
            DrawStar(50, 280);
            ofStars[1] = new Point(50, 280);
            DrawStar(300, 30);
            ofStars[2] = new Point(300, 30);
            DrawStar(300, 270);
            ofStars[3] = new Point(300, 270);
            DrawStar(450, 90);
            ofStars[4] = new Point(450, 90);
            DrawStar(530, 230);
            ofStars[5] = new Point(530, 230);
            DrawStar(600, 160);
            ofStars[6] = new Point(600, 160);
            DrawStar(600, 300);
            ofStars[7] = new Point(600, 300);

            DrawSpaceship(360, 140);

            DrawAsteroid(80, 80);
            DrawAsteroid(150, 200);
            DrawAsteroid(400, 250);
            DrawAsteroid(500, 60);

            DrawGun(384, 140);

            DrawBullet(384, 110);
        }

        private void DrawAsteroid(int x, int y)
        {
            asteroids[0] = new Point(x, y);
            asteroids[1] = new Point(x + 8, y);
            asteroids[2] = new Point(x + 12, y - 6);
            asteroids[3] = new Point(x + 16, y);
            asteroids[4] = new Point(x + 24, y);
            asteroids[5] = new Point(x + 18, y + 6);
            asteroids[6] = new Point(x + 24, y + 12);
            asteroids[7] = new Point(x + 16, y + 12);
            asteroids[8] = new Point(x + 12, y + 18);
            asteroids[9] = new Point(x + 8, y + 12);
            asteroids[10] = new Point(x, y + 12);
            asteroids[11] = new Point(x + 6, y + 6);
            g.FillPolygon(Brushes.Red, asteroids);
        }

        private void DrawStar(int x, int y)
        {
            Rectangle r = new Rectangle(x, y, 20, 20);
            g.FillEllipse(Brushes.White, r);
        }

        private void DrawBullet(int x, int y)
        {
            //384,135 
            bullet[0] = new Point(x, y);
            bullet[1] = new Point(x + 6, y - 2);
            bullet[2] = new Point(x + 8, y - 8);
            bullet[3] = new Point(x + 10, y - 2);
            bullet[4] = new Point(x + 16, y);
            bullet[5] = new Point(x + 10, y + 2);
            bullet[6] = new Point(x + 8, y + 8);
            bullet[7] = new Point(x + 6, y + 2);

            g.FillPolygon(Brushes.Green, bullet);
        }

        private void DrawSpaceship(int x, int y)
        {
            //380.120 
            spaceship[0] = new Point(x, y);
            spaceship[1] = new Point(x + 24, y - 8);
            spaceship[2] = new Point(x + 48, y);
            spaceship[3] = new Point(x + 48, y + 24);
            spaceship[4] = new Point(x + 24, y + 32);
            spaceship[5] = new Point(x, y + 24); 

            g.FillPolygon(Brushes.Yellow, spaceship);
        }

        private void DrawGun(int x, int y)
        {//360,140 
            gun[0] = new Point(x, y);
            gun[1] = new Point(x + 6, y + 10);
            gun[2] = new Point(x + 3, y + 10);
            gun[3] = new Point(x + 3, y + 22);
            gun[4] = new Point(x - 3, y + 22);
            gun[5] = new Point(x - 2, y + 10);
            gun[6] = new Point(x - 6, y + 10);
            g.FillPolygon(Brushes.Green, gun);
        }
    }
 
}



 
