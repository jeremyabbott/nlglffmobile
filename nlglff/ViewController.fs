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

 
    let loadContent () =
        let view = new BaseView()


        view

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "2015 NLGLFF"
        x.View <- loadContent ()

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

    override x.ShouldAutorotateToInterfaceOrientation (toInterfaceOrientation) =
        // Return true for supported orientations
        if UIDevice.CurrentDevice.UserInterfaceIdiom = UIUserInterfaceIdiom.Phone then
           toInterfaceOrientation <> UIInterfaceOrientation.PortraitUpsideDown
        else
           true