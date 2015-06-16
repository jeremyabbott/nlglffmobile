using System;
using System.Drawing;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace ThemeSample.UI {
	public class NavigationController : UINavigationController {
		public NavigationController (UIViewController rootViewController) : base (rootViewController)
		{
			ApplyStyles ();
		}

		void ApplyStyles ()
		{
			if (!UIDevice.CurrentDevice.CheckSystemVersion (7, 0)) {
				NavigationBar.Layer.ShadowColor = UIColor.Black.CGColor;
				NavigationBar.Layer.ShadowOffset = new SizeF (0f, 3.0f);
				NavigationBar.Layer.ShadowOpacity = 0.25f;
			}
			NavigationBar.Translucent = true;
			NavigationBar.Layer.MasksToBounds = false;
			NavigationBar.Layer.ShouldRasterize = false;
		}
	}
}

