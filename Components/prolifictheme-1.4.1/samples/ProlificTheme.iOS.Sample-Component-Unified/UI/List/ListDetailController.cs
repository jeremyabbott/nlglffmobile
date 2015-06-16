using System;
using System.Drawing;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif
using Xamarin.Themes;

namespace ThemeSample {
	public class ListDetailController : UIViewController {
		UILabel date;
		UILabel title;
		UILabel location;
		
		UIImageView avatarBackground;
		UIImageView avatar;
		
		UIImageView locationPointer;
		UIImageView calendarIcon;

		public ListDetailController (RectangleF frame)
		{
			View.Frame = frame;

			Title = "Details";

			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				EdgesForExtendedLayout = UIRectEdge.None;

			InitSubviews ();
			ApplyStyles ();

			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				EdgesForExtendedLayout = UIRectEdge.None;
		}

		void InitSubviews ()
		{
			title = new UILabel (new RectangleF (91, 9, 196, 21)) {
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = UIColor.Black,
				ShadowColor = UIColor.White,
				ShadowOffset = new SizeF (0, 1),
				BackgroundColor = UIColor.Clear
			};
			
			date = new UILabel (new RectangleF (113, 51, 138, 21)) {
				Font = UIFont.SystemFontOfSize (13),
				TextColor = UIColor.Gray,
				ShadowColor = UIColor.White,
				ShadowOffset = new SizeF (0, 1),
				BackgroundColor = UIColor.Clear
			};
			
			location = new UILabel (new RectangleF (113, 30, 138, 21)) {
				Font = UIFont.SystemFontOfSize (13),
				TextColor = UIColor.Gray,
				ShadowColor = UIColor.White,
				ShadowOffset = new SizeF (0, 1),
				BackgroundColor = UIColor.Clear
			};
						
			avatarBackground = new UIImageView (new RectangleF (10, 1, 73, 73));
			locationPointer = new UIImageView (new RectangleF (91, 36, 12, 14));
			calendarIcon = new UIImageView (new RectangleF (91, 55, 14, 13));
			avatar = new UIImageView (new RectangleF (17, 7, 60, 60)) {
				ContentMode = UIViewContentMode.ScaleAspectFill,
				Image = UIImage.FromFile ("avatar.png"),
				AutosizesSubviews = true,
				ClipsToBounds = true
			};
			
			View.AddSubviews (date, title, location, avatarBackground, avatar, locationPointer, calendarIcon);
			
			avatarBackground.Image = UIImage.FromFile ("frame.png");
			locationPointer.Image = UIImage.FromFile ("location.png");
			calendarIcon.Image = UIImage.FromFile ("calendar.png");
		}

		void ApplyStyles ()
		{
			View.BackgroundColor = UIColor.FromHSBA (0, 0, 0.93f, 1);
		}

		public void BindTo (Concert concert)
		{
			title.Text = concert.Title;
			date.Text = concert.Date;
			location.Text = concert.Location;
		}
	}
}

