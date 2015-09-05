namespace nlglff

open System
open Foundation
open UIKit
open EasyLayout
open Nlglff.Api
open UIHelpers

[<Register("ViewController")>]
type ViewController () =
    inherit BaseViewController ()
 
    let loadContent () =
        let view = new BaseView()
        let imgView = loadImageView "date_logo.png"
        let film, time = getNowPlaying System.DateTime.Now (loadFilms())
        let labelText = sprintf "%s - %s" film.Name (time.Time.ToShortTimeString())
        let filmLabel = new UILabel(Text = labelText, TextAlignment = UITextAlignment.Center)
        let twenty = nfloat 20.0

        view.AddSubviews(imgView, filmLabel)

        view.ConstrainLayout
            <@ [|
                imgView.Frame.Top = view.Frame.Top + topHeight
                imgView.Frame.CenterX = view.Frame.CenterX

                filmLabel.Frame.Top = imgView.Frame.Bottom + topHeight
                filmLabel.Frame.CenterX = view.Frame.CenterX
                filmLabel.Frame.Width = view.Frame.Width
                filmLabel.Frame.Height = twenty

            |] @> |> ignore

        view

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "2015 NLGLFF"
        x.View <- loadContent ()