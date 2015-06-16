namespace nlglff

open System
open Foundation
open UIKit
open Xamarin.Themes

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit UIApplicationDelegate ()

    override val Window = null with get, set

    // This method is invoked when the application is ready to run.
    override this.FinishedLaunching (app, options) =
        this.Window <- new UIWindow(UIScreen.MainScreen.Bounds)
        let mainViewController = new ViewController()
        this.Window.RootViewController <- new UINavigationController(mainViewController)
        this.Window.MakeKeyAndVisible()
        ProlificTheme.Apply()
        true

module Main = 
    [<EntryPoint>]
    let main args = 
        UIApplication.Main(args, null, "AppDelegate")
        0