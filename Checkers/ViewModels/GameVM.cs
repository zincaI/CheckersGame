using Checkers.Models;
using Checkers.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Checkers.Commands;

namespace Checkers.ViewModels
{
    internal class GameVM : INotifyPropertyChanged
    {
        private GameLogic gameLogic;

        private ObservableCollection<ObservableCollection<Piece>> _board;


        private int red;
        private int black;

        public bool multiplejumps = false;




        public GameVM()
        {

            //ObservableCollection<ObservableCollection<Piece>> board = Utility.initBoard();
            gameLogic = new GameLogic(this);
            //Board = board;
            //PieceClickedCommand = new RelayCommand(PieceClicked);

        }
        public int LabelTextRed
        {
            get { return red; }
            set
            {
                if (red != value)
                {
                    red = value;
                    OnPropertyChanged(nameof(LabelTextRed));
                }
            }
        }
        public int LabelTextBlack
        {
            get { return black; }
            set
            {
                if (black != value)
                {
                    black = value;
                    OnPropertyChanged(nameof(LabelTextBlack));
                }
            }
        }

        private ICommand pieceClicked;
        public ICommand PieceClickedCommand
        {

            get
            {
                //MessageBox.Show("ceva");
                if (pieceClicked == null)
                {
                    pieceClicked = new RelayCommand<Piece>(gameLogic.PieceClicked);
                }
                return pieceClicked;
            }
        }


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

        //private bool _isVisible;

        //public bool IsVisible
        //{
        //    get { return _isVisible; }
        //    set
        //    {
        //        if (_isVisible != value)
        //        {
        //            _isVisible = value;
        //            //OnPropertyChanged(nameof(IsVisible));
        //        }
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }






        //private ICommand clickPiece;

        //public ICommand ClickPieceCommand
        //{
        //    get
        //    {
        //        if(clickPiece==null)
        //        {
        //            clickPiece = new RelayCommand();
        //        }
        //        return clickPiece;
        //    }
        //}

        public void SaveClick()
        {
            Utility.SaveGame(Board);

        }

        public void Load_Click()
        {
            Board = Utility.LoadGame();
        }

        public void Statistics_Click()
        {
            (int scoreRed, int maxRed, int scoreBlack, int maxBlack) = ManageGames.ReadStatisticsFromJson();
            MessageBox.Show("Score red:" + scoreRed + "\nmaxRed:" + maxRed + "\nscoreBlack" + scoreBlack + "\nmaxBlack:" + maxBlack);

        }

    }
}

