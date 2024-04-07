//#nullable disable

#if IOS || MACCATALYST
using PlatformView = UIKit.UIView;
#elif MONOANDROID
using PlatformView = BSE.Maui.Controls.Platforms.TabbedContainerView;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.FrameworkElement;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif

using Microsoft.Maui.Handlers;
using TabbedContainer = BSE.Maui.Controls.TabbedContainerPage;

namespace BSE.Maui.Controls.Handlers
{
    public partial class TabbedContainerViewHandler : ITabbedContainerViewHandler
    {
        public static IPropertyMapper<ITabbedContainerView, ITabbedContainerViewHandler> Mapper =
            new PropertyMapper<ITabbedContainerView, ITabbedContainerViewHandler>(ViewHandler.ViewMapper)
            {
#if ANDROID
                [nameof(TabbedContainer.CurrentPage)] = MapCurrentPage,
                [nameof(TabbedContainer.BottomView)] = MapBottomView,
                [nameof(TabbedContainer.BarBackgroundColor)] = MapBarBackgroundColor,
                [nameof(TabbedContainer.UnselectedTabColor)] = MapUnselectedTabColor,
                [nameof(TabbedContainer.SelectedTabColor)] = MapSelectedTabColor,
                [nameof(BSE.Maui.Controls.Platforms.AndroidSpecific.TabbedContainerPage.IsSwipePagingEnabledProperty.PropertyName)] = MapIsSwipePagingEnabled
#endif
            };

        public static CommandMapper<ITabbedView, ITabbedContainerViewHandler> CommandMapper =
           new CommandMapper<ITabbedView, ITabbedContainerViewHandler>(ViewHandler.ElementCommandMapper);

        public TabbedContainerViewHandler()
            : base(Mapper, CommandMapper)
        {
        }

    }
}
