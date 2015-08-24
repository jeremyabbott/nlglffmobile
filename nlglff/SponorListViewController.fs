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

    let loadContent (view : UIView) =

        view.AddSubviews(sponsorListTable)

        let padding = nfloat 10.0

        view.ConstrainLayout
            <@ [|
                sponsorListTable.Frame.Top = view.Frame.Top + padding
                sponsorListTable.Frame.Width = view.Frame.Width - padding
                sponsorListTable.Frame.Bottom = view.Frame.Bottom
            |] @> |> ignore

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "Brought to you By"

        sponsorListTable.Source <- new FilmsDataSource(Nlglff.Api.loadFilms(), x.NavigationController)
        sponsorListTable.ReloadData()

        loadContent x.ContentView

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

        let sponsors = Nlglff.Api.loadSponsors() |> Array.filter (fun s -> s.Year = 2015)
        sponsorListTable.Source <- new SponsorsDataSource(sponsors, x.NavigationController)
        sponsorListTable.ReloadData()