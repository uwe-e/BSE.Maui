using FormsElement = BSE.Maui.Controls.TabbedContainerPage;

namespace BSE.Maui.Tabbed.Platforms.AndroidSpecific
{
    internal static class TabbedContainerPage
    {
        public static readonly BindableProperty IsSwipePagingEnabledProperty =
            BindableProperty.Create("IsSwipePagingEnabled", typeof(bool),
            typeof(TabbedContainerPage), true);

        public static bool GetIsSwipePagingEnabled(BindableObject element)
        {
            return (bool)element.GetValue(IsSwipePagingEnabledProperty);
        }

        public static bool IsSwipePagingEnabled(this IPlatformElementConfiguration<Microsoft.Maui.Controls.PlatformConfiguration.Android, FormsElement> config)
        {
            return GetIsSwipePagingEnabled(config.Element);
        }
    }
}
