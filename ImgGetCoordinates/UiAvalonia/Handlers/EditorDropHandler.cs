// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using ImgGetCoordinates.Libs.Be;
using ImgGetCoordinates.UiAvalonia.DnD;
using ImgGetCoordinates.ViewModels;

namespace ImgGetCoordinates.UiAvalonia.Handlers
{
    /// <summary>
    /// Project editor drop handler.
    /// </summary>
    public class EditorDropHandler : DefaultDropHandler
    {
        public static IDropHandler Instance = new EditorDropHandler();

        /// <inheritdoc/>
        public override bool Validate(object sender, DragEventArgs e, object sourceContext, object targetContext, object state)
        {
            var fN = e.Data.GetFileNames()?.FirstOrDefault();
            var result = fN != null && ImageInfo.Formats.Any(x => x.Equals(fN.Split('.').Last(), System.StringComparison.OrdinalIgnoreCase));
            return result;
        }

        /// <inheritdoc/>
        public override bool Execute(object sender, DragEventArgs e, object sourceContext, object targetContext, object state)
        {
            var fN = e.Data.GetFileNames()?.FirstOrDefault();
            if (fN != null)
            {
                try
                {
                    if (e.Source is IControl)
                    {
                        var control = e.Source as IControl;
                        if (control.DataContext is MainWindowViewModel)
                        {
                            var context = control.DataContext as MainWindowViewModel;
                            context.ImgSource = new Bitmap(fN);
                            return true;
                        }
                    }
                }
                catch { }
            }

            return false;
        }
    }
}
