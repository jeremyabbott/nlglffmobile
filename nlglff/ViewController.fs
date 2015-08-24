namespace nlglff

open System
open System.Drawing
open EasyLayout
open Foundation
open Nlglff.Api
open UIHelpers
open UIKit

[<Register("ViewController")>]
type ViewController () =
    inherit BaseViewController ()

    let filmListTable = new UITableView()
            
    let loadContent (view : UIView) =
        (*let button = UIButton.FromType(UIButtonType.RoundedRect)
        button.SetTitle("Help", UIControlState.Normal)

        button.TouchUpInside.AddHandler
            (fun _ _ ->
                anotherLabel.Text <- "Hello world")*)

        view.AddSubviews(filmListTable)

        let padding = nfloat 10.0
        let fifty = nfloat 50.0
        let eighty = nfloat 80.0
        let width = nfloat 100.0

        view.ConstrainLayout
            <@ [|
                filmListTable.Frame.Top = view.Frame.Top + padding
                filmListTable.Frame.Width = view.Frame.Width - padding
                filmListTable.Frame.Bottom = view.Frame.Bottom
            |] @> |> ignore

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "2015 NLGLFF"
        loadContent x.ContentView

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

        filmListTable.Source <- new FilmsDataSource(Nlglff.Api.loadFilms(), x.NavigationController)
        filmListTable.ReloadData()

    override x.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation) =
        // Return true for supported orientations
        if UIDevice.CurrentDevice.UserInterfaceIdiom = UIUserInterfaceIdiom.Phone then
           toInterfaceOrientation <> UIInterfaceOrientation.PortraitUpsideDown
        else
           true