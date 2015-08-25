namespace nlglff
open System
open CoreGraphics
open EasyLayout
open Foundation
open UIKit

type BaseView() as x = 
    inherit UIView ()
    let backgroundImageView = new UIImageView(UIImage.FromFile("background2.jpg"))
    do
        x.BackgroundColor <- UIColor.Clear
        x.AddSubview backgroundImageView
