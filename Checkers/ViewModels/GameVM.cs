using Checkers.Models;
using System.Collections.ObjectModel;
using Checkers.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    internal class GameVM : INotifyPropertyChanged
    {
        private GameLogic gameLogic;
        private ObservableCollection<ObservableCollection<Piece>> _board;

        Piece lastPiece;

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
            }
        }

        public void MovePiece(Piece current, Piece last)
        {
            if (current.row - last.row == 2 && current.column - last.column == 2)
            {
                Board[current.row - 1][current.column - 1].IsVisible = false;
                Board[current.row - 1][current.column - 1].Color = colorpiece.Green;
                Board[current.row - 1][current.column - 1].King = false;
            }
            if (current.row - last.row == 2 && current.column - last.column == -2)
            {
                Board[current.row - 1][current.column + 1].IsVisible = false;
                Board[current.row - 1][current.column + 1].Color = colorpiece.Green;
                Board[current.row - 1][current.column + 1].King = false;
            }
            if (current.row - last.row == -2 && current.column - last.column == 2)
            {
                Board[current.row + 1][current.column - 1].IsVisible = false;
                Board[current.row + 1][current.column - 1].Color = colorpiece.Green;
                Board[current.row + 1][current.column - 1].King = false;
            }
            if (current.row - last.row == -2 && current.column - last.column == -2)
            {
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
                                if (row >= 2 && colomn <= 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row >= 2 && colomn <= 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row >= 2 && colomn <= 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn <= 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
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
                                if (row >= 2 && colomn <= 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn <= 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn <= 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn <= 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row >= 2 && colomn <= 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn <= 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn >= 2 && Board[row + 1][colomn - 1].Color != Board[row][colomn].Color)
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
                                if (row >= 2 && colomn <= 6 && Board[row - 1][colomn + 1].Color != Board[row][colomn].Color)
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
                                if (row <= 6 && colomn <= 6 && Board[row + 1][colomn + 1].Color != Board[row][colomn].Color)
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




    }
}

