namespace nlglff

open System
open System.Collections.Generic
open System.Linq
open Foundation
open Nlglff.Api
open UIKit

type SponsorsDataSource(sponsors : Dictionary<string, Sponsor array>) =
    inherit UITableViewSource()

    let cellIdentifier = "SponsorCell"
            
    let sponsorLevels = sponsors.Keys.ToArray()

    override x.GetCell (tableView, indexPath) =
        let cell =
            match tableView.DequeueReusableCell cellIdentifier with 
            | null -> new BaseUITableViewCell(cellIdentifier) :> UITableViewCell
            | cell -> cell
        
        let levelKey = sponsorLevels.[indexPath.Section]
        let row = indexPath.Row
        cell.TextLabel.Text <- sponsors.[levelKey].[row].DisplayName
        cell

    override x.NumberOfSections (tableView) = nint sponsors.Keys.Count

    override x.RowsInSection(view, section) =
        nint sponsors.[sponsorLevels.[int section]].Length

    override x.RowSelected (tableView, indexPath) = 
        tableView.DeselectRow (indexPath, false)

    override x.TitleForHeader (tableView, section) =
        sponsorLevels.[int section]