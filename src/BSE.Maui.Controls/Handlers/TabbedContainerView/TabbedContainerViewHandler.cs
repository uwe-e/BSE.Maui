#nullable disable

using Microsoft.Maui.Handlers;
using TabbedContainer = BSE.Maui.Controls.TabbedContainerPage;

namespace BSE.Maui.Controls.Handlers
{
    public partial class TabbedContainerViewHandler
    {
        public static PropertyMapper<TabbedContainer, TabbedContainerViewHandler> Mapper =
            new PropertyMapper<TabbedContainer, TabbedContainerViewHandler>(ElementMapper)
            {
#if ANDROID
                [nameof(TabbedContainer.CurrentPage)] = MapCurrentPage,
                [nameof(TabbedContainer.BottomView)] = MapBottomView,
                [nameof(TabbedContainer.BarBackgroundColor)] = MapBarBackgroundColor,
                [nameof(TabbedContainer.UnselectedTabColor)] = MapUnselectedTabColor,
                [nameof(TabbedContainer.SelectedTabColor)] = MapSelectedTabColor,

                [nameof(BSE.Maui.Tabbed.Platforms.AndroidSpecific.TabbedContainerPage.IsSwipePagingEnabledProperty.PropertyName)] = MapIsSwipePagingEnabled
#endif
            };

        public static CommandMapper<TabbedContainer, TabbedContainerViewHandler> CommandMapper =
           new CommandMapper<TabbedContainer, TabbedContainerViewHandler>(ViewHandler.ElementCommandMapper);

        public TabbedContainerViewHandler()
            : base(Mapper, CommandMapper)
        {
        }
    }
}
