﻿namespace nlglff

open System
open UIKit

type TabController () =
    inherit UITabBarController()

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        let sponsorsController = new SponsorListViewController(Title = "Sponsors")
        sponsorsController.TabBarItem.Image <- UIImage.FromFile("dollar.png")
        let filmsController = new FilmListViewController(Title = "Films")
        filmsController.TabBarItem.Image <- UIImage.FromFile("films.png")
        let homeController = new ViewController()
        homeController.TabBarItem.Image <- UIImage.FromFile("home.png")

        let navController = new UINavigationController(filmsController)
        x.ViewControllers <- [| sponsorsController; homeController; navController;|]
        x.SelectedViewController <- homeController