namespace nlglff

open System
open System.Collections.Generic
open EasyLayout
open Foundation
open Nlglff.Api
open UIHelpers
open UIKit

type SponsorListViewController() = 
    inherit BaseViewController()

    let sponsorListTable = new UITableView()

    let content navHeight =
        let view = new BaseView()
        let headerImgView = new UIImageView(UIImage.FromFile("brand_logo_sponsors.png"))

        view.AddSubviews(headerImgView, sponsorListTable)

        sponsorListTable.BackgroundColor <- LogoPink
        sponsorListTable.SeparatorStyle <- UITableViewCellSeparatorStyle.None

        view.ConstrainLayout
            <@ [|
                headerImgView.Frame.Top = view.Frame.Top + topHeight
                headerImgView.Frame.CenterX = view.Frame.CenterX

                sponsorListTable.Frame.Top = headerImgView.Frame.Bottom + topHeight
                sponsorListTable.Frame.Width = view.Frame.Width
                sponsorListTable.Frame.Bottom = view.Frame.Bottom - navHeight
            |] @> |> ignore
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        let navHeight = x.TabBarController.TabBar.Frame.Height

        x.View <- (content navHeight)

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