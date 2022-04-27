﻿using SpaceBaloons.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpaceBaloons.Logic
{
    internal class GameLogic : Interface.GameModel
    {
        public System.Drawing.Point PlayerPos { get ; set; }
        public List<Laser> Lasers { get; set; }
        public List<Baloon> Baloons { get; set; }
        public Player player { get; set; }

        static Random r = new Random();
        public event EventHandler Changed;
        System.Windows.Size area;
        public enum Controls
        {
            Left, Right, Shoot
        }
        public void SetupGame(System.Windows.Size area)
        {
            this.area = area;
            Lasers=new List<Laser>();
            Baloons=new List<Baloon>();
        }
        public GameLogic()
        {

        }
        public void Control(Controls controls)
        {
            switch (controls)
            {
                case Controls.Left:
                    PlayerPos=new System.Drawing.Point(PlayerPos.X-1,0);
                    break;
                case Controls.Right:
                    PlayerPos = new System.Drawing.Point(PlayerPos.X + 1, 0);
                    break;
                case Controls.Shoot:
                    break;
                default:
                    break;
            }
            Changed?.Invoke(this, null);
        }
        public void TimeStep()
        {
            for (int i = 0; i < Lasers.Count; i++)
            {
                bool isin = Lasers[i].InView(Lasers[i].Pos, new System.Drawing.Size((int)area.Width, (int)area.Height));
                if (!isin)
                {
                    Lasers.RemoveAt(i);
                }
            }

            for (int i = 0; i < Baloons.Count; i++)
            {
                Rect baloonRect = new Rect(Baloons[i].Pos.X - 12, Baloons[i].Pos.Y - 12, 25, 25);
                Rect shipRect = new Rect(0, area.Height / 10,area.Width, area.Height / 10);
                if (baloonRect.IntersectsWith(shipRect))
                {
                    player.Health -= Baloons[i].Health;
                    Baloons.RemoveAt(i);
                }
                for (int j = 0; j < Lasers.Count; j++)
                {
                    Rect laserRect = new Rect(Lasers[i].Pos.X - 3, Lasers[i].Pos.Y - 5, 6, 10);
                    if (laserRect.IntersectsWith(baloonRect))
                    {
                        Baloons.RemoveAt(i);
                        Lasers.RemoveAt(j);
                    }
                }
 
            }
            Changed?.Invoke(this, null);
        }
    }
}