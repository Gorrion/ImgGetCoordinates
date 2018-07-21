using Avalonia;
using Avalonia.Media.Imaging;
using ReactiveUI;
using System.Linq;

namespace ImgGetCoordinates.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Point[] _poligonPoints = null;
        public Point[] PoligonPoints
        {
            set
            {
                _poligonPoints = value;
                this.RaisePropertyChanged(nameof(PoligonPoints));
                this.RaisePropertyChanged(nameof(PoligonCoordinatesText));

            }
            get => _poligonPoints ?? new Point[0];
        }

        public string PoligonCoordinatesText
        {
            get { return string.Join(", ", PoligonPoints.Select(x => string.Format("({0},{1})", (int)x.X, (int)x.Y))); }
        }

        private int _x;
        public int X { get => _x;
            set { _x = value; this.RaisePropertyChanged(nameof(X)); } }

        private int _y;
        public int Y { get => _y;
            set { _y = value; this.RaisePropertyChanged(nameof(Y)); } }

        private bool _isSelPoligonCheched;
        public bool IsSelPoligonCheched
        {
            get => _isSelPoligonCheched;
            set
            {
                _isSelPoligonCheched = value;
                if (value) { this.PoligonPoints = new Point[0]; }
                this.RaisePropertyChanged(nameof(IsSelPoligonCheched));
            }
        }

        private Bitmap _imgSource = null;
        public Bitmap ImgSource
        {
            get => _imgSource;
            set { _imgSource = value; ImgWidth = value.PixelWidth; ImgHeight = value.PixelHeight; this.RaisePropertyChanged(nameof(ImgSource)); }
        }

        private int _imgWidth;
        public int ImgWidth { get => _imgSource == null ? 0 : _imgWidth;
            set { _imgWidth = value; this.RaisePropertyChanged(nameof(ImgWidth)); } }

        private int _imgHeight;
        public int ImgHeight { get => _imgSource == null ? 0 : _imgHeight;
            set { _imgHeight = value; this.RaisePropertyChanged(nameof(ImgHeight)); } }
    }
}
