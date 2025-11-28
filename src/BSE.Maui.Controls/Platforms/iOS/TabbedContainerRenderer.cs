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

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // Only attempt if not already created
            if (_bottomView == null && Page != null)
            {
                try
                {
                    SetupUserInterface();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"\t\t\tERROR(SetupUserInterface in ViewDidAppear): {ex}");
                }
            }
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
                // Keep the call here for non-MediaElement cases, but the real creation
                // for MediaElement may be deferred until ViewDidAppear above
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

                    // Create a scoped IMauiContext that provides the current UIViewController and UIWindow.
                    // Some platform controls (MediaElement) inspect IMauiContext.Context or request UIWindow/UIViewController
                    // from Services during handler creation. Provide both to avoid "current view controller cannot be detected".
                    var scopedContext = new MauiContextWithViewControllerAndWindow(mauiContext, this);

                    try
                    {
                        _bottomView = view.ToPlatform(scopedContext);
                        _bottomView.Frame = new System.Drawing.RectangleF(
                            0,
                            0,
                            (float)View.Frame.Width,
                            (float)view.Height);
                        View.AddSubview(_bottomView);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"\t\t\tERROR while converting BottomView to platform view and adding as subview in SetupUserInterface: {ex}");
                        throw;
                    }
                }
            }
        }

        // Scoped IMauiContext wrapper that returns the provided UIViewController and UIWindow from Services
        // and exposes the UIWindow (or fallback to UIViewController) as Context. This covers lookup patterns
        // used by various platform controls (including MediaElement).
        private sealed class MauiContextWithViewControllerAndWindow : IMauiContext
        {
            readonly IMauiContext _inner;
            readonly UIViewController _viewController;
            readonly IServiceProvider _services;

            public MauiContextWithViewControllerAndWindow(IMauiContext inner, UIViewController viewController)
            {
                _inner = inner ?? throw new ArgumentNullException(nameof(inner));
                _viewController = viewController ?? throw new ArgumentNullException(nameof(viewController));
                _services = new ViewControllerWindowServiceProvider(inner.Services, _viewController);
            }

            // Some platform code expects IMauiContext.Context to be a UIWindow/UIView — prefer the window.
            public object Context
            {
                get
                {
                    try
                    {
                        var window = _viewController?.View?.Window;
                        if (window != null)
                            return window;
                    }
                    catch (NullReferenceException nre)
                    {
                        // NullReferenceException can occur due to teardown/race conditions when accessing View or Window.
                        System.Diagnostics.Debug.WriteLine($"NullReferenceException while accessing View.Window; falling back to UIViewController. {nre}");
                    }
                    catch (Exception ex)
                    {
                        // Log unexpected exceptions to aid debugging rather than silently swallowing them.
                        System.Diagnostics.Debug.WriteLine($"Unexpected exception while retrieving window from view controller: {ex}");
                    }

                    return _viewController;
                }
            }

            public IServiceProvider Services => _services;

            public IMauiHandlersFactory Handlers => _inner.Handlers;

            private sealed class ViewControllerWindowServiceProvider : IServiceProvider
            {
                readonly IServiceProvider _inner;
                readonly UIViewController _vc;

                public ViewControllerWindowServiceProvider(IServiceProvider inner, UIViewController vc)
                {
                    _inner = inner;
                    _vc = vc;
                }

                public object GetService(Type serviceType)
                {
                    if (serviceType == typeof(UIViewController) || typeof(UIViewController).IsAssignableFrom(serviceType))
                        return _vc;

                    if (serviceType == typeof(UIWindow) || typeof(UIWindow).IsAssignableFrom(serviceType))
                    {
                        try
                        {
                            return _vc?.View?.Window;
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Exception in GetService (UIWindow): {ex}");
                            return null;
                        }
                    }

                    return _inner.GetService(serviceType);
                }
            }
        }
    }
}