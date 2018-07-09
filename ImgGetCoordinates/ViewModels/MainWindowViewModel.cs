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
            get { return _poligonPoints ?? new Point[0]; }
        }

        public string PoligonCoordinatesText
        {
            get { return string.Join(", ", PoligonPoints.Select(x => string.Format("({0},{1})", x.X, x.Y))); }
        }

        public double _x;
        public double X { get { return _x; } set { _x = value; this.RaisePropertyChanged(nameof(X)); } }

        public double _y;
        public double Y { get { return _y; } set { _y = value; this.RaisePropertyChanged(nameof(Y)); } }

        public bool _isSelPoligonCheched;
        public bool IsSelPoligonCheched
        {
            get { return _isSelPoligonCheched; }
            set
            {
                _isSelPoligonCheched = value;
                if (value) { this.PoligonPoints = new Point[0]; }
                this.RaisePropertyChanged(nameof(IsSelPoligonCheched));
            }
        }

        public Bitmap _imgSource = null;
        public Bitmap ImgSource
        {
            get { return _imgSource; }
            set { _imgSource = value; ImgWidth = value.PixelWidth; ImgHeight = value.PixelHeight; this.RaisePropertyChanged(nameof(ImgSource)); }
        }

        public int _imgWidth;
        public int ImgWidth { get { return _imgSource == null ? 0 : _imgWidth; } set { _imgWidth = value; this.RaisePropertyChanged(nameof(ImgWidth)); } }

        public int _imgHeight;
        public int ImgHeight { get { return _imgSource == null ? 0 : _imgHeight; } set { _imgHeight = value; this.RaisePropertyChanged(nameof(ImgHeight)); } }
    }
}
