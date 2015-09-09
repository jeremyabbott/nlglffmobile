namespace nlglff

open System
open CoreGraphics
open Foundation
open UIKit
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

        addConstraints view [|filmLabel.LayoutTop == view.LayoutTop + padding
                              filmLabel.LayoutCenterX == view.LayoutCenterX
                              filmLabel.LayoutWidth == view.LayoutWidth
                              trailer.LayoutTop == filmLabel.LayoutBottom
                              trailer.LayoutCenterX == view.LayoutCenterX
                              trailer.LayoutWidth == view.LayoutWidth * adjustedWidth
                              trailer.LayoutHeight == trailer.LayoutWidth * widescreen
                              view.LayoutBottom == trailer.LayoutBottom + padding|]

        view
 
    let featuredSponsorView =
        let view = new UIView(BackgroundColor = LogoPink)
        view.Layer.CornerRadius <- nfloat 5.0
        view.Layer.MasksToBounds <- true

        let sponsors =
            let sponsorsArray = sponsorsForYear 2015
            shuffle sponsorsArray
            sponsorsArray.[..4]
            |> Array.map (fun s -> s.DisplayName)
            |> Array.reduce (fun a e -> sprintf "%s\n%s" a e)

        let sponsorsText = sponsors
        let featuredSponsorsLabel = new UILabel(Text = "Sponsored By" , TextAlignment = UITextAlignment.Center)

        featuredSponsorsLabel.BackgroundColor <- LogoPink
        featuredSponsorsLabel.TextColor <- UIColor.White
        featuredSponsorsLabel.Font <- UIFont.FromName(FontOswald, nfloat 16.)


        let sponsorsTextView =
            new UITextView(Text = sponsorsText, Editable = false, BackgroundColor = LogoPink, TextColor = UIColor.White, TextAlignment = UITextAlignment.Center)
        sponsorsTextView.Font <- UIFont.FromName(FontBrandon, nfloat 14.)

        view.AddSubviews (featuredSponsorsLabel, sponsorsTextView)


        addConstraints view [|featuredSponsorsLabel.LayoutTop == view.LayoutTop + padding
                              featuredSponsorsLabel.LayoutCenterX == view.LayoutCenterX
                              featuredSponsorsLabel.LayoutWidth == view.LayoutWidth
                              sponsorsTextView.LayoutTop == featuredSponsorsLabel.LayoutBottom + padding
                              sponsorsTextView.LayoutCenterX == view.LayoutCenterX
                              sponsorsTextView.LayoutWidth == view.LayoutWidth * adjustedWidth
                              sponsorsTextView.LayoutBottom == view.LayoutBottom - padding|]
        //addFixedHeightConstraint sponsorsTextView sponsorsTextViewHeight

        view
        
    let content (viewController : UIViewController) =
        let view = new BaseView()
        let logoButton = UIButton.FromType(UIButtonType.Custom)
        logoButton.SetImage(UIImage.FromFile("brand_logo.png"), UIControlState.Normal)
        let alert = alertAboutPace()
        alert.AddAction(UIAlertAction.Create("Thanks!", UIAlertActionStyle.Default, fun _ -> "fuck" |> ignore))

        logoButton.TouchUpInside.Add <| fun _ -> viewController.PresentViewController(alert, true, null)

        let contentWidth = UIScreen.MainScreen.Bounds.Width

        view.AddSubviews(logoButton, nextUpView, featuredSponsorView)

        addConstraints view [|logoButton.LayoutCenterX == view.LayoutCenterX
                              logoButton.LayoutTop == view.LayoutTop + topHeight
                              nextUpView.LayoutTop == logoButton.LayoutBottom + padding
                              nextUpView.LayoutCenterX == view.LayoutCenterX
                              nextUpView.LayoutWidth == view.LayoutWidth * adjustedWidth
                              nextUpView.LayoutBottom == featuredSponsorView.LayoutTop - padding
                              nextUpView.LayoutHeight == featuredSponsorView.LayoutHeight
                              featuredSponsorView.LayoutCenterX == view.LayoutCenterX
                              featuredSponsorView.LayoutWidth == view.LayoutWidth * adjustedWidth
                              featuredSponsorView.LayoutBottom == view.LayoutBottom - (viewController.TabBarController.TabBar.Frame.Height + padding)|]
        view

    override x.DidReceiveMemoryWarning () =
        // Releases the view if it doesn't have a superview.
        base.DidReceiveMemoryWarning ()
        // Release any cached data, images, etc that aren't in use.

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- "2015 NLGLFF"
        x.View <- content x