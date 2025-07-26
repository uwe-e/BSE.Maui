#nullable disable

using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using UIKit;

namespace BSE.Maui.Controls.Platforms.iOS
{
    public class TabbedContainerRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.TabbedRenderer
    {
        private UIView _bottomView;

        TabbedContainerPage Page => Element as TabbedContainerPage;

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            if (Element == null)
            {
                return;
            }
            
            if (_bottomView != null)
            {
                var frame = View.Frame;
                var tabBarFrame = TabBar.Frame;
                var bottomFrame = _bottomView.Frame;

                /*
                 * this view contains the content of the page
                */ 
                var pageView = View.Subviews[0];

                /*
                 * we place the bottom view to its position above the tabbar
                */ 
                _bottomView.Frame = new Rect(
                    frame.Left,
                    tabBarFrame.Top - bottomFrame.Height,
                    frame.Width, bottomFrame.Height);

                /*
                 * finally, we have to reduce the height of the view that contains the page content
                 * so that we can scroll through the entire content of the view. 
                */
                pageView.Frame = new Rect(
                    frame.Left,
                    frame.Top,
                    frame.Width,
                    frame.Height - bottomFrame.Height);
                
            }
        }

        public override void RemoteControlReceived(UIEvent theEvent)
        {
            base.RemoteControlReceived(theEvent);
            /*
             * The AudioPlayer view does not receive a RemoteControlReceived event.
             * Because of this we execute that event from here.
             * 
            */ 
            Console.WriteLine($"{nameof(RemoteControlReceived)} {theEvent.Subtype} ");
            _bottomView?.RemoteControlReceived(theEvent);
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Page == null)
            {
                return;
            }
            try
            {
                SetupUserInterface();
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"\t\t\tERROR: {exception.Message}");
            }
        }

        private void SetupUserInterface()
        {
            if (Page.BottomView is IView view)
            {
                var mauiContext = Page.FindMauiContext();
                if (mauiContext != null)
                {
                    var height = view.Height;
                    _bottomView = view.ToPlatform(mauiContext);
                    _bottomView.Frame = new System.Drawing.RectangleF(
                        0,
                        0,
                        (float)View.Frame.Width,
                        (float)view.Height);
                    View.AddSubview(_bottomView);
                }
            }
        }
    }
}