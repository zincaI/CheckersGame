using Checkers.Models;
//using Checkers.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}