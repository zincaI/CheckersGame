using Checkers.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Checkers.Services
{
    internal class GameLogic
    {
        private ObservableCollection<ObservableCollection<Piece>> Board;

        List<int> toColor = new List<int> { };

        public GameLogic()
        {
        }

        public GameLogic(ObservableCollection<ObservableCollection<Piece>> pieces)
        {
            this.Board = pieces;
        }

        public void ClickPiece(int row, int colomn)
        {

            PossibleActions(row, colomn);
        }
        public void PossibleActions(int row, int colomn)
        {
            if (Board[row][colomn].Color == colorpiece.Red)
            {
                if (Board[row][colomn].King == false)
                {
                    if (row != 0)
                    {
                        if (colomn != 0 && colomn != 7)
                        {
                            Board[row - 1][colomn - 1].Color = colorpiece.Green;
                            Board[row - 1][colomn - 1].IsVisible = true;

                            Board[row - 1][colomn + 1].Color = colorpiece.Green;
                            Board[row - 1][colomn + 1].IsVisible = true;


                        }
                        else if (colomn == 7)
                        {
                            Board[row - 1][colomn - 1].Color = colorpiece.Green;
                            Board[row - 1][colomn - 1].IsVisible = true;

                        }
                        else if (colomn == 0)
                        {
                            Board[row - 1][colomn + 1].Color = colorpiece.Green;
                            Board[row - 1][colomn + 1].IsVisible = true;
                        }

                    }

                }
                else
                {
                    if (row != 0)
                    {
                        if (colomn != 0 && colomn != 7)
                        {
                            Board[row - 1][colomn - 1].Color = colorpiece.Green;
                            Board[row - 1][colomn - 1].IsVisible = true;

                            Board[row - 1][colomn + 1].Color = colorpiece.Green;
                            Board[row - 1][colomn + 1].IsVisible = true;


                            Board[row + 1][colomn - 1].Color = colorpiece.Green;
                            Board[row + 1][colomn - 1].IsVisible = true;

                            Board[row + 1][colomn + 1].Color = colorpiece.Green;
                            Board[row + 1][colomn + 1].IsVisible = true;



                        }
                        else if (colomn == 7)
                        {

                            Board[row - 1][colomn - 1].Color = colorpiece.Green;
                            Board[row - 1][colomn - 1].IsVisible = true;

                            Board[row + 1][colomn - 1].Color = colorpiece.Green;
                            Board[row + 1][colomn - 1].IsVisible = true;


                        }
                        else if (colomn == 0)
                        {
                            Board[row - 1][colomn + 1].Color = colorpiece.Green;
                            Board[row - 1][colomn + 1].IsVisible = true;

                            Board[row + 1][colomn + 1].Color = colorpiece.Green;
                            Board[row + 1][colomn + 1].IsVisible = true;
                        }

                    }
                }
            }
            else if (Board[row][colomn].Color == colorpiece.Black)
            {
                if (Board[row][colomn].King == false)
                {
                    if (row != 7)
                    {
                        if (colomn != 0 && colomn != 7)
                        {
                            Board[row + 1][colomn - 1].Color = colorpiece.Green;
                            Board[row + 1][colomn - 1].IsVisible = true;

                            Board[row + 1][colomn + 1].Color = colorpiece.Green;
                            Board[row + 1][colomn + 1].IsVisible = true;
                        }
                        else if (colomn == 7)
                        {
                            Board[row + 1][colomn - 1].Color = colorpiece.Green;
                            Board[row + 1][colomn - 1].IsVisible = true;



                        }
                        else if (colomn == 0)
                        {
                            Board[row + 1][colomn + 1].Color = colorpiece.Green;
                            Board[row + 1][colomn + 1].IsVisible = true;
                        }

                    }

                }
                else
                {
                    if (row != 0)
                    {
                        if (colomn != 0 && colomn != 7)
                        {

                            Board[row - 1][colomn - 1].Color = colorpiece.Green;
                            Board[row - 1][colomn - 1].IsVisible = true;

                            Board[row - 1][colomn + 1].Color = colorpiece.Green;
                            Board[row - 1][colomn + 1].IsVisible = true;

                            Board[row + 1][colomn - 1].Color = colorpiece.Green;
                            Board[row + 1][colomn - 1].IsVisible = true;

                            Board[row + 1][colomn + 1].Color = colorpiece.Green;
                            Board[row + 1][colomn + 1].IsVisible = true;


                        }
                        else if (colomn == 7)
                        {
                            Board[row - 1][colomn - 1].Color = colorpiece.Green;
                            Board[row - 1][colomn - 1].IsVisible = true;

                            Board[row + 1][colomn - 1].Color = colorpiece.Green;
                            Board[row + 1][colomn - 1].IsVisible = true;



                        }
                        else if (colomn == 0)
                        {
                            Board[row - 1][colomn + 1].Color = colorpiece.Green;
                            Board[row - 1][colomn + 1].IsVisible = true;

                            Board[row + 1][colomn + 1].Color = colorpiece.Green;
                            Board[row + 1][colomn + 1].IsVisible = true;


                        }

                    }
                }
            }




        }
    }
}