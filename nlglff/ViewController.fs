namespace nlglff

open System
open Foundation
open UIKit
open EasyLayout
open Nlglff.Api
open UIHelpers

[<Register("ViewController")>]
type ViewController () =
    inherit BaseViewController ()

    let padding = nfloat 10.0
    let twenty = nfloat 20.

    let nextUpView =
        let widescreen = nfloat 16. / nfloat 9.

        let view = new UIView(BackgroundColor = LogoGreen)
        view.Layer.CornerRadius <- nfloat 5.0
        view.Layer.MasksToBounds <- true

        let film, time = getNowPlaying System.DateTime.Now (loadFilms())
        let labelText = sprintf "Next Screening\n%s - %s @ %s" film.Name (time.Date.ToShortDateString()) (time.Time.ToShortTimeString())
        let filmLabel = new UILabel(Text = labelText, TextAlignment = UITextAlignment.Center)
        filmLabel.SizeToFit()
        filmLabel.BackgroundColor <- LogoGreen
        filmLabel.TextColor <- UIColor.White
        filmLabel.Font <- UIFont.FromName(FontOswald, twenty)
        filmLabel.Lines <- nint 0

        let trailer = getTrailerViewForFilm film view.Frame
        trailer.BackgroundColor <- LogoGreen

        view.AddSubviews (filmLabel, trailer)

        view.ConstrainLayout
            <@ [|
                filmLabel.Frame.Top = view.Frame.Top + padding
                filmLabel.Frame.CenterX = view.Frame.CenterX
                filmLabel.Frame.Width = view.Frame.Width

                trailer.Frame.Top = filmLabel.Frame.Bottom + padding
                trailer.Frame.Bottom = view.Frame.Bottom - padding
                trailer.Frame.CenterX = view.Frame.CenterX
                trailer.Frame.Width = trailer.Frame.Height * widescreen
            |] @> |> ignore
        view
 
    let featuredSponsorView =
        let view = new UIView(BackgroundColor = LogoPink)
        view.Layer.CornerRadius <- nfloat 5.0
        view.Layer.MasksToBounds <- true

        let random = Random()
        let sponsors =
            let sponsorsArray = sponsorsForYear 2015
            shuffle sponsorsArray
            sponsorsArray

        let sponsorsText = sprintf "%s\n%s\n%s" sponsors.[0].DisplayName sponsors.[1].DisplayName sponsors.[2].DisplayName
        let featuredSponsorsLabel = new UILabel(Text = "Sponsored By" , TextAlignment = UITextAlignment.Center)

        featuredSponsorsLabel.BackgroundColor <- LogoPink
        featuredSponsorsLabel.TextColor <- UIColor.White
        featuredSponsorsLabel.Font <- UIFont.FromName(FontOswald, twenty)
        featuredSponsorsLabel.SizeToFit()

        let sponsorsTextView =
            new UITextView(Text = sponsorsText, Editable = false, BackgroundColor = LogoPink, TextColor = UIColor.White, TextAlignment = UITextAlignment.Center)

        sponsorsTextView.Font <- UIFont.FromName(FontBrandon, twenty)
        sponsorsTextView.SizeToFit()
        view.AddSubviews (featuredSponsorsLabel, sponsorsTextView)

        view.ConstrainLayout
            <@ [|
                featuredSponsorsLabel.Frame.Top = view.Frame.Top + padding
                featuredSponsorsLabel.Frame.CenterX = view.Frame.CenterX
                featuredSponsorsLabel.Frame.Width = view.Frame.Width

                sponsorsTextView.Frame.CenterX = view.Frame.CenterX
                sponsorsTextView.Frame.Width = view.Frame.Width
                sponsorsTextView.Frame.Top = featuredSponsorsLabel.Frame.Bottom + padding
                sponsorsTextView.Frame.Bottom = view.Frame.Bottom - padding
            |] @> |> ignore

        view

    let loadContent tabBarHeight =
        let view = new BaseView()
        let imgView = loadImageView "brand_logo.png"
        let adjustedScreen = UIScreen.MainScreen.Bounds.Height - (tabBarHeight + topHeight)
        let viewHeight = ((adjustedScreen - imgView.Frame.Height) / nfloat 2.) - (padding * nfloat 2.)
        let adjustedWidth = nfloat 0.9

        view.AddSubviews(imgView, nextUpView, featuredSponsorView)

        view.ConstrainLayout
            <@ [|
                imgView.Frame.Top = view.Frame.Top + topHeight
                imgView.Frame.CenterX = view.Frame.CenterX

                nextUpView.Frame.Top = imgView.Frame.Bottom + padding
                nextUpView.Frame.CenterX = view.Frame.CenterX
                nextUpView.Frame.Width = view.Frame.Width * adjustedWidth
                nextUpView.Frame.Height = viewHeight

                featuredSponsorView.Frame.Top = nextUpView.Frame.Bottom + padding
                featuredSponsorView.Frame.Height = viewHeight
                featuredSponsorView.Frame.Width = view.Frame.Width * adjustedWidth
                featuredSponsorView.Frame.CenterX = view.Frame.CenterX

            |] @> |> ignore
        view

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "2015 NLGLFF"
        x.View <- loadContent x.TabBarController.TabBar.Frame.Height