using SpaceBaloons.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBaloons.Interface
{
    internal interface GameModel
    {
        Point PlayerPos { get; set; }
        List<Laser> Lasers { get; set; }
        List<Baloon> Baloons { get; set; }
        event EventHandler Changed;
        Player player { get; set; }
    }
}
