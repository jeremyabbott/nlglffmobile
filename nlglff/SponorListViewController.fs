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
        let height = (nfloat 0.1) * UIScreen.MainScreen.Bounds.Height
        let padding = (nfloat 0.25) * height

        let headerImgView = new UIImageView(UIImage.FromFile("logo_long.jpg"))
        let headerLabel = new UILabel(Text = "Sponsors", TextAlignment = UITextAlignment.Center)
        headerLabel.Font <- UIFont.FromName("HelveticaNeue-Medium", nfloat 36.0)
        headerLabel.TextColor <- UIColor.Black
        headerLabel.AdjustsFontSizeToFitWidth <- true

        view.AddSubviews(headerImgView, headerLabel, sponsorListTable)

        sponsorListTable.BackgroundColor <- UIColor.Clear
        sponsorListTable.SeparatorStyle <- UITableViewCellSeparatorStyle.None


        let headerImgViewWidth = headerImgView.Frame.Width
        view.ConstrainLayout
            <@ [|
                headerImgView.Frame.Top = view.Frame.Top + topHeight
                headerImgView.Frame.Left = view.Frame.Left

                headerLabel.Frame.Height = headerImgView.Frame.Height
                headerLabel.Frame.Width = view.Frame.Width - headerImgViewWidth
                headerLabel.Frame.Bottom = headerImgView.Frame.Bottom
                headerLabel.Frame.Left = headerImgView.Frame.Right

                sponsorListTable.Frame.Top = headerLabel.Frame.Bottom + padding
                sponsorListTable.Frame.Width = view.Frame.Width
                sponsorListTable.Frame.Bottom = view.Frame.Bottom - topHeight
            |] @> |> ignore
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        let topHeight = UIApplication.SharedApplication.StatusBarFrame.Height

        x.View <- (content topHeight)

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