namespace nlglff

open System
open EasyLayout
open Foundation
open UIHelpers
open UIKit

type TabController () as x =
    inherit UITabBarController()

    let sponsorsController = new SponsorListViewController(Title = "Sponsors")
    let filmsController = new FilmListViewController(Title = "Films")
    let homeController = new ViewController()

    do
        x.ViewControllers <- [| sponsorsController; homeController; filmsController|]
        x.SelectedViewController <- homeController
