using Microsoft.Maui.Handlers;
//using PlatformView = UIKit.UIView;
using TabbedContainer = BSE.Maui.Controls.TabbedContainerPage;

namespace BSE.Maui.Controls.Handlers
{
    public partial class TabbedContainerViewHandler : ViewHandler<ITabbedContainerView, UIKit.UIView>
    {
        protected override UIKit.UIView CreatePlatformView() => throw new NotImplementedException();
    }
}
