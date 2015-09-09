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

    let getContent tabBarHeight =
        let view = new UIView(BackgroundColor = UIColor.White)

        filmListTable.TableFooterView <- new UIView(CGRect.Empty) // hides the footer and thus empty cells
        filmListTable.SeparatorStyle <- UITableViewCellSeparatorStyle.SingleLine
        filmListTable.SeparatorColor <- LogoGreen

        let headerImgView = loadImageView "brand_logo_films.png"

        let height = UIScreen.MainScreen.Bounds.Height
        let padding = UIScreen.MainScreen.Bounds.Height
        let topHeight = UIApplication.SharedApplication.StatusBarFrame.Height
        let tableHeight = height - (headerImgView.Frame.Height + tabBarHeight)

        view.AddSubviews(headerImgView, filmListTable)

        addConstraints view [|headerImgView.LayoutTop == view.LayoutTop + topHeight
                              headerImgView.LayoutCenterX == view.LayoutCenterX
                              filmListTable.LayoutTop == headerImgView.LayoutBottom + nfloat 10.
                              filmListTable.LayoutWidth == view.LayoutWidth
                              filmListTable.LayoutBottom == view.LayoutBottom - tabBarHeight|]
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()

        x.Title <- "Selected Films"
        x.View <- (getContent x.TabBarController.TabBar.Frame.Height)

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

        filmListTable.Source <- new FilmsDataSource(Nlglff.Api.loadFilms(), x)
        filmListTable.ReloadData()