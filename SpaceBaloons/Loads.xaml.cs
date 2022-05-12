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

namespace SpaceBaloons
{
    /// <summary>
    /// Interaction logic for Loads.xaml
    /// </summary>
    public partial class Loads : Window
    {
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
        private void LoadSaveContent(string file)
        {

        }

        
    }
}
