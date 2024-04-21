namespace BSE.Maui.Controls
{
    internal static partial class ViewExtensions
    {
        internal static IMauiContext? FindMauiContext(this Element element, bool fallbackToAppMauiContext = false)
        {
            if (element is Microsoft.Maui.IElement fe && fe.Handler?.MauiContext != null)
            {
                return fe.Handler.MauiContext;
            }

            return fallbackToAppMauiContext ? Application.Current?.FindMauiContext() : default;
        }
    }
}
