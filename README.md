# FormsCurvedBottomNavigation

FormsCurvedBottomNavigation allows you to integrate beautiful curved UI in your Xamarin Forms bottom tabbed page with a fab button (available for Android and iOS both).

## Installation

Use the nuget package manager to install FormsCurvedBottomNavigation .

```bash
Install-Package FormsCurvedBottomNavigation -Version 1.2.0
```

Or you can download manually via https://www.nuget.org/packages/FormsCurvedBottomNavigation/

## Description

There are three new properties attached to this CurvedBottomTabbedPage.
* FabIcon (To set the icon on fab button)
* FabBackgroundColor (To set the background color of the fab button)
* FabIconClicked (This event will be raised whenever FabIcon clicked).

## Usage

Android

![!Picture](https://raw.githubusercontent.com/WasifMustafa95/FormsCurvedBottomNavigationView/master/FormsCurvedBottomNavigationView/FormsCurvedBottomNavigationView/Screenshots/android.png)

iOS

![!Picture](https://raw.githubusercontent.com/WasifMustafa95/FormsCurvedBottomNavigationView/master/FormsCurvedBottomNavigationView/FormsCurvedBottomNavigationView/Screenshots/ios.png)

```c#
<curvebottomnavigation:CurvedBottomTabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
            xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
            xmlns:android="clr-namespace:Xamarin.Forms.PlatformConfiguration.AndroidSpecific;assembly=Xamarin.Forms.Core"
            xmlns:curvebottomnavigation="clr-namespace:FormsCurvedBottomNavigation;assembly=FormsCurvedBottomNavigation"
            BarBackgroundColor="FloralWhite"
            android:TabbedPage.BarItemColor="Gray"
            android:TabbedPage.BarSelectedItemColor="Blue"
            FabIcon="home_icon"
            FabBackgroundColor="SkyBlue"
	    FabIconClicked="CurvedBottomTabbedPage_FabIconClicked">

</curvebottomnavigation:CurvedBottomTabbedPage>
```

```c#
 public partial class MainPage : FormsCurvedBottomNavigation.CurvedBottomTabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.Children.Add(new Page1()
            {
                Title = "Home",
                IconImageSource = "home_icon"
            });
            this.Children.Add(new Page1()
            {
                Title = "Movies",
                IconImageSource = "movie_icon"
            });
            this.Children.Add(new Page1()
            {
                Title = "Music",
                IconImageSource = "music_icon"
            });
            this.Children.Add(new Page1()
            {
                Title = "Profile",
                IconImageSource = "profile_icon"
            });
         }
	
            private void CurvedBottomTabbedPage_FabIconClicked(object sender, EventArgs e)
            {
            	//Do something here
            }
    }
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## Support
Support this project.

[PayPal](https://paypal.me/saqibmustafa)

## License
[MIT](https://github.com/WasifMustafa95/FormsCurvedBottomNavigationView/blob/master/LICENSE)
