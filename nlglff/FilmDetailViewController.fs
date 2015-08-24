namespace nlglff

open System
open EasyLayout
open Foundation
open UIKit
open Nlglff.Api
open WebKit
open CoreGraphics

type FilmDetailViewController(film: Film) = 
    inherit BaseViewController()

    let seventy = nfloat 70.0
    let eighty = nfloat 80.0
    let fontHeight = nfloat 12.0
    let height = nfloat 20.0
    let padding = nfloat 15.0
    let labelWidth = nfloat 65.0
    let adjustment = nfloat 4.0
    let trailerHeight = (nfloat 9.0) / (nfloat 16.0)

    let getTrailerViewForFilm (film : Film) (view : UIView) =
        let view = new WKWebView(view.Frame, new WKWebViewConfiguration())

        let url =
            match film.TrailerUrl with
            | Some u -> u
            | None -> String.Empty
        
        let request = new NSUrlRequest(new NSUrl(sprintf "http:%s" url))
        view.LoadRequest(request) |> ignore
        view

    let widthLabel = new UILabel(Text = "Width Label", Font = UIFont.FromName("HelveticaNeue-Medium", fontHeight))

    let loadContent (view : UIView) =

        let title = new UILabel(Text = film.Name, TextAlignment = UITextAlignment.Center)
        let synopsisLabel = new UILabel(Text = "Synopsis", Font = UIFont.FromName("HelveticaNeue-Medium", fontHeight))
        let synopsis = new UITextView(Text = film.Synopsis, Editable = false)
        let showtimeLabel = new UILabel(Text = "Showtimes", Font = UIFont.FromName("HelveticaNeue-Medium", fontHeight))
        let showtimeText =
            film.Showtimes
            |> Array.map (fun s -> sprintf "%s @ %s" (s.Date.ToShortDateString()) (s.Time.ToShortTimeString()))
            |> Array.reduce (fun acc item -> sprintf "%s\n%s" acc item)
        
        let showtimes = new UITextView(Text = showtimeText, Editable = false)

        let trailer = getTrailerViewForFilm film view

        view.AddSubviews(title, synopsisLabel, synopsis, showtimeLabel, showtimes, trailer)

        view.ConstrainLayout
            <@ [|
                title.Frame.Top = view.Frame.Top
                title.Frame.CenterX = view.Frame.CenterX
                title.Frame.Height = height
                title.Frame.Width = view.Frame.Width

                trailer.Frame.Top = title.Frame.Bottom + padding
                trailer.Frame.CenterX = view.Frame.CenterX
                trailer.Frame.Width = view.Frame.Width - padding
                trailer.Frame.Height = trailer.Frame.Width * trailerHeight 

                synopsisLabel.Frame.Top = trailer.Frame.Bottom + padding
                synopsisLabel.Frame.Left = view.Frame.Left + padding
                synopsisLabel.Frame.Height = height
                synopsisLabel.Frame.Width = labelWidth

                synopsis.Frame.Top = synopsisLabel.Frame.Top - adjustment
                synopsis.Frame.Left = synopsisLabel.Frame.Right + padding
                synopsis.Frame.Height = eighty
                synopsis.Frame.Right = view.Frame.Right - padding

                showtimeLabel.Frame.Top = synopsis.Frame.Bottom + padding
                showtimeLabel.Frame.Left = view.Frame.Left + padding
                showtimeLabel.Frame.Height = height
                showtimeLabel.Frame.Width = labelWidth

                showtimes.Frame.Top = showtimeLabel.Frame.Top - adjustment
                showtimes.Frame.Left = showtimeLabel.Frame.Right + padding
                showtimes.Frame.Height = eighty
                showtimes.Frame.Width = view.Frame.Width - padding
            |] @> |> ignore

    override x.ViewDidLoad () =
        base.ViewDidLoad ()
        x.Title <- film.Name
        loadContent x.ContentView