using System;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace ThemeSample.UI {
	public class TextFieldDelegate : UITextFieldDelegate {
		const float AnimationDuration = 0.3f;
		const float MinScrollFraction = 0.1f;
		const float MaxScrollFraction = 0.9f;
		const float PortraitKeyboardHeight = 216;
		const float LandscapeKeyboardHeight = 140;
		
		float animatedDistance;			
		
		public override bool ShouldReturn (UITextField textField)
		{
			textField.ResignFirstResponder ();
			
			return true;
		}
		
		public override void EditingStarted (UITextField textField)
		{
			var textFieldRect = textField.Superview.Window.ConvertRectFromView (textField.Frame, textField);
			var viewRect = textField.Superview.Window.ConvertRectFromView (textField.Superview.Bounds, textField.Superview);
			
			var midLine = textFieldRect.Y + 0.5f * textFieldRect.Height;
			var numerator = midLine - viewRect.Y - MinScrollFraction * viewRect.Height;
			var denominator = (MaxScrollFraction - MinScrollFraction) * viewRect.Height;
			var heightFraction = numerator / denominator;
			
			if (heightFraction < 0.0f) 
				heightFraction = 0;
			
			if (heightFraction > 1.0f)
				heightFraction = 1;
			
			var statusBarOrientation = UIApplication.SharedApplication.StatusBarOrientation;
			var portraitOrientation = statusBarOrientation == UIInterfaceOrientation.Portrait || statusBarOrientation == UIInterfaceOrientation.PortraitUpsideDown;
			
			if (portraitOrientation)
				animatedDistance = (float)Math.Floor (PortraitKeyboardHeight * heightFraction);
			else
				animatedDistance = (float)Math.Floor (LandscapeKeyboardHeight * heightFraction);
			
			var viewFrame = textField.Superview.Frame;
			viewFrame.Y -= animatedDistance;
			
			UIView.Animate (AnimationDuration, 0, UIViewAnimationOptions.BeginFromCurrentState, () => {
				textField.Superview.Frame = viewFrame;
			}, null);
		}
		
		public override void EditingEnded (UITextField textField)
		{
			var viewFrame = textField.Superview.Frame;
			viewFrame.Y += animatedDistance;
			
			UIView.Animate (AnimationDuration, 0, UIViewAnimationOptions.BeginFromCurrentState, () => {
				textField.Superview.Frame = viewFrame;
			}, null);
		}
		
		public override bool ShouldBeginEditing (UITextField textField)
		{
			return true;
		}
		
		public override bool ShouldEndEditing (UITextField textField)
		{
			return true;
		}
	}
}

