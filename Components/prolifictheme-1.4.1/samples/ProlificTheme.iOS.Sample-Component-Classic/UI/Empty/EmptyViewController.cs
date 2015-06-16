using System;
using Xamarin.Themes;
using System.Drawing;
#if __UNIFIED__
using UIKit;
using CoreGraphics;
using PointF=CoreGraphics.CGPoint;
#else
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
#endif

namespace ThemeSample.UI {
	public class EmptyViewController : UIViewController {
		UILabel label;

		public EmptyViewController ()
		{
			Title = "Other";
			var image = ProlificTheme.SharedTheme.ImageForTab (ThemeTab.Other);
			TabBarItem.SetFinishedImages (image, image);

			InitSubviews ();
			ApplyStyles ();
		}

		void InitSubviews ()
		{
			label = new UILabel () {
				Text = Title + " tab",
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = UIColor.Black,
				ShadowColor = UIColor.White,
				ShadowOffset = new SizeF (0, 1),
				BackgroundColor = UIColor.Clear,
				AutoresizingMask = UIViewAutoresizing.FlexibleMargins
			};
			
			label.SizeToFit ();
			label.Layer.AnchorPoint = new PointF (0.5f, 0.5f);
			label.Center = new PointF ((float)View.Frame.GetMidX (), (float)View.Frame.GetMidY () - 60);

			View.Add (label);
		}

		void ApplyStyles ()
		{
			View.BackgroundColor = UIColor.White;
		}
	}
}

