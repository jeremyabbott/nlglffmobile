namespace nlglff

open System
open CoreGraphics
open EasyLayout
open Foundation
open UIKit

type BaseViewController () =
    inherit UIViewController ()

    let padding = nfloat 10.0
    let fifty = nfloat 50.0
    let eighty = nfloat 80.0
    let width = nfloat 100.0

    let content =
        let view = new UIView(BackgroundColor = UIColor.White)
        let containingView = new UIView(BackgroundColor = UIColor.White) // other views add their content here.
        let header = UIHelpers.getHeader ()
        let footer = UIHelpers.getFooter ()

        view.AddSubviews(header, containingView, footer)

        view.ConstrainLayout
            <@ [|
                header.Frame.Top = view.Frame.Top + eighty
                header.Frame.CenterX = view.Frame.CenterX

                containingView.Frame.Width = view.Frame.Width
                containingView.Frame.Top = header.Frame.Bottom
                containingView.Frame.Bottom = footer.Frame.Top

                footer.Frame.Width = view.Frame.Width - padding
                footer.Frame.Left = view.Frame.Left
                footer.Frame.Bottom = view.Frame.Bottom
                footer.Frame.Height = fifty
            |] @> |> ignore
        view

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.View <- content

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

    override x.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation) =
        // Return true for supported orientations
        if UIDevice.CurrentDevice.UserInterfaceIdiom = UIUserInterfaceIdiom.Phone then
           toInterfaceOrientation <> UIInterfaceOrientation.PortraitUpsideDown
        else
           true