using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBaloons.Models
{
    internal class Laser
    {        
        public Point Pos { get; set; }
        public int Speed { get; set; }
        public bool InView { get; set; }
        public Laser(Point pos, int speed, bool inView)
        {
            Pos = pos;
            Speed = speed;
            InView = inView;
        }
        public void Move(Size size)
        {
            Point newPos = new Point(Pos.X, Pos.Y + Speed);
            if (InView)
            {
                Pos = newPos;
            }
        }
    }
}
