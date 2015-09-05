namespace nlglff

open Foundation
open UIKit
open EasyLayout
open UIHelpers

[<Register("ViewController")>]
type ViewController () =
    inherit BaseViewController ()
 
    let loadContent () =
        let view = new BaseView()
        let imgView = loadImageView "date_logo.png"

        view.AddSubviews(imgView)

        view.ConstrainLayout
            <@ [|
                imgView.Frame.Top = view.Frame.Top + topHeight
                imgView.Frame.CenterX = view.Frame.CenterX

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