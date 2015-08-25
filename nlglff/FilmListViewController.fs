namespace nlglff

open System
open EasyLayout
open Foundation
open UIKit
open Nlglff.Api
open WebKit
open CoreGraphics

type FilmListViewController() = 
    inherit BaseViewController()

    let filmListTable = new UITableView()

    let content =
        let view = new UIView(BackgroundColor = UIColor.White)
        view.AddSubviews(filmListTable)

        let padding = nfloat 10.0

        view.ConstrainLayout
            <@ [|
                filmListTable.Frame.Top = view.Frame.Top + padding
                filmListTable.Frame.Width = view.Frame.Width - padding
                filmListTable.Frame.Bottom = view.Frame.Bottom
            |] @> |> ignore
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "Selected Films"
        x.View <- content

        filmListTable.Source <- new FilmsDataSource(Nlglff.Api.loadFilms(), x.NavigationController)
        filmListTable.ReloadData()



    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

        filmListTable.Source <- new FilmsDataSource(Nlglff.Api.loadFilms(), x.NavigationController)
        filmListTable.ReloadData()