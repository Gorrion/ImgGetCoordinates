﻿using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;

namespace ImgGetCoordinates
{
    class Program
    {
        public static void Main(string[] args) => BuildAvaloniaApp()
             .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug(Avalonia.Logging.LogEventLevel.Information);
    }
}
