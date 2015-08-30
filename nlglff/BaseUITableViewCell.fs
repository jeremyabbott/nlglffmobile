namespace nlglff

open System
open UIKit

type BaseUITableViewCell(cellId : string) as x =
    inherit UITableViewCell(UITableViewCellStyle.Default, cellId)
    do
        x.SelectionStyle <- UITableViewCellSelectionStyle.None
        x.TextLabel.TextColor <- UIColor.White
        x.TextLabel.Font <- UIFont.FromName("HelveticaNeue-Medium", nfloat 14.0)
        x.TextLabel.BackgroundColor <- (UIColor.DarkGray).ColorWithAlpha(nfloat 0.25)
        x.BackgroundColor <- (UIColor.DarkGray).ColorWithAlpha(nfloat 0.25)
        x.TextLabel.TextAlignment <- UITextAlignment.Center