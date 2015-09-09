namespace nlglff

open System
open CoreGraphics
open Praeclarum.AutoLayout
open Foundation
open UIKit
open Nlglff.Api
open WebKit
open UIHelpers

type FilmDetailViewController(film: Film) as x = 
    inherit UIViewController()

    let twenty = nfloat 20.
    let fifty = nfloat 50.
    let eighty = nfloat 80.
    let fontHeight = nfloat 14.
    let height = nfloat 20.
    let labelWidth = nfloat 65.
    let padding = nfloat 10.
    let adjustedWidth = nfloat 0.9

    let trailerView =
        let widescreen = nfloat 9. / nfloat 16.
        let view = new UIView(BackgroundColor = LogoGreen)
        view.Layer.CornerRadius <- nfloat 5.0
        view.Layer.MasksToBounds <- true

        let title = new UILabel(Text = film.Name, TextAlignment = UITextAlignment.Center, TextColor = UIColor.White)
        title.Font <- UIFont.FromName(FontOswald, twenty)
        title.SizeToFit()

        let trailer = getTrailerViewForFilm film view.Frame

        view.AddSubviews(title, trailer)

        addConstraints view [|title.LayoutTop == view.LayoutTop + padding
                              title.LayoutCenterX == view.LayoutCenterX

                              trailer.LayoutTop == title.LayoutBottom + padding
                              trailer.LayoutCenterX == view.LayoutCenterX
                              trailer.LayoutWidth == view.LayoutWidth * adjustedWidth
                              trailer.LayoutHeight == trailer.LayoutWidth * widescreen

                              view.LayoutBottom == trailer.LayoutBottom + padding|]

        view

    let synopsisView =
        let view = new UIView(BackgroundColor = LogoPink)
        view.Layer.CornerRadius <- nfloat 5.0
        view.Layer.MasksToBounds <- true

        let synopsisLabel =
            new UILabel(Text = "Synopsis", Font = UIFont.FromName(FontOswald, fontHeight), TextColor = UIColor.White)
        synopsisLabel.SizeToFit()

        let synopsis = new UITextView(Text = film.Synopsis, Editable = false, BackgroundColor = LogoPink)

        view.AddSubviews(synopsisLabel, synopsis)

        addConstraints view [|synopsisLabel.LayoutTop == view.LayoutTop + padding
                              synopsisLabel.LayoutCenterX == view.LayoutCenterX

                              synopsis.LayoutTop == synopsisLabel.LayoutBottom + padding
                              synopsis.LayoutCenterX == synopsisLabel.LayoutCenterX
                              synopsis.LayoutWidth == view.LayoutWidth * adjustedWidth
                              synopsis.LayoutBottom == view.LayoutBottom - padding|]

        view

    let showtimesView =
        let view = new UIView(BackgroundColor = LogoGreen)
        view.Layer.CornerRadius <- nfloat 5.0
        view.Layer.MasksToBounds <- true

        let showTimesLabel =
            new UILabel(Text = "Showtimes", Font = UIFont.FromName(FontOswald, fontHeight), TextColor = UIColor.White)
        showTimesLabel.SizeToFit()

        let showtimeText =
            film.Showtimes
            |> Array.map (fun s -> sprintf "%s @ %s" (s.Date.ToShortDateString()) (s.Time.ToShortTimeString()))
            |> Array.reduce (fun acc item -> sprintf "%s\n%s" acc item)
        
        let showtimes = new UITextView(Text = showtimeText, Editable = false, BackgroundColor = LogoGreen, TextAlignment = UITextAlignment.Center)

        view.AddSubviews(showTimesLabel, showtimes)

        addConstraints view [|showTimesLabel.LayoutTop == view.LayoutTop + padding
                              showTimesLabel.LayoutCenterX == view.LayoutCenterX

                              showtimes.LayoutTop == showTimesLabel.LayoutBottom + padding
                              showtimes.LayoutCenterX == showTimesLabel.LayoutCenterX
                              showtimes.LayoutWidth == view.LayoutWidth * adjustedWidth
                              showtimes.LayoutBottom == view.LayoutBottom - padding|]

        view
    
    let containerView =
        let view = new UIView(BackgroundColor = UIColor.White)
        view.AddSubviews()

        addConstraints view [|
                              |]
        //addFixedHeightConstraint showtimesView (((view.Frame.Bottom - trailerView.Frame.Bottom) - padding ) * nfloat 0.5)
        view

    //let scrollView = new UIScrollView()
    let content navBarHeight =
        let view = new UIView(BackgroundColor = UIColor.White)

        let headerImgView = loadImageView "brand_logo_films.png"
        let backButton = UIButton.FromType(UIButtonType.RoundedRect)

        backButton.SetTitle("Back to Films List", UIControlState.Normal)
        backButton.TouchUpInside.Add <| fun _ -> x.DismissViewController(false, null)

        view.AddSubviews(headerImgView, trailerView, synopsisView, showtimesView, backButton)
        //scrollView.AddSubview(containerView)




        addConstraints view [|trailerView.LayoutTop == view.LayoutTop + (padding + navBarHeight)
                              trailerView.LayoutCenterX == view.LayoutCenterX
                              trailerView.LayoutWidth == view.LayoutWidth * adjustedWidth

                              synopsisView.LayoutTop == trailerView.LayoutBottom + padding
                              synopsisView.LayoutCenterX == view.LayoutCenterX
                              synopsisView.LayoutWidth == view.LayoutWidth * adjustedWidth
                              synopsisView.LayoutHeight == showtimesView.LayoutHeight

                              showtimesView.LayoutTop == synopsisView.LayoutBottom + padding
                              showtimesView.LayoutCenterX == view.LayoutCenterX
                              showtimesView.LayoutWidth == view.LayoutWidth * adjustedWidth
                              showtimesView.LayoutBottom == backButton.LayoutTop - padding

                              backButton.LayoutCenterX == view.LayoutCenterX
                              backButton.LayoutBottom == view.LayoutBottom - padding|]
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- film.Name
        x.View <- content x.NavigationController.NavigationBar.Frame.Height

    override x.ViewDidAppear (animated) =
        //let containerHeight = nfloat (containerView.Subviews |> Array.map (fun (v: UIView) -> float v.Frame.Height) |> Array.sum)
        //let scrollViewHeight = containerHeight + (padding * nfloat 2.)
        //scrollView.ContentSize <- CGRect(nfloat 0., nfloat 0., content.Frame.Width, scrollViewHeight).Size
        
        base.ViewDidAppear(animated)