namespace nlglff
open System
open CoreGraphics
open EasyLayout
open Foundation
open UIKit

type BaseView() as x = 
    inherit UIView ()

    do
        x.BackgroundColor <- UIColor.White
   