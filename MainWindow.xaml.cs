using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System.Collections.Generic;

namespace ImgGetCoordinates
{
    public class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
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

                var drawArea = this.Find<Canvas>("DrawArea");
                drawArea.Width = img.Source.PixelWidth;
                drawArea.Height = img.Source.PixelHeight;

                //this.Renderer.Start();
            }
            //Content.
            //var context = DataContext as HelloViewModel;
            //context.Result = "Hi " + context.Name + " !";
        }


        public void DrawArea_PreviewMouseMove(object sender, PointerEventArgs args)
        {
            var tt = "";
        }

    }
}