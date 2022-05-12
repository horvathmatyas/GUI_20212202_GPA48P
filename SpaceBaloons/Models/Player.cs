using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBaloons.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Highscore { get; set; }
        public int Score { get; set; } 
        public double AttackSpeed { get; set; } //rate of fire per second (1 per second at the start)
        public double Cooldown { get; set; } //the time it takes to cool the turret down after reaching maxheat (decreased by 1 every 0.2 seconds)
        public double HeatGain { get; set; } //heat added to CurrentHeat pershot (at the beggining 1 per shot)
        public double CurrentHeat { get; set; } //the current heat of the turret (max at 100)
        public int Health { get; set; }

        public int Level { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
            Highscore = 0;
            AttackSpeed = 4;
            Cooldown = 1;
            HeatGain = 3;
            CurrentHeat = 0;
            Health = 100;
            Level = 1;
        }
        public bool InView(Point pos)
        {
            if (pos.X >= 20 &&
                pos.X <= 802
                )
            {
                return true;
            }
            return false;
        }
        public Player LoadPlayer(string name, int score,int hs, int attackSpeed, int cooldown, double heatGain, double currentHeat, int health) //needed to load player stats
        {
            return new Player(name)
            {
                Score = score,
                Highscore = hs,
                AttackSpeed = attackSpeed,
                Cooldown = cooldown,
                HeatGain = heatGain,
                CurrentHeat = currentHeat,
                Health = health
            };
        }
    }
}
