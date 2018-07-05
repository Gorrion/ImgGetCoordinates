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
        //List<Point> _selectedPoligon = new List<Point>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public class MainWindowModel : ViewModelBase
        {
            public Point[] PoligonPoints { get; set; } = new Point[0];
            public string PoligonCoordinatesText
            {
                get
                {
                    return string.Join(", ", (PoligonPoints ?? new Point[0]).Select(x => string.Format("({0},{1})", x.X, x.Y)));
                }
            }
        }

        private MainWindowModel Context = new MainWindowModel();
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            this.Find<Canvas>("cnvDrawArea").PointerPressed += cnvDrawArea_Pressed;
            this.Find<ToggleButton>("bSelPoligon").Click += bSelPoligon_Click;

            var pol1 = this.Find<Polygon>("Pol1");
            pol1.Stroke = Brushes.DarkBlue;
            pol1.Fill = Brushes.Violet;
            pol1.Points = new List<Point>();
            pol1.StrokeThickness = 2;
            pol1.Opacity = 0.5;

            this.DataContext = Context;

        }

        private void bSelPoligon_Click(object sender, RoutedEventArgs e)
        {
            Context.PoligonPoints = new Point[0];

            //var pol1 = this.Find<Polygon>("Pol1");
            //pol1.Points = new List<Point>();
            //_selectedPoligon = new List<Point>();
            //this.Find<TextBlock>("tSelPoints").Text = string.Empty;
        }

        private void OnPointDelClicked(object sender, RoutedEventArgs e)
        {
            /////////////if (Context.PoligonPoints.Count > 0) { Context.PoligonPoints.RemoveAt(Context.PoligonPoints.Count - 1); }
          //  this.Find<Polygon>("Pol1").Points = _selectedPoligon.ToArray();
        }

        private void OnPointClearClicked(object sender, RoutedEventArgs e)
        {
            Context.PoligonPoints = new Point[0];
            //this.Find<Polygon>("Pol1").Points = _selectedPoligon.ToArray();
            //this.Find<TextBlock>("tSelPoints").Text = string.Empty;
        }

        private void cnvDrawArea_Pressed(object sender, PointerEventArgs e)
        {
            var bSelPoligon = this.Find<ToggleButton>("bSelPoligon");
            if (bSelPoligon.IsChecked.HasValue && bSelPoligon.IsChecked.Value)
            {
                var pos = e.GetPosition((IVisual)sender);
                Context.PoligonPoints = Context.PoligonPoints.Concat(new[] { pos }).ToArray();


                //if (_selectedPoligon.Count > 1) { this.Find<Polygon>("Pol1").Points = _selectedPoligon.ToArray(); }
                //this.Find<TextBlock>("tSelPoints").Text =
                //    string.Join(", ", _selectedPoligon.Select(x => string.Format("({0},{1})", x.X, x.Y)));
            }
        }

        public async void OnButtonClicked(object sender, RoutedEventArgs args)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Choose a picture to load";
            ofd.Filters.Add(new FileDialogFilter { Name = "Images", Extensions = new List<string> { "png", "bmp", "jpg", "jpeg" } });
            var selectedFilePath = await ofd.ShowAsync();

            if (selectedFilePath.Length > 0)
            {
                var img = this.Find<Image>("Img1");

                img.Source = new Bitmap(selectedFilePath[0]);

                img.Width = img.Source.PixelWidth;
                img.Height = img.Source.PixelHeight;

                var drawArea = this.Find<Canvas>("cnvDrawArea");
                drawArea.Width = img.Source.PixelWidth;
                drawArea.Height = img.Source.PixelHeight;
            }
        }

        public void DrawArea_PreviewMouseMove(object sender, PointerEventArgs e)
        {
            //var tt = this.Find<ScrollViewer>("ScrollImg1");

            var pos = e.GetPosition((IVisual)sender);
            this.Find<TextBlock>("bXval").Text = pos.X.ToString();
            this.Find<TextBlock>("bYval").Text = pos.Y.ToString();
        }
    }
}