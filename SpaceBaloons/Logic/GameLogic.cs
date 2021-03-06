using SpaceBaloons.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace SpaceBaloons.Logic
{
    public class GameLogic : Interface.GameModel
    {
        public event EventHandler Changed;
        public event EventHandler GameOver;
        public event EventHandler NextLevel;
        public event EventHandler FinishGame;
        public System.Drawing.Point PlayerPos { get; set; }
        public List<Laser> Lasers { get; set; }
        public List<Baloon> Baloons { get; set; }
        public Player player { get; set; }
        int spawnTimer = 0; // a lufi spawnolasahoz kell
        int waveNumber = 0; // hanyszor spawnolt wavet -- meglehet allapitani hol tart a jatek
        int cdTime = 0;
        public int shootTimer = 0;
        static Random random = new Random();
        public List<string> Highscores { get; set; }


        public void ReduceHeat()
        {
            if (player.HeatGain >= 0.1 && player.Score >= 10)
            {
                player.HeatGain -= 0.1;
                player.Score -= 10;
            }
        }
        public void IncreaseAttackSpeed()
        {
            if (player.AttackSpeed >= 0.4 && player.Score >= 10)
            {
                player.AttackSpeed -= 0.4;
                player.Score -= 10;

            }
        }
        public void ReduceCooldown()
        {
            if (player.Score >= 10)
            {
                player.Cooldown += 0.1;
                player.Score -= 10;

            }
        }

        System.Windows.Size area;
        public enum Controls
        {
            Left, Right, Shoot, Rheat, IncAtt, RCD
        }
        public void SetupGame(System.Windows.Size area, string name)
        {
            Highscores = new List<string>();
            this.area = area;
            Lasers = new List<Laser>();
            Baloons = new List<Baloon>();
            player = new Player(name);
            PlayerPos = new System.Drawing.Point((int)area.Width / 2, (int)area.Height / 10 * 8 + 50);

        }
        public void SetupGame(System.Windows.Size area)
        {
            Highscores = new List<string>();
            this.area = area;
            Lasers = new List<Laser>();
            Baloons = new List<Baloon>();
            PlayerPos = new System.Drawing.Point((int)area.Width / 2, (int)area.Height / 10 * 8 + 50);

        }
        public GameLogic()
        {

        }
        public void NewShot()
        {
            if (player.AttackSpeed < shootTimer && player.CurrentHeat < 100)
            {
                Lasers.Add(new Laser(PlayerPos, 20));
                player.CurrentHeat += player.HeatGain;
                shootTimer = 0;
            }
        }

        private void newWave()
        {
            if (waveNumber <= 4 || player.Level==3)
            {
                double i = waveNumber;
                int j = 0;
                while (j != (int)i)
                {
                    int hp = 0;
                    if (i < 10 && i > 3)
                    {
                        hp = random.Next(4, 7);
                    }
                    else if (i <= 3)
                    {
                        hp = random.Next(1, 4);
                    }
                    else
                    {
                        hp = 6;
                    }
                    Baloons.Add(new Baloon(new System.Drawing.Point(random.Next(25, (int)area.Width - 25), random.Next(25, 100)), player.Level + 1, hp));
                    j++;
                }
            }
            else if (player.Level == 1 && Baloons.Count == 0)
            {
                waveNumber = 0;
                NextLevel?.Invoke(this, null);
            }
            else if (player.Level == 2 && Baloons.Count == 0)
            {

                waveNumber = 0;
                NextLevel?.Invoke(this, null);
            }
            else if (player.Level == 3 && Baloons.Count == 0)
            {
                waveNumber = 0;
                FinishGame?.Invoke(this, null);
            }
            waveNumber++;

            //else if (waveNumber == ?? && player.Level == 3)
            //{

            //}
            //Végtelen wave?
        }

        public void Control(Controls controls)
        {
            System.Drawing.Point newPos = new System.Drawing.Point();
            switch (controls)
            {
                case Controls.Left:
                    newPos = new System.Drawing.Point(PlayerPos.X - 15, PlayerPos.Y);
                    break;
                case Controls.Right:
                    newPos = new System.Drawing.Point(PlayerPos.X + 15, PlayerPos.Y);
                    break;
                case Controls.Shoot:
                    NewShot();
                    break;
                case Controls.Rheat:
                    ReduceHeat();
                    break;
                case Controls.IncAtt:
                    IncreaseAttackSpeed();
                    break;
                case Controls.RCD:
                    ReduceCooldown();
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
            if (!MainWindow.fireIng)
            {
                if (player.CurrentHeat > 0)
                {
                    player.CurrentHeat -= player.Cooldown;
                }
            }
            
            for (int i = 0; i < Lasers.Count; i++)
            {
                Lasers[i].Move();
                bool isin = Lasers[i].InView(Lasers[i].Pos, new System.Drawing.Size((int)area.Width, (int)area.Height));
                if (!isin)
                {
                    Lasers.RemoveAt(i);
                }
            }

            for (int i = 0; i < Baloons.Count; i++)
            {
                Baloons[i].Move();
                Rect baloonRect = new Rect(Baloons[i].Pos.X - 20, Baloons[i].Pos.Y - 20, 50, 50);
                Rect shipRect = new Rect(0, area.Height / 10 * 8.5, area.Width, area.Height / 10 + 55);
                if (baloonRect.IntersectsWith(shipRect))
                {
                    if (player.Health < Baloons[i].Health)
                    {
                        Baloons.Clear();
                        if (player.Score > player.Highscore)
                        {
                            player.Highscore = player.Score;
                        }
                        GameOver?.Invoke(this, null);
                        break;
                    }
                    player.Health -= Baloons[i].Health;
                    Baloons.RemoveAt(i);
                }
                for (int j = 0; j < Lasers.Count; j++)
                {
                    Rect laserRect = new Rect(Lasers[j].Pos.X - 4, Lasers[j].Pos.Y - 6, 8, 12);
                    if (laserRect.IntersectsWith(baloonRect))
                    {
                        if (Baloons[i]!=null && Baloons[i].Health <= 1)
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
                            player.Score++;
                        }
                    }
                }
            }
            if (spawnTimer == 150)
            {
                newWave();
                spawnTimer = 0;
            }
            spawnTimer++;
            shootTimer++;
            
            cdTime++;
            Changed?.Invoke(this, null);
            
        }
        
 
        public void ReadHs()
        {
            string[] lines = File.ReadAllLines(Path.Combine("HsFile", "hs.txt"));
            foreach (string line in lines)
            {
                Highscores.Add(line);
            }
        }
        public void WriteHs()
        {
            string newName = player.Name;
            int newScore = player.Score;
            string newhs = "";
            string[] fileContent = File.ReadAllLines(Path.Combine("HsFile", "hs.txt"));
            if (fileContent.Length==0)
            {
                newhs=player.Name + ":" + player.Score;
            }
            else
            {
                newhs = "\n" + player.Name + ":" + player.Score;
            }           
            File.AppendAllText(Path.Combine("HsFile", "hs.txt"), newhs);

        }
        public void SortHS()
        {
            Highscores=Highscores.OrderByDescending(x => int.Parse(x.Split(":")[1])).ToList();
        }
    }
}
