using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Diagnostics.ViewModels;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using System.Collections.Generic;
using System.Linq;

namespace ImgGetCoordinates
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class MainWindowModel : ViewModelBase
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
            public int ImgWidth { get { return _imgWidth; } set { _imgWidth = value; this.RaisePropertyChanged(nameof(ImgWidth)); } }

            public int _imgHeight;
            public int ImgHeight { get { return _imgHeight; } set { _imgHeight = value; this.RaisePropertyChanged(nameof(ImgHeight)); } }
        }

        private MainWindowModel Context = new MainWindowModel();
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            var cnvDrawArea = this.Find<Canvas>("CnvDrawArea");
            cnvDrawArea.PointerPressed += OnCnvDrawAreaPressed;
            cnvDrawArea.PointerMoved += OnDrawAreaPreviewMouseMove;
            this.Find<Button>("BtnSelImg").Click += OnBtnSelImgClicked;
            this.Find<Button>("BtnPointDel").Click += OnBtnPointDelClicked;
            this.Find<Button>("BtnPointsClear").Click += OnBtnPointsClearClicked;
            this.DataContext = Context;
        }

        private void OnBtnPointDelClicked(object sender, RoutedEventArgs e)
        {
            if (Context.PoligonPoints.Length > 0)
            {
                var newLst = Context.PoligonPoints.ToList();
                newLst.RemoveAt(newLst.Count - 1);
                Context.PoligonPoints = newLst.ToArray();
            }
        }

        private void OnBtnPointsClearClicked(object sender, RoutedEventArgs e)
            => Context.PoligonPoints = new Point[0];

        private void OnCnvDrawAreaPressed(object sender, PointerEventArgs e)
        {
            if (Context.IsSelPoligonCheched)
            {
                var pos = e.GetPosition((IVisual)sender);
                Context.PoligonPoints = Context.PoligonPoints.Concat(new[] { pos }).ToArray();
            }
        }

        public async void OnBtnSelImgClicked(object sender, RoutedEventArgs args)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Choose a picture to load";
            ofd.Filters.Add(new FileDialogFilter { Name = "Images", Extensions = new List<string> { "png", "bmp", "jpg", "jpeg" } });
            var selectedFilePath = await ofd.ShowAsync();

            if (selectedFilePath != null && selectedFilePath.Length > 0)
            {
                Context.ImgSource = new Bitmap(selectedFilePath[0]);
            }
        }

        public void OnDrawAreaPreviewMouseMove(object sender, PointerEventArgs e)
        {
            var pos = e.GetPosition((IVisual)sender);
            Context.X = pos.X;
            Context.Y = pos.Y;
        }
    }
}