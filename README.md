# FormsCurvedBottomNavigation

FormsCurvedBottomNavigation allows you to integrate beautiful curved UI in your bottom tabbed page with a fab button. (available on Android only for now)

## Installation

Use the nuget package manager to install FormsCurvedBottomNavigation .

```bash
Install-Package FormsCurvedBottomNavigation -Version 1.0.0
```

## Description

There are three new properties attached to this CurvedBottomTabbedPage.
* FabIcon (To set the icon on fab button)
* FabBackgroundColor (To set the background color of the fab button)
* NavigateToPage (This property is used to navigate to the page you want from fab button).

NavigateToPage property by default uses PushModalAsync.

## Usage

![Picture](https://raw.githubusercontent.com/WasifMustafa95/FormsCurvedBottomNavigationView/master/FormsCurvedBottomNavigationView/FormsCurvedBottomNavigationView/Screenshots/Screenshot_20191226-002127.png)

```c#
<curvebottomnavigation:CurvedBottomTabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
            xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
            xmlns:android="clr-namespace:Xamarin.Forms.PlatformConfiguration.AndroidSpecific;assembly=Xamarin.Forms.Core"
            xmlns:curvebottomnavigation="clr-namespace:FormsCurvedBottomNavigation;assembly=FormsCurvedBottomNavigation"
            BarBackgroundColor="FloralWhite"
            BackgroundColor="Orange"
            android:TabbedPage.BarItemColor="Gray"
            android:TabbedPage.BarSelectedItemColor="Blue"
            FabIcon="home_icon"
            FabBackgroundColor="SkyBlue">

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

            this.NavigateToPage = new Page1();
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
