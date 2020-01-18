using Android.Content;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using System.ComponentModel;

namespace FormsCurvedBottomNavigation
{
    internal class CurvedBottomNavigationView : BottomNavigationView, INotifyPropertyChanged
    {
        private Android.Graphics.Color _barColor;
        public Android.Graphics.Color BarColor
        {
            get { return _barColor; }
            set
            {
                _barColor = value;
                OnPropertyChanged("BarColor");
            }
        }

        private Path mPath;
        private Paint mPaint;

        private int CURVE_CIRCLE_RADIUS;
        // the coordinates of the first curve
        private Android.Graphics.Point mFirstCurveStartPoint = new Android.Graphics.Point();
        private Android.Graphics.Point mFirstCurveEndPoint = new Android.Graphics.Point();
        private Android.Graphics.Point mFirstCurveControlPoint1 = new Android.Graphics.Point();
        private Android.Graphics.Point mFirstCurveControlPoint2 = new Android.Graphics.Point();

        //the coordinates of the second curve
        private Android.Graphics.Point mSecondCurveStartPoint = new Android.Graphics.Point();
        private Android.Graphics.Point mSecondCurveEndPoint = new Android.Graphics.Point();
        private Android.Graphics.Point mSecondCurveControlPoint1 = new Android.Graphics.Point();
        private Android.Graphics.Point mSecondCurveControlPoint2 = new Android.Graphics.Point();
        private int mNavigationBarWidth;
        private int mNavigationBarHeight;
        public CurvedBottomNavigationView(Context context) : base(context)
        {
            init();
        }

        public CurvedBottomNavigationView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            init();
        }

        public CurvedBottomNavigationView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            init();
        }

        private void init()
        {
            mPath = new Path();
            mPaint = new Paint();
            mPaint.SetStyle(Paint.Style.FillAndStroke);
            mPaint.Color = Android.Graphics.Color.LightBlue;
            SetBackgroundColor(Android.Graphics.Color.Transparent);

            this.Measure((int)MeasureSpecMode.Unspecified, (int)MeasureSpecMode.Unspecified);
            CURVE_CIRCLE_RADIUS = this.MeasuredHeight / 2;
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            mNavigationBarWidth = Width;
            mNavigationBarHeight = Height;
            // the coordinates (x,y) of the start point before curve
            mFirstCurveStartPoint.Set((mNavigationBarWidth / 2) - (CURVE_CIRCLE_RADIUS * 2) - (CURVE_CIRCLE_RADIUS / 3), 0);
            // the coordinates (x,y) of the end point after curve
            mFirstCurveEndPoint.Set(mNavigationBarWidth / 2, CURVE_CIRCLE_RADIUS + (CURVE_CIRCLE_RADIUS / 4));
            // same thing for the second curve
            mSecondCurveStartPoint = mFirstCurveEndPoint;
            mSecondCurveEndPoint.Set((mNavigationBarWidth / 2) + (CURVE_CIRCLE_RADIUS * 2) + (CURVE_CIRCLE_RADIUS / 3), 0);

            // the coordinates (x,y)  of the 1st control point on a cubic curve
            mFirstCurveControlPoint1.Set(mFirstCurveStartPoint.X + CURVE_CIRCLE_RADIUS + (CURVE_CIRCLE_RADIUS / 4), mFirstCurveStartPoint.Y);
            // the coordinates (x,y)  of the 2nd control point on a cubic curve
            mFirstCurveControlPoint2.Set(mFirstCurveEndPoint.X - (CURVE_CIRCLE_RADIUS * 2) + CURVE_CIRCLE_RADIUS, mFirstCurveEndPoint.Y);

            mSecondCurveControlPoint1.Set(mSecondCurveStartPoint.X + (CURVE_CIRCLE_RADIUS * 2) - CURVE_CIRCLE_RADIUS, mSecondCurveStartPoint.Y);
            mSecondCurveControlPoint2.Set(mSecondCurveEndPoint.X - (CURVE_CIRCLE_RADIUS + (CURVE_CIRCLE_RADIUS / 4)), mSecondCurveEndPoint.Y);

            mPath.Reset();
            mPath.MoveTo(0, 0);
            mPath.LineTo(mFirstCurveStartPoint.X, mFirstCurveStartPoint.Y);

            mPath.CubicTo(mFirstCurveControlPoint1.X, mFirstCurveControlPoint1.Y,
                    mFirstCurveControlPoint2.X, mFirstCurveControlPoint2.Y,
                    mFirstCurveEndPoint.X, mFirstCurveEndPoint.Y);

            mPath.CubicTo(mSecondCurveControlPoint1.X, mSecondCurveControlPoint1.Y,
                    mSecondCurveControlPoint2.X, mSecondCurveControlPoint2.Y,
                    mSecondCurveEndPoint.X, mSecondCurveEndPoint.Y);

            mPath.LineTo(mNavigationBarWidth, 0);
            mPath.LineTo(mNavigationBarWidth, mNavigationBarHeight);
            mPath.LineTo(0, mNavigationBarHeight);
            mPath.Close();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            canvas.DrawPath(mPath, mPaint);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            mPaint.Color = BarColor;
        }
    }
}
