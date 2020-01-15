using CoreGraphics;
using FormsCurvedBottomNavigation;
using System;
using System.Collections.Generic;
using System.Text;
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
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            customizedTab = new CurvedBottomNavigationView();
            element = Element as CurvedBottomTabbedPage;

            customizedTab.Frame = this.TabBar.Frame;
            customizedTab.Items = this.TabBar.Items;
            customizedTab.SelectedItem = this.TabBar.SelectedItem;
            customizedTab.TintColor = this.TabBar.TintColor;
            customizedTab.UnselectedItemTintColor = this.TabBar.UnselectedItemTintColor;
            customizedTab.BarBackgroundColor = this.TabBar.BarTintColor;
            customizedTab.BackgroundColor = Xamarin.Forms.Color.Transparent.ToUIColor();
            customizedTab.ItemSpacing = 4f;
            customizedTab.ClipsToBounds = true;
            this.TabBar.RemoveFromSuperview();

            SetMenuItems();

            // Creates a Button
            var appButton = new UIButton(UIButtonType.Custom);

            UIImage imageAppButtonButton = UIImage.FromBundle(element.FabIcon);

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

            // Adds the Button to the view
            View.Add(customizedTab);
            View.AddSubview(appButton);
        }

        private void SetMenuItems()
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

        public void ButtonClick(object sender, System.EventArgs e)
        {
            element.RaiseFabIconClicked();
        }
    }
}
