using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FormsCurvedBottomNavigation
{
    public class CurvedBottomTabbedPage : TabbedPage
    {
        public static readonly BindableProperty FabIconProperty =
            BindableProperty.Create(nameof(FabIcon), typeof(ImageSource), typeof(CurvedBottomTabbedPage), default(ImageSource));

        public static readonly BindableProperty FabBackgroundColorProperty =
            BindableProperty.Create(nameof(FabBackgroundColor), typeof(Color), typeof(CurvedBottomTabbedPage), Color.SkyBlue);

        public ImageSource FabIcon
        {
            get => (ImageSource)GetValue(FabIconProperty);
            set=> SetValue(FabIconProperty, value);
        }

        public Color FabBackgroundColor
        {
            get => (Color)GetValue(FabBackgroundColorProperty);
            set => SetValue(FabBackgroundColorProperty, value);
        }

        public event EventHandler<EventArgs> FabIconClicked;

        public void RaiseFabIconClicked()
        {
            FabIconClicked?.Invoke(this, EventArgs.Empty);
        }

        public CurvedBottomTabbedPage()
        {
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(this, Xamarin.Forms.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false);
        }
    }
}
