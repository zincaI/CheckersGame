using System;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using Checkers.Models;

namespace Checkers.Services
{
    internal class ManageJson
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

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON file: " + ex.Message);
            }
            return new ObservableCollection<ObservableCollection<Piece>>();
        }


        public static (int, int) ReadStatisticsFromJson()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Integers.json");
            (int, int) result = (0, 0);
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    result = JsonConvert.DeserializeObject<(int, int)>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON file: " + ex.Message);
            }
            return result;
        }

        public static void WriteStatisticsToJson((int, int) numbers)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Integers.json");
            try
            {
                string updatedJson = JsonConvert.SerializeObject(numbers);
                File.WriteAllText(filePath, updatedJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to JSON file: " + ex.Message);
            }
        }

    }
}
