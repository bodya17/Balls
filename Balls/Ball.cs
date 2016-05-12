using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace Balls
{
    class Ball
    {
        public Vector position; // x,y - координати лівого верхнього кута
        public Vector center;
        public Vector velocity;
        public int diameter;
        public double mass;
        public Brush brush;
        
        public Ball(Vector position, int diameter, double mass, Vector velocity, Brush brush)
        {
            this.position = position;
            this.center = position + new Vector(diameter / 2, diameter / 2);
            this.diameter = diameter;
            this.mass = mass;
            this.velocity = velocity;
            this.brush = brush;
        }
 
        public void Draw(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(brush, Convert.ToInt32(position.X), Convert.ToInt32(position.Y), diameter, diameter);
            e.Graphics.DrawString(
                mass.ToString() + "kg", 
                new Font("Arial", diameter / 3, FontStyle.Bold),
                Brushes.Black, 
                Convert.ToInt32(position.X + diameter/10), 
                Convert.ToInt32(position.Y + diameter / 3));
        }

        public void Move()
        {
            this.position += velocity;
            this.center += velocity;
        }

        // Перевірити зіткненя зі стіною
        public void CollideWall()
        {
            if ((this.position.X ) <= 0)
                this.velocity.X *= -1;

            if (this.position.Y <= 0)
                this.velocity.Y *= -1;

            if (this.position.X + diameter >= 1366)
                this.velocity.X *= -1;

            if (this.position.Y + diameter >= 768)
                this.velocity.Y *= -1;
        }

        public bool DetectCollision(Ball ball)
        {
            Vector vec = this.center - ball.center;
            double len = vec.Length();
            if (len < (this.diameter / 2 + ball.diameter / 2))
            {
                return true;
            }
            else return false;
        }

        public void CheckCollision(Ball ball)
        {
            if (DetectCollision(ball))
            {
                var tempVelocity = ChangeVelocities(this, ball);
                ball.velocity = ChangeVelocities(ball, this);
                this.velocity = tempVelocity;
            }
        }

        private static Vector ChangeVelocities(Ball ball1, Ball ball2)
        {
            Vector centresVector = ball2.center - ball1.center;
            Vector ballOnePerpendicular = centresVector.PerpendicularComponent(ball1.velocity);
            Vector ballTwoPerpendicular = centresVector.PerpendicularComponent(ball2.velocity);

            //Vector3 ballOnePara = centresVector.ParralelComponent(velocityOne);
            Vector ballTwoPara = centresVector.ParralelComponent(ball2.velocity);

            Vector ballOneNewVelocity = ballTwoPara + ballOnePerpendicular; //http://sinepost.wordpress.com/category/mathematics/geometry/trigonometry/

            return ballOneNewVelocity; //returns the new velocity of the first ball passed to it
        }
    }
}
