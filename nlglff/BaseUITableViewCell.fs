namespace nlglff

open System
open Foundation
open UIKit
open UIHelpers

type BaseUITableViewCell(cellId : string) as x =
    inherit UITableViewCell(UITableViewCellStyle.Default, cellId)
    do
        x.SelectionStyle <- UITableViewCellSelectionStyle.None
        x.TextLabel.TextColor <- UIColor.White
        x.TextLabel.Font <- UIFont.FromName(FontBrandon, nfloat 16.0)
        x.TextLabel.BackgroundColor <- UIColor.White
        x.BackgroundColor <- LogoPink
        x.TextLabel.TextAlignment <- UITextAlignment.Center
        x.PreservesSuperviewLayoutMargins <- false
        x.SeparatorInset <- UIEdgeInsets.Zero
        x.LayoutMargins <- UIEdgeInsets.Zero