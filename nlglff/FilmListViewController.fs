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
        //let tableHeight = height - (headerImgView.Frame.Height + tabBarHeight)

        view.AddSubviews(filmListTable)

        addConstraints view [|filmListTable.LayoutTop == view.LayoutTop
                              filmListTable.LayoutWidth == view.LayoutWidth
                              filmListTable.LayoutBottom == view.LayoutBottom - tabBarHeight|]
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()

        let imgView = loadImageView "brand_logo_films.png"
        let centerY = x.NavigationController.NavigationBar.Frame.Height / nfloat 2.
        imgView.Center <- new CGPoint(x.NavigationController.NavigationBar.Center.X, centerY)

        x.NavigationController.NavigationBar.Translucent <- false

        x.NavigationItem.TitleView <- imgView
        x.View <- (getContent x.TabBarController.TabBar.Frame.Height x.NavigationController.NavigationBar.Frame.Height)

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

        filmListTable.Source <- new FilmsDataSource(Nlglff.Api.loadFilms(), x)
        filmListTable.ReloadData()