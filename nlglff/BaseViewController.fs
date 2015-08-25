namespace nlglff

open System
open CoreGraphics
open EasyLayout
open Foundation
open UIKit

type BaseViewController () =
    inherit UIViewController ()

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()

        x.NavigationController.NavigationBar.Translucent <- true
        x.NavigationController.NavigationBar.BarStyle <- UIBarStyle.BlackOpaque

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

    override x.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation) =
        // Return true for supported orientations
        if UIDevice.CurrentDevice.UserInterfaceIdiom = UIUserInterfaceIdiom.Phone then
           toInterfaceOrientation <> UIInterfaceOrientation.PortraitUpsideDown
        else
           true