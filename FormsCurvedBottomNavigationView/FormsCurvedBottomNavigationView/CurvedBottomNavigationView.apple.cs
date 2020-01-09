using CoreAnimation;
using CoreGraphics;
using System;
using UIKit;

namespace FormsCurvedBottomNavigation
{
    public class CurvedBottomNavigationView : UITabBar
    {
        public CALayer shapeLayer = new CALayer();
        public UIColor BarBackgroundColor { get; set; }
        public void AddShape()
        {
            var shapeLayer = new CAShapeLayer();

            shapeLayer.Path = createPath();

            shapeLayer.StrokeColor = UIColor.White.CGColor;

            if (BarBackgroundColor == null)
                shapeLayer.FillColor = UIColor.FromRGB(135, 206, 235).CGColor;
            else
                shapeLayer.FillColor = BarBackgroundColor.CGColor;

            shapeLayer.LineWidth = 0;

            var oldshapeLayer = new CALayer();

            if (oldshapeLayer == this.shapeLayer)
            {
                this.Layer.ReplaceSublayer(oldshapeLayer, shapeLayer);

            }
            else
            {
                this.Layer.InsertSublayer(shapeLayer, 0);

            }

            this.shapeLayer = shapeLayer;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            this.AddShape();
        }

        public CGPath createPath()
        {
            nfloat height = (nfloat)37.0;

            var path = new UIBezierPath();

            var centerWidth = this.Frame.Width / 2;

            path.MoveTo(new CGPoint(x: 0, y: 0)); // start top left

            path.AddLineTo(new CGPoint((centerWidth - height * 2), 0)); // the beginning of the trough
                                                                        // first curve down
            path.AddCurveToPoint(new CGPoint(x: centerWidth, y: height),
                      controlPoint1: new CGPoint(x: (centerWidth - 30), y: 0), controlPoint2: new CGPoint(x: centerWidth - 35, y: height));
            // second curve up
            path.AddCurveToPoint(new CGPoint(x: (centerWidth + height * 2), y: 0),
                      controlPoint1: new CGPoint(x: centerWidth + 35, y: height), controlPoint2: new CGPoint(x: (centerWidth + 30), y: 0));

            // complete the rect
            path.AddLineTo(new CGPoint(x: this.Frame.Width, y: 0));

            path.AddLineTo(new CGPoint(x: this.Frame.Width, y: this.Frame.Height));

            path.AddLineTo(new CGPoint(x: 0, y: this.Frame.Height));

            path.ClosePath();


            return path.CGPath;
        }

        public override bool PointInside(CGPoint point, UIEvent uievent)
        {
            //var buttonRadius = 35;
            //return (this.Center.X - point.X) > buttonRadius || point.Y > buttonRadius;
            return true;
        }
    }
}
