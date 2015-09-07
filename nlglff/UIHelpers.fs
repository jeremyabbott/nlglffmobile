module UIHelpers

open System
open CoreGraphics
open EasyLayout
open Foundation
open Nlglff.Api
open UIKit
open WebKit

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

let getTrailerViewForFilm (film : Film) (size : CGRect) =
    let view = new WKWebView(size, new WKWebViewConfiguration())

    let url =
        match film.TrailerUrl with
        | Some u -> u
        | None -> String.Empty
    
    let request = new NSUrlRequest(new NSUrl(sprintf "http:%s?modestbranding=1" url))
    view.LoadRequest(request) |> ignore
    view

// shuffle an array (in-place)
let shuffle a =
    let rand = new System.Random()

    let swap (a: _[]) x y =
        let tmp = a.[x]
        a.[x] <- a.[y]
        a.[y] <- tmp

    Array.iteri (fun i _ -> swap a i (rand.Next(i, Array.length a))) a