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

    let content navHeight =
        let view = new BaseView()
        let height = (nfloat 0.1) * UIScreen.MainScreen.Bounds.Height
        let padding = (nfloat 0.25) * height
        let topHeight = UIApplication.SharedApplication.StatusBarFrame.Height

        let headerImgView = new UIImageView(UIImage.FromFile("logo_long.jpg"))

        view.AddSubviews(headerImgView, sponsorListTable)

        sponsorListTable.BackgroundColor <- UIColor.Clear
        sponsorListTable.SeparatorStyle <- UITableViewCellSeparatorStyle.None


        let headerImgViewWidth = headerImgView.Frame.Width
        view.ConstrainLayout
            <@ [|
                headerImgView.Frame.Top = view.Frame.Top + topHeight
                headerImgView.Frame.CenterX = view.Frame.CenterX

                sponsorListTable.Frame.Top = headerImgView.Frame.Bottom + padding
                sponsorListTable.Frame.Width = view.Frame.Width
                sponsorListTable.Frame.Bottom = view.Frame.Bottom - navHeight
            |] @> |> ignore
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        let navHeight = x.TabBarController.TabBar.Frame.Height

        x.View <- (content navHeight)

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