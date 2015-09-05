module UIHelpers

open System
open CoreGraphics
open EasyLayout
open Foundation
open UIKit

let LogoBrown = UIColor.FromRGB(55, 39, 21)
let LogoCream = UIColor.FromRGB(255, 254, 223)
let LogoGreen = UIColor.FromRGB(144, 209, 198)
let LogoPink = UIColor.FromRGB(242, 155, 194)
let FontBrandon = "BrandonGrotesque-Regular"
let FontOswald = "Oswald"
let topHeight = UIApplication.SharedApplication.StatusBarFrame.Height

let loadImageView fileName = new UIImageView(UIImage.FromFile(fileName))

let getSectionHeader text =
    let view = new UIView()
    let label = new UILabel(TextColor = UIColor.White, BackgroundColor = LogoGreen, TextAlignment = UITextAlignment.Center)
    label.Font <- UIFont.FromName(FontOswald, nfloat 20.0)
    label.Text <- text

    view.AddSubview label

    view.ConstrainLayout
        <@ [|
            label.Frame.Width = view.Frame.Width
            label.Frame.Height = view.Frame.Height
        |] @> |> ignore

    view