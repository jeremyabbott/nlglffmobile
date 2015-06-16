
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
	public class ListController : UITableViewController {
		public ListController () : base (UITableViewStyle.Plain)
		{
			Title = "List";
			var image = ProlificTheme.SharedTheme.ImageForTab (ThemeTab.List);
			TabBarItem.SetFinishedImages (image, image);

			TableView.BackgroundColor = UIColor.FromRGB (192, 193, 194);
			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				EdgesForExtendedLayout = UIRectEdge.None;
		}
				
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var source = new ListSource ();

			source.TableCellSelected += OnTableCellSelected;

			TableView.Source = source;
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
		}

		void OnTableCellSelected (Concert concert)
		{
			var detailView = new ListDetailController ((RectangleF)View.Frame);
			detailView.BindTo (concert);

			NavigationController.PushViewController (detailView, true);
		}
	}
}

