﻿using System;
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
        public bool InView { get; set; }
        public Baloon(Point pos, int speed, double health)
        {
            Pos = pos;
            Speed = speed;
            Health = health;
        }       
        public void Move(System.Drawing.Size size)
        {
            System.Drawing.Point newPos = new System.Drawing.Point(Pos.X, Pos.Y+Speed);
            if (InView)
            {
                Pos = newPos;
            }
        }
        
        

    }
}