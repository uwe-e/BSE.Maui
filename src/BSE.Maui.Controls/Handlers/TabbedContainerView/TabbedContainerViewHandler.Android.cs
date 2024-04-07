#nullable enable

using BSE.Maui.Controls.Platforms;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

using TabbedContainer = BSE.Maui.Controls.TabbedContainerPage;

namespace BSE.Maui.Controls.Handlers
{
    public partial class TabbedContainerViewHandler : ViewHandler<TabbedContainer, TabbedContainerView>
    {
        protected override TabbedContainerView CreatePlatformView()
        {
            var tabbedView = new TabbedContainerView(MauiContext);
            tabbedView.SetElement(VirtualView);
            return tabbedView;
        }

        public static void MapCurrentPage(ITabbedContainerViewHandler handler, ITabbedView view)
        {
            if (handler.PlatformView is TabbedContainerView tabbedView)
            {
                tabbedView.ScrollToCurrentPage();
            }
        }

        public static void MapBottomView(ITabbedContainerViewHandler handler, ITabbedContainerView view)
        {
            if (handler?.PlatformView is TabbedContainerView tabbedView)
            {
                var contentControl = view.BottomContent;
                if (contentControl != null)
                {
                    if (contentControl is TemplatedView templatedView)
                    {
                        var bottomView = templatedView.ToPlatform(handler.MauiContext);
                        bottomView.Layout(0, 0, 0, (int)templatedView.HeightRequest);
                        tabbedView.SetBottomView(bottomView);
                    }
                }
            }
        }

        public static void MapBarBackgroundColor(ITabbedContainerViewHandler handler, ITabbedView view)
        {
            if (handler.PlatformView is TabbedContainerView tabbedView)
            {
                tabbedView.UpdateBarBackgroundColor();
            }
        }

        public static void MapSelectedTabColor(ITabbedContainerViewHandler handler, ITabbedView view)
        {
            if (handler.PlatformView is TabbedContainerView tabbedView)
            {
                tabbedView.UpdateTabItemStyle();
            }
        }

        public static void MapUnselectedTabColor(ITabbedContainerViewHandler handler, ITabbedView view)
        {
            if (handler.PlatformView is TabbedContainerView tabbedView)
            {
                tabbedView.UpdateTabItemStyle();
            }
        }

        public static void MapIsSwipePagingEnabled(ITabbedViewHandler handler, TabbedContainer container)
        {
            if (handler.PlatformView is TabbedContainerView tabbedView)
            {
                tabbedView.UpdateSwipePaging();
            }
        }

        public static void MapIsSwipePagingEnabled(ITabbedContainerViewHandler handler, ITabbedContainerView view)
        {
            if (handler.PlatformView is TabbedContainerView tabbedView)
            {
                tabbedView.UpdateSwipePaging();
            }
        }
    }
}
