namespace nlglff

open System
open Foundation
open Nlglff.Api
open UIHelpers
open UIKit

type FilmsDataSource(filmSource: Film array, parent: UIViewController) =
    inherit UITableViewSource()

    let cellIdentifier = "FilmCell"

    override x.GetCell(view, indexPath) : UITableViewCell =
        let film = filmSource.[int indexPath.Row]

        let cell =
            match view.DequeueReusableCell cellIdentifier with 
            | null -> new BaseUITableViewCell(cellIdentifier) :> UITableViewCell
            | cell -> cell

        cell.TextLabel.Text <- film.Name
        cell.Accessory <- UITableViewCellAccessory.DetailButton
        cell.TintColor <- UIColor.White
        cell

    override x.RowsInSection(view, section) = nint filmSource.Length

    override x.RowSelected (tableView, indexPath) = 
        tableView.DeselectRow (indexPath, false)

    override x.AccessoryButtonTapped (tableView, indexPath) =
        parent.PresentViewController(new FilmDetailViewController(filmSource.[int indexPath.Item]), false, null)
        tableView.DeselectRow (indexPath, false)
