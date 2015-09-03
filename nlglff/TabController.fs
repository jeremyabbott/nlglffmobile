namespace nlglff

open System
open UIKit

type TabController () =
    inherit UITabBarController()

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        let sponsorsController = new SponsorListViewController(Title = "Sponsors")
        let filmsController = new FilmListViewController(Title = "Films")
        let homeController = new ViewController()

        x.ViewControllers <- [| sponsorsController; homeController; filmsController;|]
        x.SelectedViewController <- homeController