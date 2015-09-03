﻿module UIHelpers

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

let getFooter () =

    let padding = nfloat 10.0
    let fifty = nfloat 40.0

    let view = new UIView(BackgroundColor = UIColor.White)
    let footerImgView = new UIImageView(UIImage.FromFile("pace_logo.png"))
    let footerLabel = new UILabel(Text = "Presented by",
                                    Font = UIFont.FromName("HelveticaNeue", (nfloat 20.0f)),
                                    TextAlignment = UITextAlignment.Right)
    
    footerLabel.AdjustsFontSizeToFitWidth <- true
    footerLabel.MinimumFontSize <- nfloat 14.0f
    footerLabel.Lines <- nint 1

    view.AddSubviews(footerImgView, footerLabel)

    view.ConstrainLayout
        <@ [|
            footerImgView.Frame.Height = fifty
            footerImgView.Frame.Width = fifty
            footerImgView.Frame.Top = view.Frame.Top
            footerImgView.Frame.Left = view.Frame.Right - fifty

            footerLabel.Frame.Width = view.Frame.Width - fifty
            footerLabel.Frame.Height = footerImgView.Frame.Height
            footerLabel.Frame.Right = footerImgView.Frame.Left - padding
            footerLabel.Frame.CenterY = footerImgView.Frame.CenterY

        |] @> |> ignore
    view

let getHeader () =
    let view = new UIView(BackgroundColor = UIColor.White)
    let headerImgView = new UIImageView(UIImage.FromFile("brand_logo.png"))
    let filmDatesLabel = new UILabel(TextAlignment = UITextAlignment.Center,
                            Text = "September 18th - 24th")
    
    view.AddSubviews(headerImgView, filmDatesLabel)

    let eighty = nfloat 80.0
    let padding = nfloat 10.0
    let twenty = nfloat 20.0
    let height = headerImgView.Image.Size.Height + twenty

    view.ConstrainLayout
        <@ [|
            headerImgView.Frame.Top = view.Frame.Top
            headerImgView.Frame.CenterX = view.Frame.CenterX

            filmDatesLabel.Frame.Top = headerImgView.Frame.Bottom + padding
            filmDatesLabel.Frame.Width = view.Frame.Width
            filmDatesLabel.Frame.Height = twenty
            filmDatesLabel.Frame.CenterX = view.Frame.CenterX

            view.Frame.Height = height
        |] @> |> ignore
    view

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