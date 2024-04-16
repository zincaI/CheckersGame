using Checkers.Models;
using Checkers.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace Checkers.Services
{
    internal class GameLogic
    {
        private GameVM vm;
        bool firstJump = true;

        Piece lastPiece;

        public int redScore;
        public int blackScore;

        public colorpiece playerTurn = colorpiece.Black;

        public GameLogic(GameVM gamevm)
        {
            this.vm = gamevm;

            vm.Board = Utility.initBoard();
            vm.Multiplejumps = ManageGames.ReadFromFileAndConvertToBool("mutiple.txt");
            redScore =gamevm.LabelTextRed;
            blackScore =gamevm.LabelTextBlack;
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
                        if (vm.Multiplejumps == true && firstJump == false)
                        {
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

                        if (visibleGreenPieces == false)
                        {
                            if (playerTurn == colorpiece.Black)
                            {
                                playerTurn = colorpiece.Red;
                                vm.LabelTurn = "Red player to move";

                            }
                            else
                            {
                                playerTurn = colorpiece.Black;
                                vm.LabelTurn = "Black  player to move";

                            }
                            firstJump = true;



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

                if (visibleGreenPieces == false)
                {
                    if (playerTurn == colorpiece.Black)
                    {
                        playerTurn = colorpiece.Red;
                        vm.LabelTurn = "Red player to move";

                    }
                    else
                    {
                        playerTurn = colorpiece.Black;
                        vm.LabelTurn = "Black  player to move";

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
            vm.Block = true;
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
            ManageGames.EnterInFile(vm.Multiplejumps);
        }


        public void MovePiece(Piece current, Piece last)
        {
            ManageGames.EnterInFile(vm.Multiplejumps);
            vm.Block = false;
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
        public void PossibleActions(int row, int column)
        {

            if (vm.Board[row][column].Color == colorpiece.Black)
            {
                if (vm.Board[row][column].King == false)
                {
                    if (row != 0)
                    {
                        if (column != 0 && column != 7)
                        {
                            if (vm.Board[row - 1][column - 1].IsVisible != true)
                            {
                                vm.Board[row - 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column - 1].IsVisible = true;

                            }
                            else
                            {
                                if (row >= 2 && column >= 2)
                                    if (vm.Board[row - 2][column - 2].IsVisible != true && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color)
                                    {
                                        vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column - 2].IsVisible = true;

                                    }
                            }
                            if (vm.Board[row - 1][column + 1].IsVisible != true)
                            {
                                {

                                    vm.Board[row - 1][column + 1].Color = colorpiece.Green;
                                    vm.Board[row - 1][column + 1].IsVisible = true;
                                }

                            }
                            else
                            {
                                if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column + 2].IsVisible != true && vm.Board[row - 2][column + 2].Color != vm.Board[row][column].Color)
                                    {
                                        vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column + 2].IsVisible = true;

                                    }
                            }
                        }
                        else if (column == 7)
                        {
                            if (vm.Board[row - 1][column - 1].IsVisible != true)
                            {
                                vm.Board[row - 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column - 1].IsVisible = true;

                            }
                            else
                            {
                                if (row >= 2 && column >= 2 && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column - 2].IsVisible = true;

                                    }
                            }

                        }
                        else if (column == 0)
                        {
                            if (vm.Board[row - 1][column + 1].IsVisible != true)
                            {
                                vm.Board[row - 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column + 2].IsVisible = true;

                                    }
                            }

                        }



                    }
                }
                else
                {


                    if (column != 0 && column != 7)
                    {
                        if (row != 0)
                            if (vm.Board[row - 1][column - 1].IsVisible != true)
                            {
                                vm.Board[row - 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && column >= 2 && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column - 2].IsVisible = true;

                                    }
                            }
                        if (row != 0)
                            if (vm.Board[row - 1][column + 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][column - 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][column + 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column + 2].IsVisible = true;

                                    }
                            }



                    }

                    else if (column == 7)
                    {
                        if (row != 0)

                            if (vm.Board[row - 1][column - 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && column >= 2 && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][column - 1].IsVisible != true && row != 7)
                            {

                                vm.Board[row + 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column - 2].IsVisible = true;

                                    }
                            }


                    }
                    else if (column == 0)
                    {
                        if (row != 0)

                            if (vm.Board[row - 1][column + 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][column + 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column + 2].IsVisible = true;

                                    }
                            }
                    }


                }

            }
            else if (vm.Board[row][column].Color == colorpiece.Red)
            {

                if (vm.Board[row][column].King == false)
                {
                    if (row != 7)
                    {
                        if (column != 0 && column != 7)
                        {
                            if (vm.Board[row + 1][column - 1].IsVisible != true)
                            {

                                vm.Board[row + 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column - 2].IsVisible = true;

                                    }
                            }
                            if (vm.Board[row + 1][column + 1].IsVisible != true)
                            {

                                vm.Board[row + 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column + 2].IsVisible = true;

                                    }
                            }
                        }
                        else if (column == 7)
                        {
                            if (vm.Board[row + 1][column - 1].IsVisible != true)
                            {

                                vm.Board[row + 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column - 2].IsVisible = true;

                                    }
                            }


                        }
                        else if (column == 0)
                        {
                            if (vm.Board[row + 1][column + 1].IsVisible != true)
                            {

                                vm.Board[row + 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column + 2].IsVisible = true;

                                    }
                            }
                        }

                    }

                }
                else
                {

                    if (column != 0 && column != 7)
                    {
                        if (row != 0)

                            if (vm.Board[row - 1][column - 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && column >= 2 && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column - 2].IsVisible = true;

                                    }
                            }
                        if (row != 0)

                            if (vm.Board[row - 1][column + 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][column - 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][column + 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column + 2].IsVisible = true;

                                    }
                            }


                    }
                    else if (column == 7)
                    {
                        if (row != 0)

                            if (vm.Board[row - 1][column - 1].IsVisible != true && row != 0)
                            {

                                vm.Board[row - 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && column >= 2 && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column - 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][column - 1].IsVisible != true && row != 7)
                            {

                                vm.Board[row + 1][column - 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column - 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column - 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column - 2].IsVisible = true;

                                    }
                            }



                    }
                    else if (column == 0)
                    {
                        if (row != 0)

                            if (vm.Board[row - 1][column + 1].IsVisible != true && row != 0)
                            {
                                vm.Board[row - 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row - 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row - 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row - 2][column + 2].IsVisible = true;

                                    }
                            }
                        if (row != 7)

                            if (vm.Board[row + 1][column + 1].IsVisible != true && row != 7)
                            {
                                vm.Board[row + 1][column + 1].Color = colorpiece.Green;
                                vm.Board[row + 1][column + 1].IsVisible = true;
                            }
                            else
                            {
                                if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color)
                                    if (vm.Board[row + 2][column + 2].IsVisible != true)
                                    {
                                        vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                        vm.Board[row + 2][column + 2].IsVisible = true;


                                    }
                            }


                    }


                }

            }


        }

        public void MultipleJumpsActions(int row, int column)
        {

            if (vm.Board[row][column].Color == colorpiece.Black)
            {
                if (vm.Board[row][column].King == false)
                {
                    if (row != 0)
                    {
                        if (column != 0 && column != 7)
                        {

                            if (row >= 2 && column >= 2)
                                if (vm.Board[row - 2][column - 2].IsVisible != true && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column - 1].Color != colorpiece.Green)
                                {
                                    vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column - 2].IsVisible = true;

                                }


                            if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column + 2].IsVisible != true && vm.Board[row - 2][column + 2].Color != vm.Board[row][column].Color)
                                {
                                    vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column + 2].IsVisible = true;

                                }

                        }
                        else if (column == 7)
                        {

                            if (row >= 2 && column >= 2 && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column - 2].IsVisible = true;

                                }


                        }
                        else if (column == 0)
                        {

                            if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column + 2].IsVisible = true;

                                }


                        }



                    }
                }
                else
                {


                    if (column != 0 && column != 7)
                    {
                        if (row != 0)

                            if (row >= 2 && column >= 2 && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column - 2].IsVisible = true;

                                }

                        if (row != 0)

                            if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column + 2].IsVisible = true;

                                }

                        if (row != 7)


                            if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column - 2].IsVisible = true;

                                }

                        if (row != 7)


                            if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column + 2].IsVisible = true;

                                }




                    }

                    else if (column == 7)
                    {
                        if (row != 0)


                            if (row >= 2 && column >= 2 && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column - 2].IsVisible = true;

                                }

                        if (row != 7)

                            if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column - 2].IsVisible = true;

                                }



                    }
                    else if (column == 0)
                    {
                        if (row != 0)

                            if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column + 2].IsVisible = true;

                                }

                        if (row != 7)

                            if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column + 2].IsVisible = true;

                                }

                    }


                }

            }
            else if (vm.Board[row][column].Color == colorpiece.Red)
            {

                if (vm.Board[row][column].King == false)
                {
                    if (row != 7)
                    {
                        if (column != 0 && column != 7)
                        {

                            if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column - 2].IsVisible = true;

                                }


                            if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column + 2].IsVisible = true;

                                }

                        }
                        else if (column == 7)
                        {

                            if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column - 2].IsVisible = true;

                                }



                        }
                        else if (column == 0)
                        {


                            if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column + 2].IsVisible = true;

                                }

                        }

                    }

                }
                else
                {

                    if (column != 0 && column != 7)
                    {
                        if (row != 0)


                            if (row >= 2 && column >= 2 && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column - 2].IsVisible = true;

                                }

                        if (row != 0)



                            if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column + 2].IsVisible = true;

                                }

                        if (row != 7)

                            if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column - 2].IsVisible = true;

                                }

                        if (row != 7)


                            if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column + 2].IsVisible = true;

                                }



                    }
                    else if (column == 7)
                    {
                        if (row != 0)


                            if (row >= 2 && column >= 2 && vm.Board[row - 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column - 2].IsVisible = true;

                                }

                        if (row != 7)


                            if (row < 6 && column >= 2 && vm.Board[row + 1][column - 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column - 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column - 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column - 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column - 2].IsVisible = true;

                                }




                    }
                    else if (column == 0)
                    {
                        if (row != 0)


                            if (row >= 2 && column < 6 && vm.Board[row - 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row - 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row - 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row - 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row - 2][column + 2].IsVisible = true;

                                }

                        if (row != 7)


                            if (row < 6 && column < 6 && vm.Board[row + 1][column + 1].Color != vm.Board[row][column].Color && vm.Board[row + 1][column + 1].Color != colorpiece.Green)
                                if (vm.Board[row + 2][column + 2].IsVisible != true)
                                {
                                    vm.Board[row + 2][column + 2].Color = colorpiece.Green;
                                    vm.Board[row + 2][column + 2].IsVisible = true;

                                }



                    }

                }
            }
        }




    }


}