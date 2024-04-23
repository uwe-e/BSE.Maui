# BSE.Maui

## TabbedContainerPage Extension of the TabbedPage

For a mobile player app, we needed a way to add the player actions "play", "stop", "next" etc. For this reason, we have extended the .NET MAUI TabbedPage with an additional BottomView element.

![TabbedContainerPage Android](/docs/tabbedcontainerpage/TabbedContainerPage-Android.gif)
![TabbedContainerPage iPhone](/docs/tabbedcontainerpage/TabbedContainerPage-iPhone.gif)

### Platforms
iOS, Android

*For windows we plan a Flyoutpage extension

### Sample

For using, you have to register the controls as follows:

```
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBSEControls();

            return builder.Build();
        }
```

The TabbedConatinerPage should be configured in XAML as follows

```
<?xml version="1.0" encoding="utf-8" ?>
<bse:TabbedContainerPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                         xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                         xmlns:bse="clr-namespace:BSE.Maui.Controls;assembly=BSE.Maui.Controls"
                         xmlns:views="clr-namespace:BSE.Maui.Samples.Views"
                         x:Class="BSE.Maui.Samples.Pages.TabbedPage.MainPage"
                         xmlns:android="clr-namespace:BSE.Maui.Controls.PlatformConfiguration.AndroidSpecific;assembly=BSE.Maui.Controls"
                         android:TabbedContainerPage.IsSwipePagingEnabled="False"
                         BarBackgroundColor="{StaticResource Primary}"
                         SelectedTabColor="{StaticResource White}"
                         UnselectedTabColor="{StaticResource Gray400}"
                         Title="TabbedPage">

    <NavigationPage Title="Home" IconImageSource="home.png">
        <x:Arguments>
            <views:Home/>
        </x:Arguments>
    </NavigationPage>

    <NavigationPage Title="Search" IconImageSource="playlist.png">
        <x:Arguments>
            <views:Search/>
        </x:Arguments>
    </NavigationPage>

    <NavigationPage Title="Settings" IconImageSource="settings.png">
        <x:Arguments>
            <views:Settings/>
        </x:Arguments>
    </NavigationPage>

    <bse:TabbedContainerPage.BottomView>
        <views:BottomView HeightRequest="{OnPlatform 250, iOS=90, Android=200}"/>
    </bse:TabbedContainerPage.BottomView>

</bse:TabbedContainerPage>
```

The container page shoud derived from TabbedContainerPage

```
using BSE.Maui.Controls;

namespace BSE.Maui.Samples.Pages.TabbedPage;

public partial class MainPage : TabbedContainerPage
{
	public MainPage()
	{
		InitializeComponent();
	}
}
```

The BottomView element can contain any ContentView element.

### Features
The code is as it is.
#### Android
The TabbedPages Android implementation most contains internal functions. That's why we had to copy a lot of the functions. The tabs are always at the bottom of the page. We only use the BottomNavigationView.
As an Android specific feature, we only implemented the "IsSwipePagingEnabled" setting.

```
xmlns:android="clr-namespace:BSE.Maui.Controls.PlatformConfiguration.AndroidSpecific;assembly=BSE.Maui.Controls"
android:TabbedContainerPage.IsSwipePagingEnabled="False"
```
