
using System;
using System.Drawing;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

using Xamarin.Themes;

namespace ThemeSample.UI {
	public class ListCell : UITableViewCell {
		public UILabel Date { get; private set; }
		public UILabel Title { get; private set; }
		public UILabel Location { get; private set; }

		public UIImageView AvatarBackground { get; set; }
		public UIImageView Avatar { get; set; }

		public UIImageView LocationPointer { get; set; }
		public UIImageView CalendarIcon { get; set; }

		public UIImageView Background { get; set; }		

		public static readonly NSString Key = new NSString ("ListCell");
		
		public ListCell () : base (UITableViewCellStyle.Default, Key)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
			Accessory = UITableViewCellAccessory.None;

			InitSubviews ();
			ApplyStyles ();
		}

		void InitSubviews () 
		{
			Title = new UILabel (new RectangleF (91, 9, 196, 21)) {
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = UIColor.Black,
				ShadowColor = UIColor.White,
				ShadowOffset = new SizeF (0, 1),
				BackgroundColor = UIColor.Clear,
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
			};

			Date = new UILabel (new RectangleF (113, 51, 138, 21)) {
				Font = UIFont.SystemFontOfSize (13),
				TextColor = UIColor.Gray,
				ShadowColor = UIColor.White,
				ShadowOffset = new SizeF (0, 1),
				BackgroundColor = UIColor.Clear,
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
			};

			Location = new UILabel (new RectangleF (113, 30, 138, 21)) {
				Font = UIFont.SystemFontOfSize (13),
				TextColor = UIColor.Gray,
				ShadowColor = UIColor.White,
				ShadowOffset = new SizeF (0, 1),
				BackgroundColor = UIColor.Clear,
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
			};
			
			Background = new UIImageView (new RectangleF (0, 0, 320, 77));
			Background.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

			AvatarBackground = new UIImageView (new RectangleF (10, 1, 73, 73));
			LocationPointer = new UIImageView (new RectangleF (91, 36, 12, 14));
			CalendarIcon = new UIImageView (new RectangleF (91, 55, 14, 13));
			Avatar = new UIImageView (new RectangleF (17, 7, 60, 60)) {
				ContentMode = UIViewContentMode.ScaleAspectFill,
				AutosizesSubviews = true,
				ClipsToBounds = true
			};
			
			AddSubviews (Background, Date, Title, Location, AvatarBackground, Avatar, LocationPointer, CalendarIcon);

			AvatarBackground.Image = UIImage.FromFile ("frame.png");
			LocationPointer.Image = UIImage.FromFile ("location.png");
			CalendarIcon.Image = UIImage.FromFile ("calendar.png");
			Background.Image = ProlificTheme.SharedTheme.TableCellBackground;
		}

		void ApplyStyles ()
		{
		}
	}
}

