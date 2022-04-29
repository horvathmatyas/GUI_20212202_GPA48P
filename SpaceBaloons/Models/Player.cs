using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBaloons.Models
{
    internal class Player
    {
        public string Name { get; set; }
        public int Score { get; set; } 
        public int AttackSpeed { get; set; } //rate of fire per second (1 per second at the start)
        public int Cooldown { get; set; } //the time it takes to cool the turret down after reaching maxheat (decreased by 1 every 0.2 seconds)
        public double HeatGain { get; set; } //heat added to CurrentHeat pershot (at the beggining 1 per shot)
        public double CurrentHeat { get; set; } //the current heat of the turret (max at 100)
        public int Health { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
            AttackSpeed = 1;
            Cooldown = 1;
            HeatGain = 1;
            CurrentHeat = 0;
            Health = 100;
        }

        public Player LoadPlayer(int name, int score, int attackSpeed, int cooldown, double heatGain, double currentHeat, int health) //needed to load player stats
        {
            return new Player(name)
            {
                Score = score,
                AttackSpeed = attackSpeed,
                Cooldown = cooldown,
                HeatGain = heatGain,
                CurrentHeat = currentHeat,
                Health = health
            };
        }
    }
}
