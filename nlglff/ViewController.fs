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
    inherit UIViewController ()

    let filmListTable = new UITableView()
            
    let content =
        let view = new UIView(BackgroundColor = UIColor.White)
        let header = UIHelpers.getHeader ()
        let footer = UIHelpers.getFooter ()

        (*let button = UIButton.FromType(UIButtonType.RoundedRect)
        button.SetTitle("Help", UIControlState.Normal)

        button.TouchUpInside.AddHandler
            (fun _ _ ->
                anotherLabel.Text <- "Hello world")*)

        view.AddSubviews(header, filmListTable, footer)

        let padding = nfloat 10.0
        let twenty = nfloat 20.0
        let height = nfloat 20.0
        let forty = nfloat 40.0
        let fifty = nfloat 50.0
        let sixty = nfloat 60.0
        let eighty = nfloat 80.0
        let width = nfloat 100.0

        view.ConstrainLayout
            <@ [|
                header.Frame.Top = view.Frame.Top + eighty
                header.Frame.CenterX = view.Frame.CenterX

                filmListTable.Frame.Top = header.Frame.Bottom + padding
                filmListTable.Frame.Width = view.Frame.Width - padding
                filmListTable.Frame.Bottom = footer.Frame.Top - padding

                footer.Frame.Width = view.Frame.Width - padding
                footer.Frame.Left = view.Frame.Left
                footer.Frame.Bottom = view.Frame.Bottom
                footer.Frame.Height = fifty
                //footer.Frame.Top = view.Frame.Bottom - fifty
                //footer.Frame.Right = view.Frame.Right - padding
            |] @> |> ignore
        view

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.View <- content
        x.Title <- "2015 NLGLFF"

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