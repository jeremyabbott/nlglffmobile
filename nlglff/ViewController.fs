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
            
    let loadContent (view : UIView) (navController : UINavigationController) =
        let filmButton = UIButton.FromType(UIButtonType.RoundedRect)
        filmButton.SetTitle("Films", UIControlState.Normal)

        filmButton.TouchUpInside.AddHandler
            (fun _ _ ->
                navController.PushViewController(new FilmListViewController(), true))

        view.AddSubviews(filmButton)

        let padding = nfloat 10.0

        view.ConstrainLayout
            <@ [|
                filmButton.Frame.Top = view.Frame.Top + padding
                filmButton.Frame.Width = view.Frame.Width - padding
                filmButton.Frame.Bottom = view.Frame.Bottom
            |] @> |> ignore

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "2015 NLGLFF"
        loadContent x.ContentView x.NavigationController

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

    override x.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation) =
        // Return true for supported orientations
        if UIDevice.CurrentDevice.UserInterfaceIdiom = UIUserInterfaceIdiom.Phone then
           toInterfaceOrientation <> UIInterfaceOrientation.PortraitUpsideDown
        else
           true