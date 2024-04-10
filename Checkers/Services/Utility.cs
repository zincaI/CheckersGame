using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Checkers.Services
{
    internal class Utility
    {

        public static Piece currentsq { get; set; }
        private static Dictionary<Piece, Piece> neighbours = new Dictionary<Piece, Piece>();

        private static int collectedRedPieces = 0;
        private static int collectedWhitePieces = 0;


        public static Dictionary<Piece, Piece> Neighbours
        {
            get
            {
                return neighbours;
            }
            set
            {
                neighbours = value;
            }
        }

        public static int CollectedWhitePieces
        {
            get { return collectedWhitePieces; }
            set { collectedWhitePieces = value; }
        }

        public static int CollectedRedPieces
        {
            get { return collectedRedPieces; }
            set { collectedRedPieces = value; }
        }


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
                        board[row][column].IsVisible = true; 

                    }
                    else if (row > 4 && (row + column) % 2 == 1)
                    {
                        board[row].Add(new Piece(row, column, colorpiece.Black, false));
                        board[row][column].IsVisible = true; 

                    }
                    else
                    {
                        board[row].Add(new Piece(row, column, colorpiece.Green, false));

                    }
                }
            }


            return board;
        }

        public static bool isInBounds(int row, int column)
        {
            return row >= 0 && column >= 0 && row < 8 && column < 8;
        }
        public static void initializeNeighboursToBeChecked(Piece piece, HashSet<Tuple<int, int>> neighboursToCheck)
        {
            if (piece.King == true)
            {
                neighboursToCheck.Add(new Tuple<int, int>(-1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(-1, 1));
                neighboursToCheck.Add(new Tuple<int, int>(1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(1, 1));
            }
            else if (piece.Color == colorpiece.Red)
            {
                neighboursToCheck.Add(new Tuple<int, int>(-1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(-1, 1));
            }
            else
            {
                neighboursToCheck.Add(new Tuple<int, int>(1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(1, 1));
            }
        }
    }
}
