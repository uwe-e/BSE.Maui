using BSE.Maui.Controls.Handlers;

namespace BSE.Maui.Controls
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder UseBSEControls(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
                handlers.AddHandler(typeof(TabbedContainerPage), typeof(TabbedContainerViewHandler));
#endif
            });
                return builder;
        }
    }
}
