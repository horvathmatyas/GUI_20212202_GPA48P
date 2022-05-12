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
using System.Windows.Shapes;
using SpaceBaloons.Logic;

namespace SpaceBaloons
{
    /// <summary>
    /// Interaction logic for PauseWindow.xaml
    /// </summary>
    public partial class PauseWindow : Window
    {
        GameLogic logic;
        public PauseWindow(GameLogic logic)
        {
            this.logic = logic;
            InitializeComponent();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_Load(object sender, RoutedEventArgs e)
        {
            Loads load=new Loads();
        }
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
        private void Button_Click_Continue(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
