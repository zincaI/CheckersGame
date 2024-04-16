using Checkers.ViewModels;
using Checkers.Views;
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

namespace Checkers.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Game gameWindow = new Game();
            gameWindow.Show();
        }

        private void Show_Checkbox(object sender, RoutedEventArgs e)
        {
            MultipleMoves.Visibility= Visibility.Visible;
            FinishedChoosing.Visibility= Visibility.Visible;
            ShowCheckbox.Visibility= Visibility.Collapsed;
        }

        private void Finished_Choosing_Click(object sender, RoutedEventArgs e)
        {
            MultipleMoves.Visibility = Visibility.Collapsed;
            FinishedChoosing.Visibility = Visibility.Collapsed;
            ShowCheckbox.Visibility = Visibility.Visible;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Name:Zincă Irina-Elena\n\nGroup:10LF224\n\nEmail:irina.zinca@student.unitbv.ro\n\nFeatures:\n-Simple jump\nMultiple jump\n-Save game\nLoad game\n\nRules:Before starting a game the players will choose if they want or not to be able to jump over multiple pieces in a round. By default they will only be allowed to jump over a piece at a time. The game will start with the player at the bottom of the table, after that switching turns after every move or after one player can't capture any more pieces depending on the selected option in the menu. The player that captures all of the adversary's pieces wins.\n");

        }

        public void Load_Click(object sender, RoutedEventArgs e)
        {

            if (DataContext is GameVM gameVM)
            {
                
                Game gameWindow = new Game();

                gameWindow.Show();
                gameWindow.Load_Click(sender,e);
                
            }
        }
    }
}
