using Android.Content;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace BSE.Maui.Controls.Platforms
{
    internal static partial class ViewExtensions
    {
        internal static void Arrange(
            this IView view,
            int left,
            int top,
            int right,
            int bottom,
            Context context)
        {
            var deviceIndependentLeft = context.FromPixels(left);
            var deviceIndependentTop = context.FromPixels(top);
            var deviceIndependentRight = context.FromPixels(right);
            var deviceIndependentBottom = context.FromPixels(bottom);
            var destination = Rect.FromLTRB(0, 0,
                deviceIndependentRight - deviceIndependentLeft, deviceIndependentBottom - deviceIndependentTop);

            if (!view.Frame.Equals(destination))
                view.Arrange(destination);
        }

        internal static void Arrange(this IView view, AView.LayoutChangeEventArgs e)
        {
            var context = view.Handler?.MauiContext?.Context ??
                 throw new InvalidOperationException("View is Missing Handler");

            view.Arrange(e.Left, e.Top, e.Right, e.Bottom, context);
        }
    }
}
