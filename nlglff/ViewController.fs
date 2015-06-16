namespace nlglff

open System
open System.Drawing
open EasyLayout
open Foundation
open Nlglff.Api
open UIKit

[<Register("ViewController")>]
type ViewController () =
    inherit UIViewController ()

    let filmListTable = new UITableView()
            
    let content =
        let view = new UIView(BackgroundColor = UIColor.White)
        let headerImgView = new UIImageView(UIImage.FromFile("brand_logo.png"))

        let filmDatesLabel = new UILabel()
        filmDatesLabel.TextAlignment <- UITextAlignment.Center
        filmDatesLabel.Text <- "September 18th - 24th"

        let footerImgView = new UIImageView(UIImage.FromFile("pace_logo.png"))
        let footerLabel = new UILabel(Text = "Developed for PACE by Jeremy Abbott",
                                        Font = UIFont.FromName("HelveticaNeue", (nfloat 14.0f)))

        (*let button = UIButton.FromType(UIButtonType.RoundedRect)
        button.SetTitle("Help", UIControlState.Normal)

        button.TouchUpInside.AddHandler
            (fun _ _ ->
                anotherLabel.Text <- "Hello world")*)

        view.AddSubviews(headerImgView, filmDatesLabel, filmListTable, footerImgView, footerLabel)

        let padding = nfloat 10.0
        let twenty = nfloat 20.0
        let height = nfloat 20.0
        let forty = nfloat 40.0
        let sixty = nfloat 60.0
        let eighty = nfloat 80.0
        let width = nfloat 100.0

        view.ConstrainLayout
            <@ [|
                headerImgView.Frame.Top = view.Frame.Top + eighty
                headerImgView.Frame.CenterX = view.Frame.CenterX

                filmDatesLabel.Frame.Top = headerImgView.Frame.Bottom + padding
                filmDatesLabel.Frame.Width = view.Frame.Width
                filmDatesLabel.Frame.Height = height
                filmDatesLabel.Frame.CenterX = view.Frame.CenterX

                filmListTable.Frame.Top = filmDatesLabel.Frame.Bottom + padding
                filmListTable.Frame.Width = view.Frame.Width - padding
                filmListTable.Frame.Bottom = footerImgView.Frame.Top - padding

                footerImgView.Frame.Height = sixty
                footerImgView.Frame.Width = sixty
                footerImgView.Frame.Top = view.Frame.Bottom - eighty
                footerImgView.Frame.Left = view.Frame.Left + twenty

                footerLabel.Frame.Left = footerImgView.Frame.Right + padding
                footerLabel.Frame.Top = footerImgView.Frame.Top
                footerLabel.Frame.Right = view.Frame.Right - padding
                footerLabel.Frame.Height = footerImgView.Frame.Height
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