using FormsElement = BSE.Maui.Controls.TabbedContainerPage;

namespace BSE.Maui.Controls.PlatformConfiguration.AndroidSpecific
{
    public static class TabbedContainerPage
    {
        public static readonly BindableProperty IsSwipePagingEnabledProperty =
            BindableProperty.Create("IsSwipePagingEnabled", typeof(bool),
            typeof(TabbedContainerPage), true);

        public static bool GetIsSwipePagingEnabled(BindableObject element)
        {
            return (bool)element.GetValue(IsSwipePagingEnabledProperty);
        }

        public static void SetIsSwipePagingEnabled(BindableObject element, bool value)
        {
            element.SetValue(IsSwipePagingEnabledProperty, value);
        }

        public static bool IsSwipePagingEnabled(this IPlatformElementConfiguration<Microsoft.Maui.Controls.PlatformConfiguration.Android, FormsElement> config)
        {
            return GetIsSwipePagingEnabled(config.Element);
        }
    }
}
