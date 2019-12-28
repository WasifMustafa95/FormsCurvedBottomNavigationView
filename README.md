# FormsCurvedBottomNavigation

FormsCurvedBottomNavigation allows you to integrate beautiful curved UI in your bottom tabbed page with a fab button.

## Installation

Use the nuget package manager to install FormsCurvedBottomNavigation .

```bash
Install-Package FormsCurvedBottomNavigation -Version 1.0.0
```

## Usage

```c#
<local:CurvedBottomTabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:FormsCurvedBottomNavigation;assembly=FormsCurvedBottomNavigation"
             xmlns:android="clr-namespace:Xamarin.Forms.PlatformConfiguration.AndroidSpecific;assembly=Xamarin.Forms.Core"
             android:TabbedPage.BarItemColor="Gray"
             android:TabbedPage.BarSelectedItemColor="Blue"
             BarBackgroundColor="Pink"
             FabIcon="home_icon"
             FabBackgroundColor="Pink"
             x:Class="App1.MainPage">

</local:CurvedBottomTabbedPage>
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
                Title = "Home",
                IconImageSource = "movie_icon"
            });

            this.NavigateToPage = new Page1();
        }
    }
```

NavigateToPage property is used to navigate to the page you want from fab button.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://github.com/WasifMustafa95/FormsCurvedBottomNavigationView/blob/master/LICENSE)
