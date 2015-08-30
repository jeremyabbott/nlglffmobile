namespace nlglff

open System
open System.Collections.Generic
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

        let header = new UILabel(Text = "NLGLFF Sponsors", TextAlignment = UITextAlignment.Center)
        header.Font <- UIFont.FromName("HelveticaNeue-Medium", nfloat 24.0)
        header.TextColor <- UIColor.White
        view.AddSubviews(header, sponsorListTable)

        sponsorListTable.BackgroundColor <- UIColor.Clear

        view.ConstrainLayout
            <@ [|
                header.Frame.Height = topHeight
                header.Frame.Width = view.Frame.Width
                header.Frame.Top = view.Frame.Top
                header.Frame.CenterX = view.Frame.CenterX

                sponsorListTable.Frame.Top = view.Frame.Top + topHeight
                sponsorListTable.Frame.Width = view.Frame.Width
                sponsorListTable.Frame.Bottom = view.Frame.Bottom - topHeight
            |] @> |> ignore
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        let topHeight = x.NavigationController.NavigationBar.Frame.Size.Height + (nfloat 20.0)

        x.View <- (content topHeight)
        x.NavigationController.NavigationBarHidden <- true
        x.NavigationController.HidesBarsOnSwipe <- true

        sponsorListTable.Source <- new FilmsDataSource(Nlglff.Api.loadFilms(), x.NavigationController)
        sponsorListTable.ReloadData()

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated

        let sponsors =
            let sponsorDict = Dictionary<string, Sponsor array>()

            Nlglff.Api.loadSponsors()
            |> Array.filter (fun s -> s.Year = 2015) 
            |> Array.toSeq 
            |> Seq.groupBy (fun (index : Sponsor) -> index.LevelDescription)
            |> Seq.iter (fun g -> sponsorDict.Add(fst g, (Seq.toArray (snd g))))
            sponsorDict
             
        sponsorListTable.Source <- new SponsorsDataSource(sponsors)
        sponsorListTable.ReloadData()