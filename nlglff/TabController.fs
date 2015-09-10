namespace nlglff

open System
open CoreGraphics
open UIKit
open UIHelpers

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
        navController.NavigationBar.Translucent <- false
        let imgView = loadImageView "brand_logo_films.png"
        imgView.Center <- new CGPoint(navController.NavigationBar.Center.X, navController.NavigationBar.Frame.Height / nfloat 2.)
        navController.NavigationItem.TitleView <- imgView

        x.ViewControllers <- [| sponsorsController; homeController; navController;|]
        x.SelectedViewController <- homeController