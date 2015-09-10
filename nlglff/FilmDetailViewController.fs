namespace nlglff

open System
open CoreGraphics
open Praeclarum.AutoLayout
open Foundation
open UIKit
open Nlglff.Api
open WebKit
open UIHelpers

type FilmDetailViewController(film: Film) = 
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

        let height = synopsis.Font.LineHeight * nfloat 5.
        synopsis.AddConstraint (NSLayoutConstraint.Create(synopsis, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual, null, NSLayoutAttribute.NoAttribute, nfloat 1., height))

        addConstraints view [|synopsisLabel.LayoutTop == view.LayoutTop + padding
                              synopsisLabel.LayoutCenterX == view.LayoutCenterX

                              synopsis.LayoutTop == synopsisLabel.LayoutBottom + padding
                              synopsis.LayoutCenterX == synopsisLabel.LayoutCenterX
                              synopsis.LayoutWidth >== view.LayoutWidth * adjustedWidth
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

        let height = showtimes.Font.LineHeight * nfloat 4.
        view.AddSubviews(showTimesLabel, showtimes)

        showtimes.AddConstraint (NSLayoutConstraint.Create(showtimes, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual, null, NSLayoutAttribute.NoAttribute, nfloat 1., height))

        addConstraints view [|showTimesLabel.LayoutTop == view.LayoutTop + padding
                              showTimesLabel.LayoutCenterX == view.LayoutCenterX

                              showtimes.LayoutTop == showTimesLabel.LayoutBottom + padding
                              showtimes.LayoutCenterX == showTimesLabel.LayoutCenterX
                              showtimes.LayoutWidth >== view.LayoutWidth * adjustedWidth
                              view.LayoutBottom == showtimes.LayoutBottom + padding|]

        view
    
    let containerView =
        let view = new UIView(BackgroundColor = UIColor.White)
        let s1 = new UIView()

        let s2 = new UIView()
        view.AddSubviews(trailerView, s1, synopsisView, s2, showtimesView)


        addConstraints view [|trailerView.LayoutTop == view.LayoutTop + padding
                              trailerView.LayoutCenterX == view.LayoutCenterX
                              trailerView.LayoutWidth == view.LayoutWidth * adjustedWidth

                              s1.LayoutTop == trailerView.LayoutBottom
                              s1.LayoutBottom <== synopsisView.LayoutTop

                              synopsisView.LayoutCenterX == view.LayoutCenterX
                              synopsisView.LayoutWidth == view.LayoutWidth * adjustedWidth

                              s2.LayoutHeight == s1.LayoutHeight
                              s2.LayoutTop == synopsisView.LayoutBottom
                              s2.LayoutBottom <== showtimesView.LayoutTop

                              showtimesView.LayoutCenterX == view.LayoutCenterX
                              showtimesView.LayoutWidth == view.LayoutWidth * adjustedWidth
                              showtimesView.LayoutBottom == view.LayoutBottom - padding @@ 1000.0f |]

        s2.AddConstraint (NSLayoutConstraint.Create(s2, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual, null, NSLayoutAttribute.NoAttribute, nfloat 1., nfloat 5.))
        view

    let scrollView = new UIScrollView()
    let content navBarHeight tabBarHeight =
        let view = new UIView(BackgroundColor = UIColor.White)

        scrollView.AddSubview containerView

        addConstraints scrollView [|containerView.LayoutTop == scrollView.LayoutTop
                                    containerView.LayoutWidth == scrollView.LayoutWidth
                                    containerView.LayoutBottom == scrollView.LayoutBottom
                                    containerView.LayoutCenterX == scrollView.LayoutCenterX
                                    containerView.LayoutHeight >== scrollView.LayoutHeight|]

        view.AddSubview scrollView

        addConstraints view [|scrollView.LayoutTop == view.LayoutTop
                              scrollView.LayoutBottom == view.LayoutBottom - tabBarHeight
                              scrollView.LayoutCenterX == view.LayoutCenterX
                              scrollView.LayoutWidth == view.LayoutWidth|]
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- film.Name
        x.View <- content x.NavigationController.NavigationBar.Frame.Height x.TabBarController.TabBar.Frame.Height
        x.SetNavBarItemTitleView "brand_logo_films.png"

    override x.ViewDidAppear (animated) =

        printfn "%A" scrollView.Frame.Top
        printfn "%A" containerView.Frame.Top
        printfn "%A" x.TabBarController.TabBar.Frame.Top
        printfn "%A" containerView.Frame.Height
        printfn "%A" scrollView.Frame.Height
        base.ViewDidAppear(animated)