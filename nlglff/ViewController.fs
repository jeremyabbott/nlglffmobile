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

    let height = nfloat 20.0

    let navButton title  (navController : UINavigationController) (controller : UIViewController) =
        let button = UIButton.FromType(UIButtonType.RoundedRect)
        button.SetTitle(title, UIControlState.Normal)

        button.TouchUpInside.AddHandler
            (fun _ _ ->
                navController.PushViewController(controller, true))
        button

    let loadContent (navController : UINavigationController) =
        let view = new BaseView()

        let filmButton = navButton "Films" navController (new FilmListViewController())
        let sponsorButton = navButton "Sponsors" navController (new SponsorListViewController())

        view.AddSubviews(filmButton, sponsorButton)

        let padding = nfloat 10.0
        let topHeight = navController.NavigationBar.Frame.Size.Height + padding

        view.ConstrainLayout
            <@ [|
                filmButton.Frame.Top = view.Frame.Top + topHeight
                filmButton.Frame.Width = view.Frame.Width - padding
                filmButton.Frame.Height = height
                filmButton.Frame.CenterX = view.Frame.CenterX

                sponsorButton.Frame.Top = filmButton.Frame.Bottom + padding
                sponsorButton.Frame.Width = filmButton.Frame.Width - padding
                sponsorButton.Frame.Height = height
                sponsorButton.Frame.CenterX = view.Frame.CenterX
            |] @> |> ignore
        view

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "2015 NLGLFF"
        x.View <- loadContent x.NavigationController

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

    override x.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation) =
        // Return true for supported orientations
        if UIDevice.CurrentDevice.UserInterfaceIdiom = UIUserInterfaceIdiom.Phone then
           toInterfaceOrientation <> UIInterfaceOrientation.PortraitUpsideDown
        else
           true