using SpaceBaloons.Converter;
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
        static public bool fireIng=false;
        bool goingLeft = false;
        bool goingRight = false;
        bool RH = false;
        bool IA = false;
        bool RC = false;
        public string name { get; set; }

        public NumberToColorConverter cv = new NumberToColorConverter();
        public MainWindow(string name)
        {
            if (logic!=null)
            {
                name = logic.player.Name;
            }
            else
            {
                this.name = name; //itt visszaall a nev a regire valamiert
            }
            
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
            if (RH)
            {
                logic.Control(GameLogic.Controls.Rheat);
            }
            if (IA)
            {
                logic.Control(GameLogic.Controls.IncAtt);
            }
            if (RC)
            {
                logic.Control(GameLogic.Controls.RCD);
            }
            if (logic.player!=null)
            {
                hppb.Value = logic.player.Health;
                hppb.Foreground = cv.Convert(logic.player.Health);
                heatpb.Value = logic.player.CurrentHeat;
                heatpb.Foreground = cv.Convert(100 - (int)logic.player.CurrentHeat);
                slb.Content = logic.player.Score;
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
            if (e.Key == Key.Q)
            {
                logic.Control(GameLogic.Controls.Rheat);
            }
            if (e.Key == Key.W)
            {
                logic.Control(GameLogic.Controls.IncAtt);
            }
            if (e.Key == Key.E)
            {
                logic.Control(GameLogic.Controls.RCD);
            }
            if (e.Key == Key.P)
            {
                Logic_GameOver(this, null);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            logic = new GameLogic();
            logic.GameOver += Logic_GameOver;
            logic.NextLevel += Logic_NextLevel;
            display.SetupModel(logic);
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(20);
            dt.Tick += Dt_Tick;
            dt.Start();

            logic.SetupGame(new Size(game_grid.ActualWidth, grid.ActualHeight), name);
            display.SetupSizes(new Size(game_grid.ActualWidth, grid.ActualHeight));
            logic.ReadHs();
            lb_highScore.ItemsSource = logic.Highscores;


        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (logic != null)
            {
                logic.SetupGame(new Size(game_grid.ActualWidth, grid.ActualHeight), name);
                display.SetupSizes(new Size(game_grid.ActualWidth, grid.ActualHeight));
            }
        }
        private void Logic_NextLevel(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Advance to next level?", "Level Up!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                logic.player.Level++;
            }
            else
            {
                var r = MessageBox.Show("Game Over");
                if (r == MessageBoxResult.OK)
                {
                    logic.WriteHs();
                    this.Close();
                }
            }
 
        }
        private void Logic_GameOver(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Game Over");
            if (result == MessageBoxResult.OK)
            {
                logic.WriteHs();
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
            if (e.Key == Key.Q)
            {
                RH = false;
            }
            if (e.Key == Key.W)
            {
                IA = false;
            }
            if (e.Key == Key.E)
            {
                RC = false;
            }
        }
    }
}