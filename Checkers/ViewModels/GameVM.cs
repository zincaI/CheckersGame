using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Checkers.Models;
using Checkers.Services;

namespace Checkers.ViewModels
{
    internal class GameVM : INotifyPropertyChanged
    {
        private GameLogic gameLogic;
        private ObservableCollection<ObservableCollection<Piece>> _board;

        Piece lastPiece;

        private int redScore = 0;
        private int blackScore = 0;

        colorpiece playerTurn = colorpiece.Black;

        public int LabelTextRed
        {
            get { return redScore; }
            set
            {
                if (redScore != value)
                {
                    redScore = value;
                    OnPropertyChanged(nameof(LabelTextRed));
                }
            }
        }
        public int LabelTextBlack
        {
            get { return blackScore; }
            set
            {
                if (blackScore != value)
                {
                    blackScore = value;
                    OnPropertyChanged(nameof(LabelTextBlack));
                }
            }
        }


        public ICommand PieceClickedCommand { get; }


        public ObservableCollection<ObservableCollection<Piece>> Board
        {
            get { return _board; }
            set
            {
                if (_board != value)
                {
                    _board = value;
                    OnPropertyChanged(nameof(Board));
                }
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    //OnPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }


        public GameVM()
        {

            ObservableCollection<ObservableCollection<Piece>> board = Utility.initBoard();
            gameLogic = new GameLogic(board);
            Board = board;
            PieceClickedCommand = new RelayCommand(PieceClicked);

        }

        private void PieceClicked(object parameter)
        {
            var piece = parameter as Piece;
            if (piece.Color == playerTurn || piece.Color == colorpiece.Green)
            {

                for (int i = 0; i < Board.Count; i++)
                {
                    for (int j = 0; j < Board[i].Count; j++)
                    {
                        if (Board[i][j].Color == colorpiece.Green && Board[i][j].IsVisible == true)
                        {
                            Board[i][j].IsVisible = false;
                        }
                    }
                }
                if (piece != null && piece.Color != colorpiece.Green)
                {
                    lastPiece = piece;
                    PossibleActions(piece.row, piece.column);
                }
                else
                {
                    MovePiece(piece, lastPiece);
                    if (playerTurn == colorpiece.Black)
                    {
                        playerTurn = colorpiece.Red;
                    }
                    else
                    {
                        playerTurn = colorpiece.Black;
                    }
                }
            }
        }
        public void EndGame()
        {
            if (LabelTextRed == 12)
            {
                MessageBox.Show("A castigat rosul");

            }
            else if (LabelTextBlack == 12)
            {
                MessageBox.Show("A castigat negru");
            }
            Board.Clear();
            Board = Utility.initBoard();
            playerTurn = colorpiece.Black;
            LabelTextBlack = 0;
            LabelTextRed = 0;

        }
        public void MovePiece(Piece current, Piece last)
        {
            if (current.row - last.row == 2 && current.column - last.column == 2)
            {
                if (Board[current.row - 1][current.column - 1].Color == colorpiece.Red)
                {
                    LabelTextBlack++;
                }
                else if (Board[current.row - 1][current.column - 1].Color == colorpiece.Black)
                {
                    LabelTextRed++;
                }
                Board[current.row - 1][current.column - 1].IsVisible = false;
                Board[current.row - 1][current.column - 1].Color = colorpiece.Green;
                Board[current.row - 1][current.column - 1].King = false;

            }
            if (current.row - last.row == 2 && current.column - last.column == -2)
            {
                if (Board[current.row - 1][current.column + 1].Color == colorpiece.Red)
                {
                    LabelTextBlack++;
                }
                else if (Board[current.row - 1][current.column + 1].Color == colorpiece.Black)
                {
                    LabelTextRed++;
                }
                Board[current.row - 1][current.column + 1].IsVisible = false;
                Board[current.row - 1][current.column + 1].Color = colorpiece.Green;
                Board[current.row - 1][current.column + 1].King = false;
            }
            if (current.row - last.row == -2 && current.column - last.column == 2)
            {
                if (Board[current.row + 1][current.column - 1].Color == colorpiece.Red)
                {
                    LabelTextBlack++;
                }
                else if (Board[current.row + 1][current.column - 1].Color == colorpiece.Black)
                {
                    LabelTextRed++;
                }
                Board[current.row + 1][current.column - 1].IsVisible = false;
                Board[current.row + 1][current.column - 1].Color = colorpiece.Green;
                Board[current.row + 1][current.column - 1].King = false;
            }
            if (current.row - last.row == -2 && current.column - last.column == -2)
            {
                if (Board[current.row + 1][current.column + 1].Color == colorpiece.Red)
                {
                    LabelTextBlack++;
                }
                else if (Board[current.row + 1][current.column + 1].Color == colorpiece.Black)
                {
                    LabelTextRed++;
                }
                Board[current.row + 1][current.column + 1].IsVisible = false;
                Board[current.row + 1][current.column + 1].Color = colorpiece.Green;
                Board[current.row + 1][current.column + 1].King = false;
            }



            Board[current.row][current.column].IsVisible = true;
            Board[current.row][current.column].Color = lastPiece.Color;
            Board[current.row][current.column].King = last.King;

            Board[last.row][last.column].IsVisible = false;
            Board[last.row][last.column].Color = colorpiece.Green;
            Board[last.row][last.column].King = false;




            for (int i = 0; i < Board.Count; i++)
            {
                for (int j = 0; j < Board[i].Count; j++)
                {
                    if (Board[i][j].Color == colorpiece.Green && Board[i][j].IsVisible == true)
                    {
                        Board[i][j].IsVisible = false;
                    }
                }
            }
            if (current.Color == colorpiece.Black && current.row == 0)
            {
                current.King = true;
            }
            if (current.Color == colorpiece.Red && current.row == 7)
            {
                current.King = true;
            }

            Board = new ObservableCollection<ObservableCollection<Piece>>(Board);

            OnPropertyChanged(nameof(Board));

            if (LabelTextRed == 12 || LabelTextBlack == 12)
            {
                EndGame();
            }

        }
        public void PossibleActions(int row, int colomn)
        {

            if (Board[row][colomn].Color == colorpiece.Black)
            {
                if (Board[row][colomn].King == false)
                {
                    if (row != 0)
                    {
                        if (colomn != 0 && colomn != 7)
                        {
                            if (Board[row - 1][colomn - 1].IsVisible != true)
                            {
                                Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                Board[row - 1][colomn - 1].IsVisible = true;

                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2)
                                    if (Board[row - 2][colomn - 2].IsVisible != true && Board[row - 1][colomn - 1].Color != Board[row][colomn].Color)
                                    {
                                        Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }
                            if (Board[row - 1][colomn + 1].IsVisible != true)
                            {
                                {

                                    Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                    Board[row - 1][colomn + 1].IsVisible = true;
                                }

                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn + 2].IsVisible != true && Board[row - 2][colomn + 2].Color != Board[row][colomn].Color)
                                    {
                                        Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        }
                        else if (colomn == 7)
                        {
                            if (Board[row - 1][colomn - 1].IsVisible != true)
                            {
                                Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                Board[row - 1][colomn - 1].IsVisible = true;

                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2 && Board[row - 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }

                        }
                        else if (colomn == 0)
                        {
                            if (Board[row - 1][colomn + 1].IsVisible != true)
                            {
                                Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                Board[row - 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }

                        }



                    }
                }
                else
                {


                    if (colomn != 0 && colomn != 7)
                    {
                        if (row != 0)
                            if (Board[row - 1][colomn - 1].IsVisible != true)
                            {
                                Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                Board[row - 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2 && Board[row - 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 0)
                            if (Board[row - 1][colomn + 1].IsVisible != true && row != 0)
                            {
                                Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                Board[row - 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (Board[row + 1][colomn - 1].IsVisible != true && row != 7)
                            {
                                Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (Board[row + 1][colomn + 1].IsVisible != true && row != 7)
                            {
                                Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn + 2].IsVisible = true;

                                    }
                            }



                    }

                    else if (colomn == 7)
                    {
                        if (row != 0)

                            if (Board[row - 1][colomn - 1].IsVisible != true && row != 0)
                            {
                                Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                Board[row - 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2 && Board[row - 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (Board[row + 1][colomn - 1].IsVisible != true && row != 7)
                            {

                                Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }


                    }
                    else if (colomn == 0)
                    {
                        if (row != 0)

                            if (Board[row - 1][colomn + 1].IsVisible != true && row != 0)
                            {
                                Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                Board[row - 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (Board[row + 1][colomn + 1].IsVisible != true && row != 7)
                            {
                                Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn + 2].IsVisible = true;

                                    }
                            }
                    }


                }

            }
            else if (Board[row][colomn].Color == colorpiece.Red)
            {

                if (Board[row][colomn].King == false)
                {
                    if (row != 7)
                    {
                        if (colomn != 0 && colomn != 7)
                        {
                            if (Board[row + 1][colomn - 1].IsVisible != true)
                            {

                                Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }
                            if (Board[row + 1][colomn + 1].IsVisible != true)
                            {

                                Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        }
                        else if (colomn == 7)
                        {
                            if (Board[row + 1][colomn - 1].IsVisible != true)
                            {

                                Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }


                        }
                        else if (colomn == 0)
                        {
                            if (Board[row + 1][colomn + 1].IsVisible != true)
                            {

                                Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        }

                    }

                }
                else
                {

                    if (colomn != 0 && colomn != 7)
                    {
                        if (row != 0)

                            if (Board[row - 1][colomn - 1].IsVisible != true && row != 0)
                            {
                                Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                Board[row - 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2 && Board[row - 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 0)

                            if (Board[row - 1][colomn + 1].IsVisible != true && row != 0)
                            {
                                Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                Board[row - 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (Board[row + 1][colomn - 1].IsVisible != true && row != 7)
                            {
                                Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (Board[row + 1][colomn + 1].IsVisible != true && row != 7)
                            {
                                Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn + 2].IsVisible = true;

                                    }
                            }


                    }
                    else if (colomn == 7)
                    {
                        if (row != 0)

                            if (Board[row - 1][colomn - 1].IsVisible != true && row != 0)
                            {

                                Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                Board[row - 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2 && Board[row - 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (Board[row + 1][colomn - 1].IsVisible != true && row != 7)
                            {

                                Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }



                    }
                    else if (colomn == 0)
                    {
                        if (row != 0)

                            if (Board[row - 1][colomn + 1].IsVisible != true && row != 0)
                            {
                                Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                Board[row - 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row - 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (Board[row + 1][colomn + 1].IsVisible != true && row != 7)
                            {
                                Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
                                    if (Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        Board[row + 2][colomn + 2].IsVisible = true;

                                    }
                            }


                    }

                }

            }
            Board = new ObservableCollection<ObservableCollection<Piece>>(Board);

            OnPropertyChanged(nameof(Board));


        }

        private Piece[,] ConvertBoardToMatrix(ObservableCollection<ObservableCollection<Piece>> board)
        {
            int rows = board.Count;
            int columns = board.FirstOrDefault()?.Count ?? 0;

            Piece[,] matrix = new Piece[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = board[i][j];
                }
            }

            return matrix;
        }


        public void SaveClick()
        {
            Utility.SaveGame(Board);

        }

        public void Load_Click()
        {
            Board = Utility.LoadGame();
        }




    }
}

