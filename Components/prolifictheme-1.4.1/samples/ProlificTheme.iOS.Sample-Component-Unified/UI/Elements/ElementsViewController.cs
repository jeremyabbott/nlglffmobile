
using System;
using System.Drawing;
using System.Collections.Generic;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif
using Xamarin.Themes;
using Xamarin.Controls.iOS.ProgressBar;
using Xamarin.Controls.iOS.SegmentedControl;
using Xamarin.Controls.iOS.Switch;

namespace ThemeSample.UI {
	public partial class ElementsViewController : UIViewController {
		UIScrollView scrollView;

		PopoverProgressBar progressBar;
		UISlider slider;

		public ElementsViewController ()
		{
			Title = "Elements";
			var image = ProlificTheme.SharedTheme.ImageForTab (ThemeTab.Elemets);
			TabBarItem.SetFinishedImages (image, image);

			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0)) {
				EdgesForExtendedLayout = UIRectEdge.None;
				ExtendedLayoutIncludesOpaqueBars = false;
			}
		}
		
		public override void ViewDidLoad ()
		{
			InitSubviews ();
			ApplyStyles ();
		}

		void InitSubviews ()
		{	
			scrollView = new UIScrollView (View.Bounds) {
				BackgroundColor = UIColor.Clear,
				AutoresizingMask = UIViewAutoresizing.All,
				ContentSize = View.Bounds.Size
			};
			
			progressBar = new PopoverProgressBar (new RectangleF (10, 30, 292, 28));
			progressBar.Progress = 0.5f;
			progressBar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			
			scrollView.Add (progressBar);

			
			var label = new UILabel (new RectangleF (10, 70, 292, 28)) {
				Text = "Move slider below to see progress",

				//Font = UIFont.SystemFontOfSize (14),
				BackgroundColor = UIColor.Clear,
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
			};
			
			scrollView.Add (label);
			
			slider = new UISlider (new RectangleF (10, 110, 292, 28)) {
				MaxValue = 1,
				MinValue = 0,
				Value = 0.5f,
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
			};
			
			slider.ValueChanged += OnValueChanged;

			// Applies theme styles on specified element			
			ProlificTheme.Apply (slider);
			
			scrollView.Add (slider);
			
			var segments = new List<object> (3) { "Featured", "Top Charts", "Categories" };
			
			var segmentControl = new SegmentedControl (segments) {
				Frame = new RectangleF (10, 140, 292, 44),
				SelectedIndex = 1,
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
			};
			
			scrollView.Add (segmentControl);
			
			var switchControl = new SwitchOnOff (new RectangleF (120, 230, 80, 27));
			switchControl.SetState (true, false);
			switchControl.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;

			scrollView.Add (switchControl);
			
			var textField = new UITextField (new RectangleF (10, 300, 292, 31)) {
				LeftView = new UIView (new RectangleF (0, 0, 5, 20)),
				LeftViewMode = UITextFieldViewMode.Always,
				Text = "Type your text!",
				TextColor = UIColor.DarkGray,
				VerticalAlignment = UIControlContentVerticalAlignment.Center,
				Background = ProlificTheme.SharedTheme.TextFielBackground,
				Delegate = new TextFieldDelegate (),
				Font = UIFont.SystemFontOfSize (14),
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
				//this.textField.text = "";

			};

			scrollView.Add (textField);

			InitLabels ();

			View.Add (scrollView);

			base.ViewDidLoad ();
		}

		void ApplyStyles ()
		{
			View.BackgroundColor = UIColor.FromHSBA (0, 0, 0.93f, 1);
		}

		void InitLabels ()
		{
			var color = 81f/255f;

			var textLabel = new UILabel (new RectangleF (15, 260, 200, 30)){
				TextColor = UIColor.FromRGB (color, color, color),
				ShadowColor = UIColor.White,
				ShadowOffset = new SizeF (0, 1),
				BackgroundColor = UIColor.Clear,
				Font = UIFont.BoldSystemFontOfSize (20),
				Text = "Label",
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
			};

			scrollView.Add (textLabel);
		}

		void OnValueChanged (object sender, EventArgs e)
		{
			progressBar.Progress = slider.Value;
		}
	}
}

