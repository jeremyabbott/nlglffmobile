namespace nlglff

open System
open Foundation
open Nlglff.Api
open UIKit

type FilmsDataSource(filmSource: Film array, navigation: UITabBarController) =
    inherit UITableViewSource()

    let cellIdentifier = "FilmCell"

    override x.GetCell(view, indexPath) : UITableViewCell =
        let film = filmSource.[int indexPath.Row]

        let cell =
            match view.DequeueReusableCell cellIdentifier with 
            | null -> new BaseUITableViewCell(cellIdentifier) :> UITableViewCell
            | cell -> cell

        cell.TextLabel.Text <- film.Name
        cell

    override x.RowsInSection(view, section) = nint filmSource.Length

    override x.RowSelected (tableView, indexPath) = 
        tableView.DeselectRow (indexPath, false)
        //navigation.PushViewController (new FilmDetailViewController(filmSource.[int indexPath.Item]), true)
