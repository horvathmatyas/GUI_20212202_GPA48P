using System;
using System.Collections.Generic;
using System.IO;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using SpaceBaloons.Models;

namespace SpaceBaloons
{
    /// <summary>
    /// Interaction logic for Loads.xaml
    /// </summary>
    public partial class Loads : Window
    {
        public string SelectedSave { get; set; }
        public Loads()
        {
            
            InitializeComponent();
            lb_saves.ItemsSource = GetAllSaves();
            
        }
        private List<string> GetAllSaves()
        {
            List<string> saves = new List<string>();
            foreach (var item in Directory.GetFiles("Save"))
            {
                saves.Add(item.Split(".")[0]);

            }
            return saves;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamReader st = new StreamReader(SelectedSave+".json");
            string content=st.ReadToEnd();
            Player playerToLoad = JsonSerializer.Deserialize<Player>(content);
            MainWindow m = new MainWindow(playerToLoad);
            m.Show();
            this.Close();
        }

        private void lb_saves_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            SelectedSave = (sender as ListBox).SelectedItem.ToString();
        }
    }
}
