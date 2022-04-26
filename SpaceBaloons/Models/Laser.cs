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
        public Laser(Point pos, int speed)
        {
            Pos = pos;
            Speed = speed;
        }
        public void Move(Size size)
        {
            Point newPos = new Point(Pos.X, Pos.Y + Speed);
            if (InView(newPos,size))
            {
                Pos = newPos;
            }
        }
        public bool InView(Point pos, Size area)
        {
            if (pos.X >= 0 &&
                pos.X <= area.Width &&
                pos.Y >= 0 &&
                pos.Y <= area.Height
                )
            {
                return true;
            }
            return false;
        }
    }
}
