namespace nlglff

open System
open CoreGraphics
open Praeclarum.AutoLayout
open Foundation
open Nlglff.Api
open UIHelpers
open UIKit

type FilmListViewController() = 
    inherit BaseViewController()

    let filmListTable = new UITableView(BackgroundColor = LogoPink)

    let getContent tabBarHeight navBarHeight =
        let view = new UIView(BackgroundColor = UIColor.White)

        filmListTable.TableFooterView <- new UIView(CGRect.Empty) // hides the footer and thus empty cells
        filmListTable.SeparatorStyle <- UITableViewCellSeparatorStyle.SingleLine
        filmListTable.SeparatorColor <- LogoGreen

        let height = UIScreen.MainScreen.Bounds.Height
        let padding = UIScreen.MainScreen.Bounds.Height
        let topHeight = UIApplication.SharedApplication.StatusBarFrame.Height

        view.AddSubviews(filmListTable)

        addConstraints view [|filmListTable.LayoutTop == view.LayoutTop
                              filmListTable.LayoutWidth == view.LayoutWidth
                              filmListTable.LayoutBottom == view.LayoutBottom - tabBarHeight|]
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()

        x.SetNavBarItemTitleView "brand_logo_films.png"
        x.View <- (getContent x.TabBarController.TabBar.Frame.Height x.NavigationController.NavigationBar.Frame.Height)

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

        filmListTable.Source <- new FilmsDataSource(Nlglff.Api.loadFilms(), x)
        filmListTable.ReloadData()