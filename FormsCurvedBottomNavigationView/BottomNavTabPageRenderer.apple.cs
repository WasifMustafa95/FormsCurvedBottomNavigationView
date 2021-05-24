using CoreGraphics;
using FormsCurvedBottomNavigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CurvedBottomTabbedPage), typeof(BottomNavTabPageRenderer))]
namespace FormsCurvedBottomNavigation
{
    public class BottomNavTabPageRenderer : TabbedRenderer
    {
        CurvedBottomNavigationView customizedTab;
        CurvedBottomTabbedPage element;
        UIButton appButton;

        public BottomNavTabPageRenderer()
        {
            customizedTab = new CurvedBottomNavigationView();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            CreatingTabBar();
        }

        private async void CreatingTabBar()
        {
            element = Element as CurvedBottomTabbedPage;
            this.RemoveFromParentViewController();

            if (customizedTab != null)
            {
                this.customizedTab.RemoveFromSuperview();
                View.WillRemoveSubview(this.customizedTab);
            }

            if (appButton != null)
            {
                this.appButton.RemoveFromSuperview();
                View.WillRemoveSubview(this.appButton);
            }

            this.TabBar.RemoveFromSuperview();
            View.WillRemoveSubview(this.TabBar);

            customizedTab.Frame = this.TabBar.Frame;
            customizedTab.Items = this.TabBar.Items;
            customizedTab.SelectedItem = this.TabBar.SelectedItem;
            customizedTab.TintColor = this.TabBar.TintColor;
            customizedTab.UnselectedItemTintColor = this.TabBar.UnselectedItemTintColor;
            customizedTab.BarBackgroundColor = this.TabBar.BarTintColor;
            customizedTab.BackgroundColor = Xamarin.Forms.Color.Transparent.ToUIColor();
            customizedTab.ItemSpacing = 4f;
            customizedTab.ClipsToBounds = true;

            // Creates a Button
            appButton = new UIButton(UIButtonType.Custom);

            UIImage imageAppButtonButton =  await element.FabIcon.ToUIImage();

            if (imageAppButtonButton != null)
                appButton.SetImage(imageAppButtonButton, UIControlState.Normal);

            // Sets width and height to the Button
            appButton.Frame = new CGRect(0.0f, 0.0f, 48, 48);

            //appButton.SetBackgroundImage(imageAppButtonButton, UIControlState.Normal);
            appButton.BackgroundColor = UIColor.FromCGColor(element.FabBackgroundColor.ToCGColor());

            CGPoint centers = TabBar.Center;
            centers.Y = this.customizedTab.Frame.Y;
            appButton.Center = centers;

            var eventHandler = new EventHandler(ButtonClick);
            appButton.AddTarget(eventHandler, events: UIControlEvent.TouchUpInside);

            //Create shadow effect
            appButton.Layer.ShadowColor = UIColor.Black.CGColor;
            appButton.Layer.ShadowOffset = new CGSize(width: 0.0, height: 5.0);
            appButton.Layer.ShadowOpacity = 0.5f;
            appButton.Layer.ShadowRadius = 2.0f;
            appButton.Layer.MasksToBounds = false;
            appButton.Layer.CornerRadius = 24;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                SetMenuItemsForPhone();
            }
            else
            {
                SetMenuItemsForTablet();
            }

            //Adds the Button to the view
            if (View.Subviews.Length == 1)
            {
                View.Add(customizedTab);
                View.Add(appButton);
            }
        }

        private void SetMenuItemsForPhone()
        {
            var items = customizedTab.Items;
            if (items.Length == 4)
            {
                var item1 = items[1];
                var item2 = items[2];
                item1.TitlePositionAdjustment = new UIOffset(-20f, 0f);
                item2.TitlePositionAdjustment = new UIOffset(20f, 0f);
            }
            else if (items.Length == 2)
            {
                var item1 = items[0];
                var item2 = items[1];
                item1.TitlePositionAdjustment = new UIOffset(-20f, 0f);
                item2.TitlePositionAdjustment = new UIOffset(20f, 0f);
            }
            else
                throw new Exception("Items should be equal to 2 or 4");
        }

        private void SetMenuItemsForTablet()
        {
            var items = customizedTab.Items;
            if (items.Length == 4)
            {
                var item1 = items[1];
                var item2 = items[2];
                item1.TitlePositionAdjustment = new UIOffset(-3f, 0f);
                item2.TitlePositionAdjustment = new UIOffset(0f, 0f);
            }
            else if (items.Length == 2)
            {
                var item1 = items[0];
                var item2 = items[1];
                item1.TitlePositionAdjustment = new UIOffset(-3f, 0f);
                item2.TitlePositionAdjustment = new UIOffset(0f, 0f);
            }
            else
                throw new Exception("Items should be equal to 2 or 4");
        }

        public void ButtonClick(object sender, System.EventArgs e)
        {
            element.RaiseFabIconClicked();
        }
    }

    public static class Extensions
    {
        public static async Task<UIImage> ToUIImage(this ImageSource imageSource)
        {
            if (imageSource == null)
            {
                return null;
            }
            IImageSourceHandler handler = Xamarin.Forms.Internals.Registrar.Registered.GetHandlerForObject<IImageSourceHandler>(imageSource); 
#if __MOBILE__
                float scale = (float)UIScreen.MainScreen.Scale;
#else
				float scale = (float)NSScreen.MainScreen.BackingScaleFactor;
#endif
                var originalBitmap = await handler.LoadImageAsync(imageSource,scale: scale);

            return originalBitmap;
        }
    }
}
