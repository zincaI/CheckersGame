using System.ComponentModel;
using System.Windows.Media;

namespace Checkers.Models
{
    class Piece : BaseNotification
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private colorpiece color;
        bool king = false;
        public int row { get; set; }
        public int column { get; set; }

        private bool _isVisible;

        private SolidColorBrush _textColor;

        public SolidColorBrush TextColor
        {
            get { return _textColor; }
            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    OnPropertyChanged(nameof(TextColor));
                }
            }
        }



        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged("IsVisible");

                }
            }
        }
        public colorpiece Color
        {
            get { return color; }
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged("Color");
                }
            }
        }
        public Piece(int row, int column, colorpiece v1, bool v2)
        {
            this.row = row;
            this.column = column;

            this.color = v1;
            this.king = v2;
        }

        public bool King
        {
            get { return king; }
            set
            {
                king = value;
                //NotifyPropertyChanged("King");
                OnPropertyChanged("King");
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
