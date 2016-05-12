using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Balls
{
    public partial class Form1 : Form
    {
        private List<Ball> balls;
        private List<Brush> colors;
        private int ballsCount; // к-сть кульок

        public Form1()
        {
            InitializeComponent();
            ballsCount = 5;
            colors = new List<Brush>();
            colors.Add(Brushes.Green);
            colors.Add(Brushes.Red);
            colors.Add(Brushes.Yellow);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            balls = new List<Ball>();
            
            this.Bounds = Screen.PrimaryScreen.Bounds;
            
            var rand = new Random();

            for (int i = 0; i < ballsCount; i++)
            {
                var diameter = rand.Next(30, 100);
                var x = rand.Next(0, this.Width - diameter);
                var y = rand.Next(0, this.Height - diameter);
                var mass = rand.Next(2, 4);
                var velocityX = rand.Next(-4, 4);
                var velocityY = rand.Next(-4, 4);
                var ball = new Ball(new Vector(x, y), diameter, mass, new Vector(velocityX, velocityY), colors[rand.Next(0, colors.Count)]);
                balls.Add(ball);
            }
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            // Do nothing. // Для зменншення мигання
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (var ball in balls)
            {
                ball.CollideWall();
                ball.Move();
                ball.Draw(e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (var i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    balls[i].CheckCollision(balls[j]);
                }
            }
            this.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
