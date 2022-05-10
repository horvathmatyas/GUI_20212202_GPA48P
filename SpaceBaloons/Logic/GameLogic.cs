using SpaceBaloons.Models;
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
        public event EventHandler Changed;
        public event EventHandler GameOver;
        public System.Drawing.Point PlayerPos { get ; set; }
        public List<Laser> Lasers { get; set; }
        public List<Baloon> Baloons { get; set; }
        public Player player { get; set; }
        int spawnTimer=0; // a lufi spawnolasahoz kell
        int waveNumber = 0; // hanyszor spawnolt wavet -- meglehet allapitani hol tart a jatek
        public int shootTimer = 0;
        static Random random = new Random();


        System.Windows.Size area;
        public enum Controls
        {
            Left, Right, Shoot
        }
        public void SetupGame(System.Windows.Size area, string name)
        {
            this.area = area;
            Lasers=new List<Laser>();
            Baloons=new List<Baloon>();
            player = new Player(name);
            PlayerPos = new System.Drawing.Point((int)area.Width / 2,(int)area.Height / 10 * 8 + 50);
            
        }
        public GameLogic()
        {

        }
        public void NewShot()
        {
            if (player.AttackSpeed < shootTimer && player.CurrentHeat < 100)
            {
                Lasers.Add(new Laser(PlayerPos, 30));
                player.CurrentHeat += player.HeatGain;
                shootTimer = 0;
            }
            //if (player.CurrentHeat < 100)
            //{
            //    Lasers.Add(new Laser(PlayerPos, 30));
            //    player.CurrentHeat += player.HeatGain;
            //}
        }

        private void newWave()
        {
            double i = waveNumber+2;
            int j = 0;
            while (j != (int)i)
            {
                int hp = 0;
                if (i < 17 && i > 6)
                {
                    hp = random.Next((int)i - 7, 7);
                }
                else if (i <= 6)
                {
                    hp = random.Next(0, (int)i);
                }
                else
                {
                    hp = 6;

                }
                Baloons.Add(new Baloon(new System.Drawing.Point(random.Next(25, (int)area.Width - 25), 25), 2, hp, hp));
                j++;
            }
            waveNumber++;
        }

        public void Control(Controls controls)
        {
            System.Drawing.Point newPos = new System.Drawing.Point();
            switch (controls)
            {
                case Controls.Left:
                    newPos=new System.Drawing.Point(PlayerPos.X-15,PlayerPos.Y);
                    break;
                case Controls.Right:
                    newPos = new System.Drawing.Point(PlayerPos.X + 15, PlayerPos.Y);
                    break;
                case Controls.Shoot:
                    NewShot();
                    break;
                default:
                    break;
            }
            if (player.InView(newPos)) //20
            {
                PlayerPos = newPos;
            }
            Changed?.Invoke(this, null);
        }
        public void TimeStep()
        {
            //for (int i = 0; i < Lasers.Count; i++)
            //{
            //    Lasers[j].Move();
            //    bool isin = Lasers[i].InView(Lasers[i].Pos, new System.Drawing.Size((int)area.Width, (int)area.Height));
            //    if (!isin)
            //    {
            //        Lasers.RemoveAt(i);
            //    }
            //}

            for (int i = 0; i < Baloons.Count; i++)
            {
                Baloons[i].Move();
                Rect baloonRect = new Rect(Baloons[i].Pos.X - 20, Baloons[i].Pos.Y - 20, 40, 40);
                Rect shipRect = new Rect(0, area.Height / 10 * 9,area.Width, area.Height / 10);
                if (baloonRect.IntersectsWith(shipRect))
                {
                    if (player.Health > Baloons[i].Health)
                    {
                        player.Health -= Baloons[i].Health;
                        Baloons.RemoveAt(i);
                    }
                    else
                    {
                        Baloons.RemoveAt(i);
                        if (player.Score > player.Highscore)
                        {
                            player.Highscore = player.Score;
                        }
                        GameOver?.Invoke(this, null);

                    }
                }
                for (int j = 0; j < Lasers.Count; j++)
                {
                    Lasers[j].Move();
                    bool isin = Lasers[j].InView(Lasers[j].Pos, new System.Drawing.Size((int)area.Width, (int)area.Height));
                    if (!isin)
                    {
                        Lasers.RemoveAt(j);
                    }
                    else
                    {
                        Rect laserRect = new Rect(Lasers[j].Pos.X - 4, Lasers[j].Pos.Y - 6, 8, 12);
                        if (laserRect.IntersectsWith(baloonRect))
                        {
                                if (Baloons[i].Health == 1)
                                {
                                    player.Score += Baloons[i].Health;
                                    Baloons.RemoveAt(i);
                                    Lasers.RemoveAt(j);
                                }
                                else
                                {
                                    Baloons.Add(Baloons[i].Pop());
                                    Baloons.RemoveAt(i);
                                    Lasers.RemoveAt(j); 
                                    player.Score += Baloons[i].Type;                          
                                }


                        }
                    }
                    
                }
 
            }
            if (spawnTimer==100)
            {
                newWave();
                Baloons.Add(new Baloon(new System.Drawing.Point(random.Next(25, (int)area.Width - 25), 25), 5, 6, 6));
                spawnTimer = 0;
            }
            spawnTimer ++;
            shootTimer++;
            Changed?.Invoke(this, null);
        }
    }
}
