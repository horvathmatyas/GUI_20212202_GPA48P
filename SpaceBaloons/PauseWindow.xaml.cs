using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.Text.Json.Serialization;
using System.IO;

namespace SpaceBaloons
{
    /// <summary>
    /// Interaction logic for PauseWindow.xaml
    /// </summary>
    public partial class PauseWindow : Window
    {
        GameLogic logic;
        MainWindow m;
        public PauseWindow(GameLogic logic,MainWindow m)
        {
            this.m= m;
            this.logic = logic;
            InitializeComponent();
        }
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            string save = JsonSerializer.Serialize(logic.player);
            try
            {
                File.WriteAllText(System.IO.Path.Combine("Save", logic.player.Name) + ".json", save);

            }
            catch (IOException ioe)
            {
                MessageBox.Show("Már van ilyen nevű mentés");
            }
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
            m.dt.Start();
            this.Close();      
        }
    }
}
