using Checkers.Models;
using Checkers.ViewModels;
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

namespace Checkers.Views
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        GameVM gm;
        public Game()
        {
            InitializeComponent();
            gm = new GameVM();
            DataContext = gm;
        }
        public void Save_Click(object sender, RoutedEventArgs e)
        {
            gm.SaveClick();
        }

        public void Load_Click(object sender, RoutedEventArgs e)
        {
            gm.Load_Click();
        }

    }
}
