using Checkers.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Checkers.Services
{
    internal class Utility
    {
        public static ObservableCollection<ObservableCollection<Piece>> initBoard()
        {
            ObservableCollection<ObservableCollection<Piece>> board = new ObservableCollection<ObservableCollection<Piece>>();
            const int boardSize = 8;

            for (int row = 0; row < boardSize; ++row)
            {
                board.Add(new ObservableCollection<Piece>());
                for (int column = 0; column < boardSize; ++column)
                {
                    if ((row + column) % 2 == 1 && row < 3)
                    {
                        board[row].Add(new Piece(row, column, colorpiece.Red, false));
                        board[row][column].IsVisible = true; // Coloane pare sunt vizibile, iar coloane impare sunt invizibile

                    }
                    else if (row > 4 && (row + column) % 2 == 1)
                    {
                        board[row].Add(new Piece(row, column, colorpiece.Black, false));
                        board[row][column].IsVisible = true; // Coloane pare sunt vizibile, iar coloane impare sunt invizibile

                    }
                    else
                    {
                        board[row].Add(new Piece(row, column, colorpiece.Green, false));

                    }
                }
            }


            return board;
        }


        public static void SaveGame(ObservableCollection<ObservableCollection<Piece>> board)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"; // Filtrul de fișiere pentru dialog
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Directorul inițial

            bool? answer = saveDialog.ShowDialog();
            if (answer == true)
            {
                var path = saveDialog.FileName;
                using (var writer = new StreamWriter(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, board);
                    MessageBox.Show("Jocul a fost salvat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information); // Mesaj de succes
                }
            }
            else
            {
                MessageBox.Show("Salvarea jocului a fost anulată.", "Anulat", MessageBoxButton.OK, MessageBoxImage.Warning); // Mesaj dacă utilizatorul anulează salvarea
            }
        }
        public static ObservableCollection<ObservableCollection<Piece>> LoadGame()
        {
            ObservableCollection<ObservableCollection<Piece>> board = new ObservableCollection<ObservableCollection<Piece>>();

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"; // Filtrul de fișiere pentru dialog
            bool? answer = openDialog.ShowDialog();

            if (answer == true)
            {
                var path = openDialog.FileName;
                using (var reader = new StreamReader(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    board = (ObservableCollection<ObservableCollection<Piece>>)serializer.Deserialize(reader, typeof(ObservableCollection<ObservableCollection<Piece>>));
                }
            }
            return board;
        }

    }
}
