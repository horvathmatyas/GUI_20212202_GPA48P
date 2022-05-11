using SpaceBaloons.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpaceBaloons
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal GameLogic logic;
        bool fireIng=false;
        bool goingLeft = false;
        bool goingRight = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Dt_Tick(object? sender, EventArgs e)
        {           
            if (fireIng)
            {
                logic.Control(GameLogic.Controls.Shoot);
            }
            if (goingRight)
            {
                logic.Control(GameLogic.Controls.Right);
            }
            if (goingLeft)
            {
                logic.Control(GameLogic.Controls.Left);
            }
            logic.TimeStep();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.A)
            {
                goingLeft = true;
            }
            if (e.Key == Key.Right || e.Key == Key.D)
            {              
                goingRight = true;
            }
            if (e.Key == Key.Space)
            {
                fireIng = true;              
            }   
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new GameLogic();
            logic.GameOver += Logic_GameOver;
            display.SetupModel(logic);

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(30);
            dt.Tick += Dt_Tick;
            dt.Start();

            logic.SetupGame(new Size(game_grid.ActualWidth, grid.ActualHeight), "xy");
            display.SetupSizes(new Size(game_grid.ActualWidth, grid.ActualHeight));
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (logic != null)
            {
                logic.SetupGame(new Size(game_grid.ActualWidth, grid.ActualHeight), "xy");
                display.SetupSizes(new Size(game_grid.ActualWidth, grid.ActualHeight));
            }
        }

        private void Logic_GameOver(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Game Over");
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                fireIng = false;
            }
            if (e.Key == Key.Left)
            {
                goingLeft = false;
            }
            if (e.Key == Key.A)
            {
                goingLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goingRight = false;
            }
            if (e.Key == Key.D)
            {
                goingRight = false;
            }
        }

        private void Reduce_Heat(object sender, RoutedEventArgs e)
        {
            logic.player.CurrentHeat -= 0.1;
        }

        private void Increase_Attack_Speed(object sender, RoutedEventArgs e)
        {
            logic.player.AttackSpeed -= 1;

        }

        private void Reduce_Cooldown(object sender, RoutedEventArgs e)
        {
            logic.player.Cooldown -= 1;

        }
    }
}