namespace nlglff

open System
open Foundation
open Nlglff.Api
open UIKit

type SponsorsDataSource(sponsorSource : Sponsor array) =
    inherit UITableViewSource()

    let cellIdentifier = "SponsorCell"

    override x.GetCell(view, indexPath) : UITableViewCell =
        let sponsor = sponsorSource.[int indexPath.Row]

        let cell =
            match view.DequeueReusableCell cellIdentifier with 
            | null -> new BaseUITableViewCell(cellIdentifier) :> UITableViewCell
            | cell -> cell
        
        cell.TextLabel.Text <- sprintf "%s - %s" sponsor.LevelDescription sponsor.DisplayName
        cell

    override x.RowsInSection(view, section) = nint sponsorSource.Length

    override x.RowSelected (tableView, indexPath) = 
        tableView.DeselectRow (indexPath, false)