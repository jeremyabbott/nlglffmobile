namespace nlglff

open System
open CoreGraphics
open Foundation
open UIKit
open EasyLayout
open Praeclarum.AutoLayout
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

    (*let nextUpView =
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

        view.AddConstraint(createTopConstraint filmLabel view padding)
        view.AddConstraint(createBottomConstraint trailer view (padding * nfloat -1.))
        view.AddConstraint(createEqualConstraint NSLayoutAttribute.Bottom NSLayoutAttribute.Top filmLabel trailer (nfloat 0.))

        view.ConstrainLayout
            <@ [|
                filmLabel.Frame.CenterX = view.Frame.CenterX
                filmLabel.Frame.Width = view.Frame.Width

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

        view.AddConstraint(createTopConstraint featuredSponsorsLabel view padding)
        view.AddConstraint(createBottomConstraint sponsorsTextView view (padding * nfloat -1.))
        view.AddConstraint(createEqualConstraint NSLayoutAttribute.Bottom NSLayoutAttribute.Top featuredSponsorsLabel sponsorsTextView (nfloat 0.))

        view.ConstrainLayout
            <@ [|
                featuredSponsorsLabel.Frame.CenterX = view.Frame.CenterX
                featuredSponsorsLabel.Frame.Width = view.Frame.Width

                sponsorsTextView.Frame.Height = sponsorsTextViewHeight
                sponsorsTextView.Frame.CenterX = view.Frame.CenterX
                sponsorsTextView.Frame.Width = view.Frame.Width
            |] @> |> ignore

        view
    *)

        
    let content =
        let view = new BaseView()
        let logoButton = UIButton.FromType(UIButtonType.Custom)
        logoButton.SetImage(UIImage.FromFile("brand_logo.png"), UIControlState.Normal)
        logoButton.TranslatesAutoresizingMaskIntoConstraints <- false




        let contentWidth = UIScreen.MainScreen.Bounds.Width
        view.AddSubview logoButton
        let leading = nfloat 5.
        view.AddConstraints [|logoButton.LayoutLeading == view.LayoutLeading + leading
                              logoButton.LayoutCenterX == view.LayoutCenterX
                              logoButton.LayoutTop == view.LayoutTop + topHeight|]
        (*
        view.AddSubviews(logoButton, nextUpView, featuredSponsorView)
        view.AddConstraint(createTopConstraint logoButton view topHeight)
        view.AddConstraint(createEqualConstraint NSLayoutAttribute.Top NSLayoutAttribute.Bottom nextUpView logoButton (nfloat 0.))
        view.AddConstraint(createEqualConstraint NSLayoutAttribute.Top NSLayoutAttribute.Bottom featuredSponsorView nextUpView (nfloat 0.))
        view.AddConstraint(createEqualConstraint NSLayoutAttribute.Bottom NSLayoutAttribute.Top featuredSponsorView view (nfloat 0.))

        view.ConstrainLayout
            <@ [|
                logoButton.Frame.CenterX = view.Frame.CenterX

                nextUpView.Frame.CenterX = view.Frame.CenterX
                nextUpView.Frame.Width = view.Frame.Width * adjustedWidth

                featuredSponsorView.Frame.Width = view.Frame.Width * adjustedWidth
                featuredSponsorView.Frame.CenterX = view.Frame.CenterX

            |] @> |> ignore       
            *) 
        view

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "2015 NLGLFF"
        x.View <- content

    override x.ViewDidAppear (animated) =
        base.ViewDidAppear(animated)

