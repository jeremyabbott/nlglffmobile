
using System;
using System.Drawing;
using System.Threading.Tasks;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

using Xamarin.Themes;

namespace ThemeSample.UI {
	public class ColorChoiceViewController : UIViewController {
		UILabel descriptionLabel;
		UILabel hueLabel;
		UILabel hueValueLabel;
		UILabel hueNumericValueLabel;

		UILabel saturationLabel;
		UILabel saturationValueLabel;
		UILabel saturationNumericValueLabel;

		UIImageView colorImageView;

		UIButton button;

		UISlider hueSlider;
		UISlider saturationSlider;

		public override void ViewDidLoad ()
		{
			InitSubviews ();
			ApplyStyles ();

		}

		private void InitSubviews ()
		{
			descriptionLabel = new UILabel (new RectangleF (32, 38, 245, 56)){
				Font = UIFont.SystemFontOfSize (17),
				TextColor = UIColor.Gray,
				BackgroundColor = UIColor.Clear,
				Text = "Move the sliders to choose your color",
				Lines = 2
			};
			
			hueLabel = new UILabel (new RectangleF (32, 190, 198, 21)) {
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = UIColor.Gray,
				BackgroundColor = UIColor.Clear,
				Text = "Hue"
			};
			
			hueValueLabel = new UILabel (new RectangleF (160, 190, 52, 21)) {
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = UIColor.Gray,
				BackgroundColor = UIColor.Clear,
				Text = "value:"
			};
			
			hueNumericValueLabel = new UILabel (new RectangleF (211, 190, 66, 21)) {
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = UIColor.Gray,
				TextAlignment = UITextAlignment.Right,
				BackgroundColor = UIColor.Clear,
				Text = "0.0000", 
				AdjustsFontSizeToFitWidth = true
			};			
			
			saturationLabel = new UILabel (new RectangleF (32, 276, 198, 21)) {
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = UIColor.Gray,
				BackgroundColor = UIColor.Clear,
				Text = "Saturation"
			};
			
			saturationValueLabel = new UILabel (new RectangleF (160, 276, 52, 21)) {
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = UIColor.Gray,
				BackgroundColor = UIColor.Clear,
				Text = "value:"
			};
			
			saturationNumericValueLabel = new UILabel (new RectangleF (211, 276, 66, 21)) {
				Font = UIFont.BoldSystemFontOfSize (17),
				TextColor = UIColor.Gray,
				TextAlignment = UITextAlignment.Right,
				BackgroundColor = UIColor.Clear,
				Text = "1.0000",
				AdjustsFontSizeToFitWidth = true
			};

			colorImageView = new UIImageView (new RectangleF (32, 110, 245, 59)) {
				Image = ProlificTheme.SharedTheme.NavigationBackgroundForBarMetrics (UIBarMetrics.Default)
			};
			
			button = new UIButton (new RectangleF (32, 363, 245, 37)) {
				Font = UIFont.BoldSystemFontOfSize (15),
				TitleShadowOffset = new SizeF (0, -1)
			};

			button.SetTitle ("Done", UIControlState.Normal);
			button.SetTitleColor (UIColor.White, UIControlState.Normal);
			button.SetTitleShadowColor (UIColor.Gray, UIControlState.Normal);
			button.SetBackgroundImage (ProlificTheme.SharedTheme.ButtonBackgroundForState (UIControlState.Normal), UIControlState.Normal);

			hueSlider = new UISlider (new RectangleF (30, 224, 249, 23)) {
				MinValue = -3.14f,
				MaxValue = 3.14f,
				Value = 0
			};

			saturationSlider = new UISlider (new RectangleF (30, 305, 249, 23)) {
				MinValue = 0,
				MaxValue = 2,
				Value = 1
			};

			// Applies theme styles on specified element
			ProlificTheme.Apply (hueSlider);
			ProlificTheme.Apply (saturationSlider);			

			hueSlider.ValueChanged += ColorParametersValueChanged;
			saturationSlider.ValueChanged += ColorParametersValueChanged;

			button.TouchUpInside += OnDoneTouched;

			View.AddSubviews (descriptionLabel, hueLabel, hueValueLabel, hueNumericValueLabel);
			View.AddSubviews (saturationLabel, saturationValueLabel, saturationNumericValueLabel);
			View.AddSubviews (hueSlider, saturationSlider, colorImageView, button);
		}

		void OnDoneTouched (object sender, EventArgs e)
		{
			//Applies theme styles globally
			ProlificTheme.Apply ();

			var tabBarController = new UITabBarController () {
				ViewControllers = new UIViewController[] {
					new NavigationController (new ElementsViewController ()),
					new NavigationController (new ButtonsViewController ()),
					new NavigationController (new ListController ()),
					new NavigationController (new EmptyViewController ())
				}
			};

			PresentViewController (tabBarController, true, null);

			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
		}

		void ColorParametersValueChanged (object sender, EventArgs e)
		{
			hueNumericValueLabel.Text = Math.Round (hueSlider.Value, 4).ToString ("0.0000");
			saturationNumericValueLabel.Text = Math.Round (saturationSlider.Value, 4).ToString ("0.0000");

			var hue = hueSlider.Value;
			var saturation = saturationSlider.Value;

			colorImageView.Image = FilteredImage (hue, saturation);
		}

		public UIImage FilteredImage (float hue, float saturation)
		{
			ProlificTheme.SharedTheme.Hue = hue;
			ProlificTheme.SharedTheme.Saturation = saturation;
			
			return ProlificTheme.SharedTheme.NavigationBackgroundForBarMetrics (UIBarMetrics.Default);
		}

		void ApplyStyles ()
		{
			View.BackgroundColor = UIColor.FromRGB (242, 242, 242);
		}
	}
}

