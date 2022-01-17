using System;
using Avalonia;
//using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;

namespace ImgGetCoordinates
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
             .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToTrace(Avalonia.Logging.LogEventLevel.Information);
    }
}
