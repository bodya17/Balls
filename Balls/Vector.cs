using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balls
{
    class Vector
    {
        private double x, y;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public Vector()
        {
            x = 0.0;
            y = 0.0;
        }

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector(Vector v)
        {
            x = v.x;
            y = v.y;
        }

        public double Length()
        {
            return Math.Sqrt(x * x + y * y);
        }

        // Множення на скаляр
        public Vector Scale(double scale)
        {
            return new Vector(scale * x, scale * y);
        }

        public static Vector operator *(double k, Vector v1)
        {
            return new Vector(k * v1.x, k * v1.y);
        }

        public static Vector operator *(Vector v1, double k)
        {
            return new Vector(k * v1.x, k * v1.y);
        }


        // Перевантаження операції додавання для векторів
        public static Vector operator + (Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        // Перевантаження віднімання
        public static Vector operator - (Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y); 
        }

        // Cкалярне множення
        public double DotProduct(Vector v2)
        {
            return (x * v2.X + y * v2.Y);
        }

        // Нормування вектора
        public Vector Unit()
        {
            double length = this.Length();
            return new Vector(x / length, y / length);
        }

        // Проекткування вектора v2 на вектор this
        public Vector ParralelComponent(Vector v2)
        {
            double lengthSquared, dotProduct, scale;
            lengthSquared = Math.Pow(Length(), 2);
            dotProduct = DotProduct(v2);
            if (lengthSquared != 0)
                scale = dotProduct / lengthSquared;
            else
                return new Vector();
            return new Vector(this.Scale(scale));
        }

        public Vector PerpendicularComponent(Vector v2)
        {
            //subtract the parallel component from the orginal vecot
            //to get the orthogonal bit

            return new Vector(v2 - this.ParralelComponent(v2)); // perpendicular component = vectorAnswer - parallel component
        }

    }
}
