using System;
using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AMenu = Android.Views.Menu;
using AView = Android.Views.View;
using AColor = Android.Graphics.Color;
using System.Collections.Specialized;
using Color = Xamarin.Forms.Color;
using Android.Content.Res;
using Android.Support.Design.Internal;
using System.ComponentModel;
using FormsCurvedBottomNavigation;
using Android.Support.Design.BottomNavigation;

[assembly: ExportRenderer(typeof(CurvedBottomTabbedPage), typeof(BottomNavTabPageRenderer))]
namespace FormsCurvedBottomNavigation
{
    internal class BottomNavTabPageRenderer : TabbedPageRenderer
    {
        private bool _isShiftModeSet;
        CurvedBottomNavigationView bottombar;
        FloatingActionButton actionbutton;
        CurvedBottomTabbedPage element;
        ColorStateList _originalTabTextColors;
        ColorStateList _orignalTabIconColors;

        ColorStateList _newTabTextColors;
        ColorStateList _newTabIconColors;
        int[] _checkedStateSet = null;
        int[] _selectedStateSet = null;
        int[] _emptyStateSet = null;
        int _defaultARGBColor = Color.Default.ToAndroid().ToArgb();
        AColor _defaultAndroidColor = Color.Default.ToAndroid();

        public Color BarItemColor
        {
            get
            {
                if (Element != null)
                {
                    if (Element.IsSet(Xamarin.Forms.TabbedPage.UnselectedTabColorProperty))
                        return Element.UnselectedTabColor;
                }

                return Color.Default;
            }
        }

