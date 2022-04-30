﻿using SpaceBaloons.Logic;
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

namespace SpaceBaloons
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameLogic logic;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                logic.Control(GameLogic.Controls.Left);
            }
            else if (e.Key == Key.A)
            {
                logic.Control(GameLogic.Controls.Right);
            }
            else if (e.Key == Key.Right)
            {
                logic.Control(GameLogic.Controls.Right);
            }
            else if (e.Key == Key.D)
            {
                logic.Control(GameLogic.Controls.Right);
            }
            else if (e.Key == Key.Space)
            {
                logic.Control(GameLogic.Controls.Shoot);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new GameLogic();
            display.SetupModel(logic);
            logic.SetupGame(new Size(game_grid.ActualWidth, grid.ActualHeight), "xy");
            display.SetupSizes(new Size(game_grid.ActualWidth,grid.ActualHeight));
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (logic != null)
            {
                logic.SetupGame(new Size(game_grid.ActualWidth, grid.ActualHeight), "xy");
                display.SetupSizes(new Size(game_grid.ActualWidth, grid.ActualHeight));
            }
        }
    }
}
