using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    internal class RoundInfo
    {
        public ObservableCollection<ObservableCollection<Piece>> Board { get; set; }
        public string MultipleJumpsAllowed { get; set; }
        public string TurnData { get; set; }

    }
}
