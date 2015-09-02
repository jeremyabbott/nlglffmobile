namespace nlglff

open System
open EasyLayout
open Foundation
open UIHelpers
open UIKit

type TabController () =
    inherit UITabBarController()

   

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        let sponsorsController = new SponsorListViewController(Title = "Sponsors")
        let filmsController = new FilmListViewController(Title = "Films")
        let homeController = new ViewController()
        let navController = new UINavigationController(filmsController)

        x.ViewControllers <- [| sponsorsController; homeController; navController;|]
        x.SelectedViewController <- homeController