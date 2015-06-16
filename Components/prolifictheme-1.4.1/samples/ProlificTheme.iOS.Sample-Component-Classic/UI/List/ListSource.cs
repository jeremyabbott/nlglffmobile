
using System;
using System.Drawing;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

using System.Collections.Generic;

namespace ThemeSample.UI {
	public class ListSource : UITableViewSource {
		public event Action<Concert> TableCellSelected;

		List<Concert> concerts;

		public ListSource ()
		{
			concerts = new List<Concert> {
				new Concert (),
				new Concert (),
				new Concert ()				
			};
		}

		#if __UNIFIED__
		public override nint NumberOfSections (UITableView tableView)
		#else
		public override int NumberOfSections (UITableView tableView)
		#endif
		{
			return 1;
		}
		
		#if __UNIFIED__
		public override nint RowsInSection (UITableView tableview, nint section)
		#else
		public override int RowsInSection (UITableView tableview, int section)
		#endif
		{
			return 3;
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (ListCell.Key) as ListCell;
			if (cell == null)
				cell = new ListCell ();

			var data = concerts[indexPath.Row];

			cell.Title.Text = data.Title;
			cell.Location.Text = data.Location;
			cell.Date.Text = data.Date;

			cell.Avatar.Image = UIImage.FromFile ("avatar.png");

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (TableCellSelected != null)
				TableCellSelected (concerts[indexPath.Row]);
		}

		#if __UNIFIED__
		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		#else
		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		#endif
		{
			return 77;
		}
	}
}

