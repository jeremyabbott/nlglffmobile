namespace nlglff

open System
open CoreGraphics
open EasyLayout
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
        filmListTable.SeparatorColor <- LogoGreen

        let headerImgView = new UIImageView(UIImage.FromFile("logo_long.jpg"))
        let height = UIScreen.MainScreen.Bounds.Height
        let padding = UIScreen.MainScreen.Bounds.Height
        let topHeight = UIApplication.SharedApplication.StatusBarFrame.Height
        let tableHeight = height - (headerImgView.Frame.Height + tabBarHeight)
        view.AddSubviews(headerImgView, filmListTable)



        view.ConstrainLayout
            <@ [|
                headerImgView.Frame.Top = view.Frame.Top + topHeight
                headerImgView.Frame.CenterX = view.Frame.CenterX

                filmListTable.Frame.Top = headerImgView.Frame.Bottom + topHeight
                filmListTable.Frame.Width = view.Frame.Width
                filmListTable.Frame.Bottom = view.Frame.Bottom - tabBarHeight
            |] @> |> ignore
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()

        x.Title <- "Selected Films"
        x.View <- (getContent x.TabBarController.TabBar.Frame.Height)

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

        filmListTable.Source <- new FilmsDataSource(Nlglff.Api.loadFilms(), x.NavigationController)
        filmListTable.ReloadData()