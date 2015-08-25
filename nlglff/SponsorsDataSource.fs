namespace nlglff

open System
open Foundation
open Nlglff.Api
open UIKit

type SponsorsDataSource(sponsorSource : Sponsor array) =
    inherit UITableViewSource()

    let cellIdentifier = "SponsorCell"
    let setCellProperties (cell : UITableViewCell) =
        cell.TextLabel.TextColor <- UIColor.White
        cell.TextLabel.Font <- UIFont.FromName("HelveticaNeue-Medium", nfloat 14.0)
        cell.TextLabel.BackgroundColor <- (UIColor.DarkGray).ColorWithAlpha(nfloat 0.5)
        cell.BackgroundColor <- UIColor.Clear
        cell

    override x.GetCell(view, indexPath) : UITableViewCell =
        let sponsor = sponsorSource.[int indexPath.Row]

        let cell =
            match view.DequeueReusableCell cellIdentifier with 
            | null -> new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier)
            | cell -> cell
        
        setCellProperties cell |> ignore
        cell.TextLabel.Text <- sprintf "%s - %s" sponsor.LevelDescription sponsor.DisplayName
        cell

    override x.RowsInSection(view, section) = nint sponsorSource.Length

    override x.RowSelected (tableView, indexPath) = 
        tableView.DeselectRow (indexPath, false)