using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Avalonia.VisualTree;
using ImgGetCoordinates.Libs.OS;
using ImgGetCoordinates.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImgGetCoordinates.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            // this.AttachDevTools();
#endif
        }

        private MainWindowViewModel Context => (MainWindowViewModel)this.DataContext;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);


            var cnvDrawArea = this.Find<Canvas>("CnvDrawArea");
            cnvDrawArea.PointerPressed += OnCnvDrawAreaPressed;
            cnvDrawArea.PointerMoved += OnDrawAreaPreviewMouseMove;
            this.Find<Button>("BtnSelImg").Click += OnBtnSelImgClicked;
            this.Find<Button>("BtnPointDel").Click += OnBtnPointDelClicked;
            this.Find<Button>("BtnPointsClear").Click += OnBtnPointsClearClicked;
            this.Find<Button>("BtnPointsCopy").Click += OnBtnPointsCopyClicked;
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

        private void OnBtnPointsCopyClicked(object sender, RoutedEventArgs e)
        {
            Clipboard.Copy(string.Join(";", Context.PoligonPoints.Select(x => string.Format("{0},{1}", (int)x.X, (int)x.Y))));
        }

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
            var selectedFilePath = await ofd.ShowAsync(this);

            if (selectedFilePath != null && selectedFilePath.Length > 0)
            {
                Context.ImgSource = new Bitmap(selectedFilePath[0]);
            }
        }

        public void OnDrawAreaPreviewMouseMove(object sender, PointerEventArgs e)
        {
            var pos = e.GetPosition((IVisual)sender);
            Context.X = (int)pos.X;
            Context.Y = (int)pos.Y;
        }
    }
}
