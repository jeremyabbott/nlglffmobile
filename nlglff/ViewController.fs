namespace nlglff

open Foundation
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