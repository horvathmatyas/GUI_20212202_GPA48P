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
            lb_saves.ItemsSource=GetAllSaves();
            InitializeComponent();
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
        private void LoadSaveContent()
        {
            Player playerToLoad = JsonSerializer.Deserialize<Player>(System.IO.Path.Combine("Save", SelectedSave) + ".json");
            MainWindow m = new MainWindow(playerToLoad);
        }

        public void lb_saves_Selected(object sender, RoutedEventArgs e)
        {
            SelectedSave = (sender as ListBoxItem).Content.ToString();
        }
    }
}
