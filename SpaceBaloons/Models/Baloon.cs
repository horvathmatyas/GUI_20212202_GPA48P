using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBaloons.Models
{
    internal class Baloon
    {
        public Point Pos { get; set; }
        public int Speed { get; set; }
        public double Health { get; set; }
        public Baloon(Point pos, int speed, double health)
        {
            Pos = pos;
            Speed = speed;
            Health = health;
        }       
        public void Move(Size size)
        {
            Point newPos = new System.Drawing.Point(Pos.X, Pos.Y+Speed);
            if (InView(newPos,size))
            {
                Pos = newPos;
            }
        }
        public  InView(Point pos, Size area)
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
        public Baloon Pop()
        {
            return new Baloon(Pos,Speed,Health-1);
        }

    }
}
