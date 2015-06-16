using System;
using System.Drawing;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif
using Xamarin.Themes;

namespace ThemeSample.UI {
	public class ButtonsViewController : UIViewController {
		public ButtonsViewController () 
		{
			Title = "Buttons";
			var image = ProlificTheme.SharedTheme.ImageForTab (ThemeTab.Buttons);
			TabBarItem.SetFinishedImages (image, image);

			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				EdgesForExtendedLayout = UIRectEdge.None;
		}

		public override void ViewDidLoad ()
		{
			ConfigureButtonWithFrame (
				new RectangleF (26, 32, 128, 37),
           		"Small Confirm",
                UIColor.FromRGB (77f/255f, 97f/255f, 13f/255f),
				ProlificTheme.SharedTheme.SmallConfirmButtomImage,
				15).AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin;
			
			
			ConfigureButtonWithFrame (
				new RectangleF (167, 32, 128, 37), 
                "Small Cancel",
                UIColor.FromRGB (142f/255, 0, 0), 
      		    ProlificTheme.SharedTheme.SmallCancelButtomImage,
				15).AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
			
			ConfigureButtonWithFrame (
				new RectangleF (26, 81, 269, 48), 
                "Large Confirm Button",
                UIColor.FromRGB (77f/255, 97f/255, 13f/255), 
		 	    ProlificTheme.SharedTheme.ConfirmButtomImage, 
                20);
			
			
			ConfigureButtonWithFrame (
				new RectangleF (26, 138, 269, 48),
                "Large Cancel Button",
                UIColor.FromRGB (142f/255, 0, 0), 
      		    ProlificTheme.SharedTheme.CancelButtomImage, 
                20);			
			
			ConfigureButtonWithFrame (
				new RectangleF (26, 193, 269, 48),
		        "Large Regular Button",
		        UIColor.FromRGB (0, 59f/255, 101f/255), 
			    ProlificTheme.SharedTheme.RegularButtomImage,  
		        20);
			
			View.BackgroundColor = UIColor.FromHSB (0, 0, 0.93f);
			
			var addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			addButton.TintColor = UIColor.FromHSB (0, 0, 0.93f);
			addButton.Clicked += OnAddButtonClicked;
			
		
			NavigationItem.SetRightBarButtonItem (addButton, true);

			base.ViewDidLoad ();
		}

		void OnAddButtonClicked (object sender, EventArgs e)
		{
			var alert = new UIAlertView ("Tab bar button clicked", "", null, "Close", null);
			alert.Show ();
		}

		UIButton ConfigureButtonWithFrame (RectangleF frame, string title, UIColor color, UIImage image, float fontSize)
		{
			var button = UIButton.FromType (UIButtonType.Custom);
			
			button.Frame = frame;
			button.SetBackgroundImage (image, UIControlState.Normal);
			button.SetTitle (title, UIControlState.Normal);
			button.SetTitleColor (color, UIControlState.Normal);
			button.SetTitleShadowColor (UIColor.White.ColorWithAlpha (0.32f), UIControlState.Normal);

			button.TitleLabel.ShadowOffset = new SizeF (0, 1);
			button.TitleLabel.Font = UIFont.BoldSystemFontOfSize (fontSize);
			button.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			
			View.Add (button);
			
			return button;
		}

	}
}

