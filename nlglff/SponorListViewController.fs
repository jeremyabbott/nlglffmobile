namespace nlglff

open System
open EasyLayout
open Foundation
open UIKit
open Nlglff.Api
open WebKit
open CoreGraphics

type SponsorListViewController() = 
    inherit BaseViewController()

    let sponsorListTable = new UITableView()

    let content topHeight =
        let view = new BaseView()

        view.AddSubviews(sponsorListTable)

        sponsorListTable.BackgroundColor <- UIColor.Clear

        let padding = nfloat 10.0

        view.ConstrainLayout
            <@ [|
                sponsorListTable.Frame.Top = view.Frame.Top + topHeight
                sponsorListTable.Frame.Width = view.Frame.Width
                sponsorListTable.Frame.Bottom = view.Frame.Bottom
            |] @> |> ignore
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        let topHeight = x.NavigationController.NavigationBar.Frame.Size.Height + (nfloat 20.0)
        x.Title <- "Brought to you By"
        x.View <- (content topHeight)

        sponsorListTable.Source <- new FilmsDataSource(Nlglff.Api.loadFilms(), x.NavigationController)
        sponsorListTable.ReloadData()

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

        let sponsors = Nlglff.Api.loadSponsors() |> Array.filter (fun s -> s.Year = 2015)
             
        sponsorListTable.Source <- new SponsorsDataSource(sponsors)
        sponsorListTable.ReloadData()