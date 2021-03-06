using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBaloons.Models
{
    public class Baloon
    {
        public Point Pos { get; set; }
        public int Speed { get; set; }
        public int Health { get; set; }
        public Baloon(Point pos, int speed, int health)
        {
            Pos = pos;
            Speed = speed;
            Health = health;

        }
        public void Move()
        {
            Point newPos = new System.Drawing.Point(Pos.X, Pos.Y+Speed);                       
            Pos = newPos;            
        }
 
        
        public Baloon Pop()
        {
            return new Baloon(Pos,Speed, Health - 1);
        }

    }
}
