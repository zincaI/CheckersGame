using Checkers.Models;
using Checkers.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Checkers.Services
{
    internal class GameLogic
    {
        private GameVM vm;
        bool firstJump = true;

        Piece lastPiece;

        private int redScore = 0;
        private int blackScore = 0;

        colorpiece playerTurn = colorpiece.Black;

        public GameLogic(GameVM gamevm)
        {
            this.vm = gamevm;
            //vm.Board = new ObservableCollection<ObservableCollection<Piece>>();

            vm.Board = Utility.initBoard();
            vm.multiplejumps = true;
        }

        public void PieceClicked(object parameter)
        {

            var piece = parameter as Piece;

            if (firstJump)
            {
                if (piece.Color == playerTurn || piece.Color == colorpiece.Green)
                {

                    for (int i = 0; i < vm.Board.Count; i++)
                    {
                        for (int j = 0; j < vm.Board[i].Count; j++)
                        {
                            if (vm.Board[i][j].Color == colorpiece.Green && vm.Board[i][j].IsVisible == true)
                            {
                                vm.Board[i][j].IsVisible = false;
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
                        lastPiece = piece;
                        if (vm.multiplejumps == true && firstJump == false)
                        {
                            MultipleJumpsActions(piece.row, piece.column);
                        }
                        else
                        {
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
            }
            else
            {
                if (piece != null && piece.Color == colorpiece.Green)
                {
                    MovePiece(piece, lastPiece);


                    MultipleJumpsActions(piece.row, piece.column);
                }
                bool visibleGreenPieces = false;
                for (int i = 0; i < vm.Board.Count; i++)
                {
                    for (int j = 0; j < vm.Board[i].Count; j++)
                    {
                        if (vm.Board[i][j].Color == colorpiece.Green && vm.Board[i][j].IsVisible == true)
                        {
                            visibleGreenPieces = true;
                            break;
                        }
                    }
                }

                if (!visibleGreenPieces)
                {
                    if (playerTurn == colorpiece.Black)
                    {
                        playerTurn = colorpiece.Red;
                    }
                    else
                    {
                        playerTurn = colorpiece.Black;
                    }
                    firstJump = true;




                }
                for (int i = 0; i < vm.Board.Count; i++)
                {
                    for (int j = 0; j < vm.Board[i].Count; j++)
                    {
                        if (vm.Board[i][j].Color == colorpiece.Green && vm.Board[i][j].IsVisible == true)
                        {
                            vm.Board[i][j].IsVisible = false;
                        }
                    }
                }
            }
            vm.Board = new ObservableCollection<ObservableCollection<Piece>>(vm.Board);

        }
        public void EndGame()
        {
            if (redScore == 12)
            {
                MessageBox.Show("A castigat rosul");
                (int scoreRed, int maxRed, int scoreBlack, int maxBlack) = ManageGames.ReadStatisticsFromJson();
                scoreRed++;
                if (maxRed < 12 - blackScore)
                {
                    maxRed = 12 - blackScore;
                }
                ManageGames.WriteStatisticsToJson((scoreRed, maxRed, scoreBlack, maxBlack));
            }
            else if (blackScore == 12)
            {
                MessageBox.Show("A castigat negru");
                (int scoreRed, int maxRed, int scoreBlack, int maxBlack) = ManageGames.ReadStatisticsFromJson();
                scoreBlack++;
                if (maxBlack < 12 - redScore)
                {
                    maxBlack = 12 - redScore;
                }
                ManageGames.WriteStatisticsToJson((scoreRed, maxRed, scoreBlack, maxBlack));
            }
            vm.Board.Clear();
            vm.Board = Utility.initBoard();
            playerTurn = colorpiece.Black;
            redScore = 0;
            blackScore = 0;
            vm.LabelTextBlack = blackScore;
            vm.LabelTextRed = redScore;

        }


        public void MovePiece(Piece current, Piece last)
        {
            if (current.row - last.row == 2 && current.column - last.column == 2)
            {
                if (vm.Board[current.row - 1][current.column - 1].Color == colorpiece.Red)
                {
                    blackScore++;
                    vm.LabelTextBlack = blackScore;

                }
                else if (vm.Board[current.row - 1][current.column - 1].Color == colorpiece.Black)
                {
                    redScore++;

                    vm.LabelTextRed = redScore;
                }
                vm.Board[current.row - 1][current.column - 1].IsVisible = false;
                vm.Board[current.row - 1][current.column - 1].Color = colorpiece.Green;
                vm.Board[current.row - 1][current.column - 1].King = false;
                firstJump = false;

            }
            if (current.row - last.row == 2 && current.column - last.column == -2)
            {
                if (vm.Board[current.row - 1][current.column + 1].Color == colorpiece.Red)
                {
                    blackScore++;
                    vm.LabelTextBlack = blackScore;

                }
                else if (vm.Board[current.row - 1][current.column + 1].Color == colorpiece.Black)
                {
                    redScore++;

                    vm.LabelTextRed = redScore;
                }
                vm.Board[current.row - 1][current.column + 1].IsVisible = false;
                vm.Board[current.row - 1][current.column + 1].Color = colorpiece.Green;
                vm.Board[current.row - 1][current.column + 1].King = false;
                firstJump = false;
            }
            if (current.row - last.row == -2 && current.column - last.column == 2)
            {
                if (vm.Board[current.row + 1][current.column - 1].Color == colorpiece.Red)
                {
                    blackScore++;
                    vm.LabelTextBlack = blackScore;

                }
                else if (vm.Board[current.row + 1][current.column - 1].Color == colorpiece.Black)
                {
                    redScore++;

                    vm.LabelTextRed = redScore;
                }
                vm.Board[current.row + 1][current.column - 1].IsVisible = false;
                vm.Board[current.row + 1][current.column - 1].Color = colorpiece.Green;
                vm.Board[current.row + 1][current.column - 1].King = false;
                firstJump = false;
            }
            if (current.row - last.row == -2 && current.column - last.column == -2)
            {
                if (vm.Board[current.row + 1][current.column + 1].Color == colorpiece.Red)
                {
                    blackScore++;
                    vm.LabelTextBlack = blackScore;

                }
                else if (vm.Board[current.row + 1][current.column + 1].Color == colorpiece.Black)
                {
                    redScore++;

                    vm.LabelTextRed = redScore;
                }
                vm.Board[current.row + 1][current.column + 1].IsVisible = false;
                vm.Board[current.row + 1][current.column + 1].Color = colorpiece.Green;
                vm.Board[current.row + 1][current.column + 1].King = false;
                firstJump = false;
            }



            vm.Board[current.row][current.column].IsVisible = true;
            vm.Board[current.row][current.column].Color = lastPiece.Color;
            vm.Board[current.row][current.column].King = last.King;

            vm.Board[last.row][last.column].IsVisible = false;
            vm.Board[last.row][last.column].Color = colorpiece.Green;
            vm.Board[last.row][last.column].King = false;




            for (int i = 0; i < vm.Board.Count; i++)
            {
                for (int j = 0; j < vm.Board[i].Count; j++)
                {
                    if (vm.Board[i][j].Color == colorpiece.Green && vm.Board[i][j].IsVisible == true)
                    {
                        vm.Board[i][j].IsVisible = false;
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


            if (redScore == 12 || blackScore == 12)
            {
                EndGame();
            }

        }
        public void PossibleActions(int row, int colomn)
        {

            if (vm.Board[row][colomn].Color == colorpiece.Black)
            {
                if (vm.Board[row][colomn].King == false)
                {
                    if (row != 0)
                    {
                        if (colomn != 0 && colomn != 7)
                        {
                            if (vm.Board[row - 1][colomn - 1].IsVisible != true)
                            {
                                vm.Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn - 1].IsVisible = true;

                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2)
                                    if (vm.Board[row - 2][colomn - 2].IsVisible != true && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    {
                                        vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }
                            if (vm.Board[row - 1][colomn + 1].IsVisible != true)
                            {
                                {

                                    vm.Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                    vm.Board[row - 1][colomn + 1].IsVisible = true;
                                }

                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn + 2].IsVisible != true && vm.Board[row - 2][colomn + 2].Color != vm.Board[row][colomn].Color)
                                    {
                                        vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        }
                        else if (colomn == 7)
                        {
                            if (vm.Board[row - 1][colomn - 1].IsVisible != true)
                            {
                                vm.Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn - 1].IsVisible = true;

                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2 && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }

                        }
                        else if (colomn == 0)
                        {
                            if (vm.Board[row - 1][colomn + 1].IsVisible != true)
                            {
                                vm.Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn + 2].IsVisible = true;

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
                            if (vm.Board[row - 1][colomn - 1].IsVisible != true)
                            {
                                vm.Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2 && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 0)
                            if (vm.Board[row - 1][colomn + 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][colomn - 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][colomn + 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn + 2].IsVisible = true;

                                    }
                            }



                    }

                    else if (colomn == 7)
                    {
                        if (row != 0)

                            if (vm.Board[row - 1][colomn - 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2 && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][colomn - 1].IsVisible != true && row != 7)
                            {

                                vm.Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }


                    }
                    else if (colomn == 0)
                    {
                        if (row != 0)

                            if (vm.Board[row - 1][colomn + 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][colomn + 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn + 2].IsVisible = true;

                                    }
                            }
                    }


                }

            }
            else if (vm.Board[row][colomn].Color == colorpiece.Red)
            {

                if (vm.Board[row][colomn].King == false)
                {
                    if (row != 7)
                    {
                        if (colomn != 0 && colomn != 7)
                        {
                            if (vm.Board[row + 1][colomn - 1].IsVisible != true)
                            {

                                vm.Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }
                            if (vm.Board[row + 1][colomn + 1].IsVisible != true)
                            {

                                vm.Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        }
                        else if (colomn == 7)
                        {
                            if (vm.Board[row + 1][colomn - 1].IsVisible != true)
                            {

                                vm.Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }


                        }
                        else if (colomn == 0)
                        {
                            if (vm.Board[row + 1][colomn + 1].IsVisible != true)
                            {

                                vm.Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn + 2].IsVisible = true;

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

                            if (vm.Board[row - 1][colomn - 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2 && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 0)

                            if (vm.Board[row - 1][colomn + 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][colomn - 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][colomn + 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn + 2].IsVisible = true;

                                    }
                            }


                    }
                    else if (colomn == 7)
                    {
                        if (row != 0)

                            if (vm.Board[row - 1][colomn - 1].IsVisible != true && row != 0)
                            {

                                vm.Board[row - 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn >= 2 && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][colomn - 1].IsVisible != true && row != 7)
                            {

                                vm.Board[row + 1][colomn - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn - 2].IsVisible = true;

                                    }
                            }



                    }
                    else if (colomn == 0)
                    {
                        if (row != 0)

                            if (vm.Board[row - 1][colomn + 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row - 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row - 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][colomn + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][colomn + 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][colomn + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][colomn + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color)
                                    if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][colomn + 2].IsVisible = true;


                                    }
                            }


                    }


                }

            }

            //OnPropertyChanged(nameof(vm.Board));
            //vm.Board = vm.Board;



        }







        public void MultipleJumpsActions(int row, int colomn)
        {

            if (vm.Board[row][colomn].Color == colorpiece.Black)
            {
                if (vm.Board[row][colomn].King == false)
                {
                    if (row != 0)
                    {
                        if (colomn != 0 && colomn != 7)
                        {

                            if (row >= 2 && colomn >= 2)
                                if (vm.Board[row - 2][colomn - 2].IsVisible != true && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn - 1].Color != colorpiece.Green)
                                {
                                    vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn - 2].IsVisible = true;

                                }


                            if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn + 2].IsVisible != true && vm.Board[row - 2][colomn + 2].Color != vm.Board[row][colomn].Color)
                                {
                                    vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn + 2].IsVisible = true;

                                }

                        }
                        else if (colomn == 7)
                        {

                            if (row >= 2 && colomn >= 2 && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn - 2].IsVisible = true;

                                }


                        }
                        else if (colomn == 0)
                        {

                            if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn + 2].IsVisible = true;

                                }


                        }



                    }
                }
                else
                {


                    if (colomn != 0 && colomn != 7)
                    {
                        if (row != 0)

                            if (row >= 2 && colomn >= 2 && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn - 2].IsVisible = true;

                                }

                        if (row != 0)

                            if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn + 2].IsVisible = true;

                                }

                        if (row != 7)


                            if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn - 2].IsVisible = true;

                                }

                        if (row != 7)


                            if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn + 2].IsVisible = true;

                                }




                    }

                    else if (colomn == 7)
                    {
                        if (row != 0)


                            if (row >= 2 && colomn >= 2 && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn - 2].IsVisible = true;

                                }

                        if (row != 7)

                            if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn - 2].IsVisible = true;

                                }



                    }
                    else if (colomn == 0)
                    {
                        if (row != 0)

                            if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn + 2].IsVisible = true;

                                }

                        if (row != 7)

                            if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn + 2].IsVisible = true;

                                }

                    }


                }

            }
            else if (vm.Board[row][colomn].Color == colorpiece.Red)
            {

                if (vm.Board[row][colomn].King == false)
                {
                    if (row != 7)
                    {
                        if (colomn != 0 && colomn != 7)
                        {

                            if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn - 2].IsVisible = true;

                                }


                            if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn + 2].IsVisible = true;

                                }

                        }
                        else if (colomn == 7)
                        {

                            if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn - 2].IsVisible = true;

                                }



                        }
                        else if (colomn == 0)
                        {


                            if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn + 2].IsVisible = true;

                                }

                        }

                    }

                }
                else
                {

                    if (colomn != 0 && colomn != 7)
                    {
                        if (row != 0)


                            if (row >= 2 && colomn >= 2 && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn - 2].IsVisible = true;

                                }

                        if (row != 0)



                            if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn + 2].IsVisible = true;

                                }

                        if (row != 7)

                            if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn - 2].IsVisible = true;

                                }

                        if (row != 7)


                            if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn + 2].IsVisible = true;

                                }



                    }
                    else if (colomn == 7)
                    {
                        if (row != 0)


                            if (row >= 2 && colomn >= 2 && vm.Board[row - 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn - 2].IsVisible = true;

                                }

                        if (row != 7)


                            if (row < 6 && colomn >= 2 && vm.Board[row + 1][colomn - 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn - 2].IsVisible = true;

                                }




                    }
                    else if (colomn == 0)
                    {
                        if (row != 0)


                            if (row >= 2 && colomn < 6 && vm.Board[row - 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row - 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][colomn + 2].IsVisible = true;

                                }

                        if (row != 7)


                            if (row < 6 && colomn < 6 && vm.Board[row + 1][colomn + 1].Color != vm.Board[row][colomn].Color && vm.Board[row + 1][colomn + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][colomn + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][colomn + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][colomn + 2].IsVisible = true;

                                }



                    }

                }
            }
        }




    }


}