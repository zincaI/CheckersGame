using Checkers.Models;
using Checkers2.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

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


        public static void SaveGame(ObservableCollection<ObservableCollection<Piece>> board, string extraData, string turnData)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"; // Filtrul de fișiere pentru dialog
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Directorul inițial

            if (saveDialog.ShowDialog() == true)
            {
                try
                {
                    var path = saveDialog.FileName;
                    var gameData = new RoundInfo
                    {
                        Board = board,
                        MultipleJumpsAllowed = extraData,
                        Turn = turnData

                    };

                    using (var writer = new StreamWriter(path))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, gameData);
                        MessageBox.Show("Jocul a fost salvat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information); // Mesaj de succes
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A apărut o eroare la salvarea jocului: " + ex.Message, "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Salvarea jocului a fost anulată.", "Anulat", MessageBoxButton.OK, MessageBoxImage.Warning); // Mesaj dacă utilizatorul anulează salvarea
            }
        }

        public static (ObservableCollection<ObservableCollection<Piece>>, string, string) LoadGame()
        {
            ObservableCollection<ObservableCollection<Piece>> board = new ObservableCollection<ObservableCollection<Piece>>();
            string extraData = string.Empty;
            string turnData = string.Empty;


            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"; // Filtrul de fișiere pentru dialog

            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    var path = openDialog.FileName;
                    using (var reader = new StreamReader(path))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        var gameData = (RoundInfo)serializer.Deserialize(reader, typeof(RoundInfo));
                        board = gameData.Board;
                        extraData = gameData.MultipleJumpsAllowed;
                        turnData = gameData.Turn;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A apărut o eroare la încărcarea jocului: " + ex.Message, "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return (board, extraData, turnData);
        }
        //public static ObservableCollection<ObservableCollection<Piece>> LoadGame()
        //{
        //    ObservableCollection<ObservableCollection<Piece>> board = new ObservableCollection<ObservableCollection<Piece>>();

        //    OpenFileDialog openDialog = new OpenFileDialog();
        //    openDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"; // Filtrul de fișiere pentru dialog
        //    bool? answer = openDialog.ShowDialog();

        //    if (answer == true)
        //    {
        //        var path = openDialog.FileName;
        //        using (var reader = new StreamReader(path))
        //        {
        //            JsonSerializer serializer = new JsonSerializer();
        //            board = (ObservableCollection<ObservableCollection<Piece>>)serializer.Deserialize(reader, typeof(ObservableCollection<ObservableCollection<Piece>>));
        //        }
        //    }
        //    return board;
        //}

    }
}
