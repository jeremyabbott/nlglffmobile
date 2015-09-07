namespace nlglff

open System
open CoreGraphics
open EasyLayout
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

        view.ConstrainLayout
            <@ [|
                title.Frame.Top = view.Frame.Top + padding
                title.Frame.CenterX = view.Frame.CenterX

                trailer.Frame.Top = title.Frame.Bottom
                trailer.Frame.CenterX = view.Frame.CenterX
                trailer.Frame.Width = view.Frame.Width * adjustedWidth
                trailer.Frame.Height = trailer.Frame.Width * widescreen

                view.Frame.Bottom = trailer.Frame.Bottom + padding
            |] @> |> ignore
        
        view

    let synopsisView =
        let view = new UIView(BackgroundColor = LogoPink)
        view.Layer.CornerRadius <- nfloat 5.0
        view.Layer.MasksToBounds <- true

        let synopsisLabel =
            new UILabel(Text = "Synopsis", Font = UIFont.FromName(FontOswald, fontHeight), TextColor = UIColor.White)
        synopsisLabel.SizeToFit()
        let synopsis = new UITextView(Text = film.Synopsis, Editable = false, BackgroundColor = LogoPink)

        synopsis.SizeToFit()
        view.AddSubviews(synopsisLabel, synopsis)

        view.ConstrainLayout
            <@ [|
                synopsisLabel.Frame.Top = view.Frame.Top + padding
                synopsisLabel.Frame.CenterX = view.Frame.CenterX

                synopsis.Frame.Top = synopsisLabel.Frame.Bottom + padding
                synopsis.Frame.CenterX = view.Frame.CenterX
                synopsis.Frame.Width = view.Frame.Width * adjustedWidth
                synopsis.Frame.Height = eighty

                view.Frame.Bottom = synopsis.Frame.Bottom + padding
            |] @> |> ignore
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

        view.ConstrainLayout
            <@ [|
                showTimesLabel.Frame.Top = view.Frame.Top + padding
                showTimesLabel.Frame.CenterX = view.Frame.CenterX

                showtimes.Frame.Top = showTimesLabel.Frame.Bottom + padding
                showtimes.Frame.CenterX = view.Frame.CenterX
                showtimes.Frame.Width = view.Frame.Width * adjustedWidth
                showtimes.Frame.Height = fifty

                view.Frame.Bottom = showtimes.Frame.Bottom + padding
            |] @> |> ignore
        view
    
    let containerView =
        let view = new UIView(BackgroundColor = UIColor.White)
        view.AddSubviews(trailerView, synopsisView, showtimesView)

        view.ConstrainLayout
            <@ [|
                trailerView.Frame.Top = view.Frame.Top
                trailerView.Frame.CenterX = view.Frame.CenterX
                trailerView.Frame.Width = view.Frame.Width * adjustedWidth

                synopsisView.Frame.Top = trailerView.Frame.Bottom + padding
                synopsisView.Frame.CenterX = view.Frame.CenterX
                synopsisView.Frame.Width = view.Frame.Width * adjustedWidth

                showtimesView.Frame.Top = synopsisView.Frame.Bottom + padding
                showtimesView.Frame.CenterX = view.Frame.CenterX
                showtimesView.Frame.Width = view.Frame.Width * adjustedWidth
            |] @> |> ignore
        view

    let scrollView = new UIScrollView()
    let content =
        let view = new UIView(BackgroundColor = UIColor.White)

        let headerImgView = loadImageView "brand_logo_films.png"
        let backButton = UIButton.FromType(UIButtonType.RoundedRect)

        backButton.SetTitle("Back to Films List", UIControlState.Normal)
        backButton.TouchUpInside.Add <| fun _ -> x.DismissViewController(false, null)

        view.AddSubviews(headerImgView, scrollView, backButton)
        scrollView.AddSubview(containerView)

        scrollView.ConstrainLayout
            <@ [|
                containerView.Frame.Top = scrollView.Frame.Top
                containerView.Frame.Width = scrollView.Frame.Width
                containerView.Frame.CenterX = scrollView.Frame.CenterX
            |] @> |> ignore

        view.ConstrainLayout
            <@ [|
                headerImgView.Frame.Top = view.Frame.Top + topHeight
                headerImgView.Frame.CenterX = view.Frame.CenterX

                scrollView.Frame.Top = headerImgView.Frame.Bottom + padding
                scrollView.Frame.Width = view.Frame.Width
                scrollView.Frame.Bottom = backButton.Frame.Top - padding
                scrollView.Frame.CenterX = view.Frame.CenterX

                backButton.Frame.CenterX = view.Frame.CenterX
                backButton.Frame.Bottom = view.Frame.Bottom - padding
            |] @> |> ignore
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- film.Name
        x.View <- content

    override x.ViewDidAppear (animated) =
        let containerHeight = nfloat (containerView.Subviews |> Array.map (fun (v: UIView) -> float v.Frame.Height) |> Array.sum)
        let scrollViewHeight = containerHeight + (padding * nfloat 2.)
        scrollView.ContentSize <- CGRect(nfloat 0., nfloat 0., content.Frame.Width, scrollViewHeight).Size
        
        base.ViewDidAppear(animated)