        public Color BarSelectedItemColor
        {
            get
            {
                if (Element != null)
                {
                    if (Element.IsSet(Xamarin.Forms.TabbedPage.SelectedTabColorProperty))
                        return Element.SelectedTabColor;
                }

                return Color.Default;
            }
        }
        public BottomNavTabPageRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            try
            {
                element = Element as CurvedBottomTabbedPage;

                var metrics = Resources.DisplayMetrics;
                var width = metrics.WidthPixels;
                var height = metrics.HeightPixels;

                if (!(GetChildAt(0) is ViewGroup layout))
                    return;

                if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
                    return;

                bottomNavigationView.RemoveFromParent();
                bottomNavigationView.RemoveAllViews();
                bottomNavigationView.RemoveAllViewsInLayout();

                var bottomView = LayoutInflater.From(Context).Inflate(Resource.Layout.BottomNavBar, null);

                bottombar = bottomView.FindViewById<CurvedBottomNavigationView>(Resource.Id.bottom_nav_bar);
                bottombar.RemoveFromParent();
                actionbutton = bottomView.FindViewById<FloatingActionButton>(Resource.Id.fab);
                actionbutton.RemoveFromParent();

                SetTabItems();
                SettingUpMenu();
                OnChildrenCollectionChanged(null, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

                SetFabProperties();
                actionbutton.Click += Actionbutton_Click;

                bottombar.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityLabeled;
                layout.AddView(actionbutton);
                layout.AddView(bottombar);
                bottombar.SetZ(0);

                UpdateBarBackgroundColor();
                UpdateBarTextColor();
                UpdateItemIconColor();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SettingUpMenu()
        {
            try
            {
                var menuView = bottombar.GetChildAt(0) as BottomNavigationMenuView;
                if (menuView.ChildCount == 4)
                {
                    BottomNavigationItemView item1 = (BottomNavigationItemView)menuView.GetChildAt(0);
                    BottomNavigationItemView item2 = (BottomNavigationItemView)menuView.GetChildAt(1);
                    BottomNavigationItemView item3 = (BottomNavigationItemView)menuView.GetChildAt(2);
                    BottomNavigationItemView item4 = (BottomNavigationItemView)menuView.GetChildAt(3);
                    item1.SetPadding(0, 0, 20, 0);
                    item2.SetPadding(0, 0, 20, 0);
                    item3.SetPadding(20, 0, 0, 0);
                    item4.SetPadding(20, 0, 0, 0);
                }
                else if (menuView.ChildCount == 2)
                {
                    BottomNavigationItemView item1 = (BottomNavigationItemView)menuView.GetChildAt(0);
                    BottomNavigationItemView item2 = (BottomNavigationItemView)menuView.GetChildAt(1);
                    item1.SetPadding(0, 0, 20, 0);
                    item2.SetPadding(20, 0, 0, 0);
                }
                else
                    throw new Exception("Items should be equal to 2 or 4");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SetFabProperties()
        {
            try
            {
                if (!string.IsNullOrEmpty(element.FabIcon))
                {
                    var resId = GetDrawable(element.FabIcon);
                    actionbutton.SetImageResource(resId);
                }

                if (element.FabBackgroundColor != Color.SkyBlue)
                {
                    Android.Content.Res.ColorStateList csl = new Android.Content.Res.ColorStateList(new int[][] { new int[0] }, new int[] { element.FabBackgroundColor.ToAndroid() });
                    actionbutton.BackgroundTintList = csl;
                }

                if (element.BarBackgroundColor != Color.Default)
                    bottombar.BarColor = element.BarBackgroundColor.ToAndroid();
                else
                {
                    element.BarBackgroundColor = Color.LightBlue;
                    bottombar.BarColor = element.BarBackgroundColor.ToAndroid();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private int GetDrawable(string fabIcon)
        {
            int resID = Resources.GetIdentifier(fabIcon, "drawable", this.Context.PackageName);
            return resID;
        }

        private void Actionbutton_Click(object sender, EventArgs e)
        {
            element.RaiseFabIconClicked();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == NavigationPage.BarBackgroundColorProperty.PropertyName)
                UpdateBarBackgroundColor();
            else if (e.PropertyName == NavigationPage.BarTextColorProperty.PropertyName ||
                e.PropertyName == Xamarin.Forms.TabbedPage.UnselectedTabColorProperty.PropertyName ||
                e.PropertyName == Xamarin.Forms.TabbedPage.SelectedTabColorProperty.PropertyName)
            {
                _newTabTextColors = null;
                _newTabIconColors = null;
                UpdateBarTextColor();
                UpdateItemIconColor();
            }
        }

        void UpdateBarBackgroundColor()
        {
            Color tintColor = Element.BarBackgroundColor;

            if (tintColor.IsDefault)
                bottombar.SetBackground(null);
            else if (!tintColor.IsDefault)
                bottombar.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }

        void UpdateBarTextColor()
        {
            var colors = GetItemTextColorStates() ?? _originalTabTextColors;
            bottombar.ItemTextColor = colors;
        }

        void UpdateItemIconColor()
        {
            bottombar.ItemIconTintList = GetItemIconTintColorState() ?? _orignalTabIconColors;
        }

        protected virtual ColorStateList GetItemTextColorStates()
        {
            if (_originalTabTextColors == null)
                _originalTabTextColors = bottombar.ItemTextColor;

            Color barItemColor = BarItemColor;
            Color barTextColor = Element.BarTextColor;
            Color barSelectedItemColor = BarSelectedItemColor;

            if (barItemColor.IsDefault && barTextColor.IsDefault && barSelectedItemColor.IsDefault)
                return _originalTabTextColors;

            if (_newTabTextColors != null)
                return _newTabTextColors;

            int checkedColor;
            int defaultColor;

            if (!barTextColor.IsDefault)
            {
                checkedColor = barTextColor.ToAndroid().ToArgb();
                defaultColor = checkedColor;
            }
            else
            {
                defaultColor = barItemColor.ToAndroid().ToArgb();

                if (barItemColor.IsDefault && _originalTabTextColors != null)
                    defaultColor = _originalTabTextColors.DefaultColor;

                checkedColor = defaultColor;

                if (!barSelectedItemColor.IsDefault)
                    checkedColor = barSelectedItemColor.ToAndroid().ToArgb();
            }

            _newTabTextColors = GetColorStateList(defaultColor, checkedColor);
            return _newTabTextColors;
        }

        ColorStateList GetColorStateList(int defaultColor, int checkedColor)
        {
            int[][] states = new int[2][];
            int[] colors = new int[2];

            states[0] = GetSelectedStateSet();
            colors[0] = checkedColor;
            states[1] = GetEmptyStateSet();
            colors[1] = defaultColor;

            return new ColorStateList(states, colors);
        }

        int[] GetSelectedStateSet()
        {
            if (_checkedStateSet == null)
                _checkedStateSet = new int[] { global::Android.Resource.Attribute.StateChecked };

            return _checkedStateSet;
        }

        int[] GetEmptyStateSet()
        {
            if (_emptyStateSet == null)
                _emptyStateSet = GetStateSet(AView.EmptyStateSet);

            return _emptyStateSet;
        }

        int[] GetStateSet(System.Collections.Generic.IList<int> stateSet)
        {
            var results = new int[stateSet.Count];
            for (int i = 0; i < results.Length; i++)
                results[i] = stateSet[i];

            return results;
        }

        void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Element.Children.Count == 0)
            {
                bottombar.Menu.Clear();
            }
            else
            {
                bottombar.SetOnNavigationItemSelectedListener(this);
            }
        }

        void SetTabItems()
        {
            try
            {
                int startingIndex = 0;

                for (var i = startingIndex; i < Element.Children.Count; i++)
                {
                    Page child = Element.Children[i];
                    var menuItem = bottombar.Menu.Add(AMenu.None, i, i, child.Title);
                    if (Element.CurrentPage == child)
                        bottombar.SelectedItemId = menuItem.ItemId;
                }

                for (var i = 0; i < Element.Children.Count; i++)
                {
                    Page child = Element.Children[i];
                    child.Padding = new Thickness(0, 0, 0, 56);
                    var menuItem = bottombar.Menu.GetItem(i);
                    _ = ResourceManagerAndroid.ApplyDrawableAsync(Context, child, Page.IconImageSourceProperty, icon =>
                    {
                        menuItem.SetIcon(icon);
                    });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            try
            {
                if (!_isShiftModeSet)
                {
                    var children = GetAllChildViews(ViewGroup);

                    if (children.SingleOrDefault(x => x is BottomNavigationView) is BottomNavigationView bottomNav)
                    {
                        ShiftModeClass.SetShiftMode(bottomNav, false, false);
                        _isShiftModeSet = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error setting ShiftMode: {e}");
            }
        }

        private T FindChildOfType<T>(ViewGroup viewGroup) where T : Android.Views.View
        {
            if (viewGroup == null || viewGroup.ChildCount == 0) return null;

            for (var i = 0; i < viewGroup.ChildCount; i++)
            {
                var child = viewGroup.GetChildAt(i);

                var typedChild = child as T;
                if (typedChild != null) return typedChild;

                if (!(child is ViewGroup)) continue;

                var result = FindChildOfType<T>(child as ViewGroup);

                if (result != null) return result;
            }

            return null;
        }

        private List<Android.Views.View> GetAllChildViews(Android.Views.View view)
        {
            if (!(view is ViewGroup group))
            {
                return new List<Android.Views.View> { view };
            }

            var result = new List<Android.Views.View>();

            for (int i = 0; i < group.ChildCount; i++)
            {
                var child = group.GetChildAt(i);

                var childList = new List<Android.Views.View> { child };
                childList.AddRange(GetAllChildViews(child));

                result.AddRange(childList);
            }

            return result.Distinct().ToList();
        }
    }
}
