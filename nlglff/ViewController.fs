namespace nlglff

open System
open CoreGraphics
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
    let adjustedWidth = nfloat 0.9

    let alertAboutPace () =
        let aboutPace = "The mission of the North Louisiana Gay & Lesbian Film Festival is to recognize the important contributions that LGBT people have made to our culture; to educate the general population and raise awareness of LGBT concerns, as well as effect change in the political arena; and to investigate the history of LGBT people in film, including the stereotyping that has been a major part of this history and to counteract this stereotyping with valid, meaningful and diverse portrayals of LGBT people."
        let alert = UIAlertController.Create("About the NLGLFF", aboutPace, UIAlertControllerStyle.Alert)
        alert


    let nextUpView =
        let widescreen = nfloat 9. / nfloat 16.

        let view = new UIView(BackgroundColor = LogoGreen)
        view.Layer.CornerRadius <- nfloat 5.0
        view.Layer.MasksToBounds <- true

        let film, time = getNowPlaying System.DateTime.Now (loadFilms())
        let labelText = sprintf "Next Screening\n%s - %s @ %s" film.Name (time.Date.ToShortDateString()) (time.Time.ToShortTimeString())
        let filmLabel = new UILabel(Text = labelText, TextAlignment = UITextAlignment.Center)

        filmLabel.BackgroundColor <- LogoGreen
        filmLabel.TextColor <- UIColor.White
        filmLabel.Font <- UIFont.FromName(FontOswald, nfloat 14.)
        filmLabel.Lines <- nint 0

        let trailer = getTrailerViewForFilm film (new CGRect())
        trailer.BackgroundColor <- LogoGreen

        view.AddSubviews (filmLabel, trailer)
        filmLabel.SizeToFit()

        view.ConstrainLayout
            <@ [|
                filmLabel.Frame.Top = view.Frame.Top + padding
                filmLabel.Frame.CenterX = view.Frame.CenterX
                filmLabel.Frame.Width = view.Frame.Width
                filmLabel.Frame.Bottom = trailer.Frame.Top

                trailer.Frame.Bottom = view.Frame.Bottom - padding
                trailer.Frame.CenterX = view.Frame.CenterX
                trailer.Frame.Width = view.Frame.Width * adjustedWidth
                trailer.Frame.Height = view.Frame.Width * widescreen
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
        featuredSponsorsLabel.Font <- UIFont.FromName(FontOswald, nfloat 16.)

        let sponsorsTextView =
            new UITextView(Text = sponsorsText, Editable = false, BackgroundColor = LogoPink, TextColor = UIColor.White, TextAlignment = UITextAlignment.Center)

        sponsorsTextView.Font <- UIFont.FromName(FontBrandon, nfloat 14.)
        view.AddSubviews (featuredSponsorsLabel, sponsorsTextView)
        featuredSponsorsLabel.SizeToFit()
        let sponsorsTextViewHeight = nfloat 70.

        view.ConstrainLayout
            <@ [|
                featuredSponsorsLabel.Frame.Top = view.Frame.Top + padding
                featuredSponsorsLabel.Frame.CenterX = view.Frame.CenterX
                featuredSponsorsLabel.Frame.Width = view.Frame.Width
                featuredSponsorsLabel.Frame.Bottom = sponsorsTextView.Frame.Top

                sponsorsTextView.Frame.Height = sponsorsTextViewHeight
                sponsorsTextView.Frame.CenterX = view.Frame.CenterX
                sponsorsTextView.Frame.Width = view.Frame.Width
                sponsorsTextView.Frame.Bottom = view.Frame.Bottom - padding
            |] @> |> ignore

        view

    let loadContent (viewController : UIViewController) =
        let view = new BaseView()
        let imgView = loadImageView "brand_logo.png"
        let logoButton = UIButton.FromType(UIButtonType.Custom)
        logoButton.SetImage(UIImage.FromFile("brand_logo.png"), UIControlState.Normal)

        let alert = alertAboutPace()
        alert.AddAction(UIAlertAction.Create("Thanks!", UIAlertActionStyle.Default, fun _ -> "fuck" |> ignore))

        logoButton.TouchUpInside.Add <| fun _ -> viewController.PresentViewController(alert, true, null)

        let contentWidth = UIScreen.MainScreen.Bounds.Width

        view.AddSubviews(logoButton, nextUpView, featuredSponsorView)

        view.ConstrainLayout
            <@ [|
                logoButton.Frame.Top = view.Frame.Top + topHeight
                logoButton.Frame.CenterX = view.Frame.CenterX

                nextUpView.Frame.Top = logoButton.Frame.Bottom + padding
                nextUpView.Frame.CenterX = view.Frame.CenterX
                nextUpView.Frame.Width = view.Frame.Width * adjustedWidth

                featuredSponsorView.Frame.Top = nextUpView.Frame.Bottom + padding
                featuredSponsorView.Frame.Width = view.Frame.Width * adjustedWidth
                featuredSponsorView.Frame.CenterX = view.Frame.CenterX
                featuredSponsorView.Frame.Bottom <= view.Frame.Bottom

            |] @> |> ignore
        view

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "2015 NLGLFF"
        x.View <- loadContent x