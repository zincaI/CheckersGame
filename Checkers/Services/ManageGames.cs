using System;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using System.Windows;
using System.Linq;
using Checkers.Models;

namespace Checkers.Services
{
    internal class ManageGames
    {
        public static ObservableCollection<ObservableCollection<Piece>> ReadBoardFromJson()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Board.json");
            ObservableCollection<ObservableCollection<Piece>> board;
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    board = JsonConvert.DeserializeObject<ObservableCollection<ObservableCollection<Piece>>>(json);
                }
                else
                {
                    board = new ObservableCollection<ObservableCollection<Piece>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON file: " + ex.Message);
                board = new ObservableCollection<ObservableCollection<Piece>>();
            }
            return board;
        }

        public static void WriteBoardToJson(ObservableCollection<ObservableCollection<Piece>> board)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Board.json");
            try
            {
                ObservableCollection<ObservableCollection<Piece>> existingBoard = ReadBoardFromJson();

                // Adaugă noile date la cele existente
                foreach (var row in board)
                {
                    existingBoard.Add(row);
                }

                string updatedJson = JsonConvert.SerializeObject(existingBoard, Formatting.Indented);
                File.WriteAllText(filePath, updatedJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to JSON file: " + ex.Message);
            }
        }

        public static ObservableCollection<ObservableCollection<Piece>> GetMatrixFromJson(int matrixIndex)
        {
            ObservableCollection<ObservableCollection<Piece>>[] matrices;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Board.json");

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    matrices = JsonConvert.DeserializeObject<ObservableCollection<ObservableCollection<Piece>>[]>(json);
                    if (matrixIndex >= 0 && matrixIndex < matrices.Length)
                    {
                        return matrices[matrixIndex];
                    }
                    else
                    {
                        Console.WriteLine("Invalid matrix index.");
                    }
                    //MessageBox.Show(matrices[matrixIndex].Count);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON file: " + ex.Message);
            }
            // Returnăm o matrice goală dacă nu am putut citi din fișier, dacă fișierul nu conține matrici valide sau dacă indexul matricei este invalid
            return new ObservableCollection<ObservableCollection<Piece>>();
        }

        public static (int redScore, int maxRed, int blackScore, int maxBlack) ReadStatisticsFromJson()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Integers.json");
            (int redScore, int maxRed, int blackScore, int maxBlack) result = (0, 0, 0, 0);
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var statistics = JsonConvert.DeserializeObject<dynamic>(json);
                    result = (
                        redScore: (int)statistics.RedScore,
                        maxRed: (int)statistics.MaxRed,
                        blackScore: (int)statistics.BlackScore,
                        maxBlack: (int)statistics.MaxBlack
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON file: " + ex.Message);
            }
            return result;
        }


        public static void WriteStatisticsToJson((int redScore, int maxRed, int blackScore, int maxBlack) numbers)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Integers.json");
            try
            {
                var statistics = new
                {
                    RedScore = numbers.redScore,
                    MaxRed = numbers.maxRed,
                    BlackScore = numbers.blackScore,
                    MaxBlack = numbers.maxBlack
                };

                string updatedJson = JsonConvert.SerializeObject(statistics, Formatting.Indented);
                File.WriteAllText(filePath, updatedJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to JSON file: " + ex.Message);
            }
        }


    }
}
