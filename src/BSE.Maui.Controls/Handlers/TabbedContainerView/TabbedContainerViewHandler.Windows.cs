using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml;
using TabbedContainer = BSE.Maui.Controls.TabbedContainerPage;

namespace BSE.Maui.Controls.Handlers
{
    public partial class TabbedContainerViewHandler : ViewHandler<ITabbedContainerView, Microsoft.UI.Xaml.FrameworkElement>
    {
        protected override FrameworkElement CreatePlatformView()
        {
            throw new NotImplementedException();
        }
    }
}
