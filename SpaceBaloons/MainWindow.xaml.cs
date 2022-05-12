using SpaceBaloons.Converter;
using SpaceBaloons.Logic;
using SpaceBaloons.Models;
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
        public DispatcherTimer dt;
        public string name { get; set; }
        public Player loadedPlayer;

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
                name = null;
            }
            Application.Current.MainWindow = this;
            dt = new DispatcherTimer();
            InitializeComponent();
        }
        public MainWindow(Player p)
        {
            if (logic != null)
            {
                name = logic.player.Name;
            }
            else
            {
                loadedPlayer=p;
            }
            InitializeComponent();
            dt = new DispatcherTimer();
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
                if (logic.player.AttackSpeed == 0)
                {
                    lb_ias.Content = "MAX";
                }
                else
                {
                    lb_ias.Content = logic.player.AttackSpeed;
                }
                if (logic.player.HeatGain == 0)
                {
                    lb_rdh.Content = "MAX";
                }
                else
                {
                    lb_rdh.Content = logic.player.HeatGain;
                }
                lb_rcd.Content = logic.player.Cooldown;
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
                if (!dt.IsEnabled)
                {
                    dt.Start();
                }
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
            if (e.Key==Key.Escape)
            {
                PauseWindow p = new PauseWindow(logic,this);
                dt.Stop();
                p.ShowDialog();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            logic = new GameLogic();
            logic.GameOver += Logic_GameOver;
            logic.NextLevel += Logic_NextLevel;
            logic.FinishGame += Logic_Finish;
            display.SetupModel(logic);
            
            dt.Interval = TimeSpan.FromMilliseconds(20);
            dt.Tick += Dt_Tick;
            dt.Start();
            if (loadedPlayer!=null)
            {
                logic.SetupGame(new Size(game_grid.ActualWidth, grid.ActualHeight));
                logic.player = loadedPlayer;
            }
            else if (logic.player is null)
            {
                logic.SetupGame(new Size(game_grid.ActualWidth, grid.ActualHeight), name);
            }
            else
            {
                logic.SetupGame(new Size(game_grid.ActualWidth, grid.ActualHeight));
                logic.player = loadedPlayer;
            }
            display.SetupSizes(new Size(game_grid.ActualWidth, grid.ActualHeight));
            logic.ReadHs();
            logic.SortHS();
            Style s = new Style(typeof(Label));
            s.Setters.Add(new Setter(Label.ForegroundProperty, Brushes.Black));
            s.Setters.Add(new Setter(Label.FontSizeProperty,30.0));
            s.Setters.Add(new Setter(Label.FontWeightProperty, FontWeights.UltraBold));
            s.Setters.Add(new Setter(Label.FontFamilyProperty, new FontFamily("Arial")));
            s.Setters.Add(new Setter(Label.BorderThicknessProperty, new Thickness(5)));
            s.Setters.Add(new Setter(Label.BorderBrushProperty, Brushes.LightGray));
            s.Setters.Add(new Setter(Label.HorizontalAlignmentProperty,HorizontalAlignment.Stretch));
            foreach (var item in logic.Highscores)
            {
                Label lbl = new Label();
                lbl.Content = item;
                lbl.Style = s;
                lb_highScore.Items.Add(lbl);
            }
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
        private void Logic_Finish(object? sender, EventArgs e)
        {
            MessageBox.Show("You saved Earth and everyon on it, Good Job!", "Now you can play the game for eternety", MessageBoxButton.OK);               
        }
        private void Logic_GameOver(object? sender, EventArgs e)
        {            
            logic.WriteHs();
            GameOver gameOver = new GameOver();
            dt.Stop();
            gameOver.ShowDialog();
            this.Close();                          
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