using SpaceBaloons.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBaloons.Logic
{
    internal class GameLogic : Interface.GameModel
    {
        public Point PlayerPos { get ; set; }
        public List<Laser> Lasers { get; set; }
        public List<Baloon> Baloons { get; set; }

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
                    PlayerPos=new Point(PlayerPos.X-1,0);
                    break;
                case Controls.Right:
                    PlayerPos = new Point(PlayerPos.X + 1, 0);
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
                if (Lasers[i].InView(Lasers[i].Pos,area))
                {

                }
            }
        }
    }
}
