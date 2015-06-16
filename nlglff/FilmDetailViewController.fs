namespace nlglff

open System
open EasyLayout
open Foundation
open UIKit
open Nlglff.Api

type FilmDetailViewController(film: Film) = 
    inherit UIViewController()

    let eighty = nfloat 80.0
    let fontHeight = nfloat 12.0
    let height = nfloat 20.0
    let padding = nfloat 15.0
    let labelWidth = nfloat 65.0
    let adjustment = nfloat 4.0

    let getTrailerViewForFilm (film : Film) =
        let view = new UIWebView()

        let url =
            match film.TrailerUrl with
            | Some u -> u
            | None -> String.Empty
        
        view.LoadHtmlString(sprintf "<iframe src=\"http://%s\"></iframe>" url, null)

        view

    let trailer = getTrailerViewForFilm film

    let content =
        let view = new UIView(BackgroundColor = UIColor.White)
        let title = new UILabel(Text = film.Name)
        let synopsisLabel = new UILabel(Text = "Synopsis", Font = UIFont.FromName("HelveticaNeue-Medium", fontHeight))
        let synopsis = new UITextView(Text = film.Synopsis, Editable = false)
        let showtimeLabel = new UILabel(Text = "Showtimes", Font = UIFont.FromName("HelveticaNeue-Medium", fontHeight))
        let showtimeText =
            film.Showtimes
            |> Array.map (fun s -> sprintf "%s @ %s" (s.Date.ToShortDateString()) (s.Time.ToShortTimeString()))
            |> Array.reduce (fun acc item -> sprintf "%s\n%s" acc item)
        let showtimes = new UITextView(Text = showtimeText, Editable = false)

        view.AddSubviews(title, synopsisLabel, synopsis, showtimeLabel, showtimes, trailer)

        view.ConstrainLayout
            <@ [|
                title.Frame.Top = view.Frame.Top + eighty
                title.Frame.CenterX = view.Frame.CenterX + padding
                title.Frame.Height = height
                title.Frame.Width = view.Frame.Width

                synopsisLabel.Frame.Top = title.Frame.Bottom + padding
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

                trailer.Frame.Top = showtimes.Frame.Bottom + padding
                trailer.Frame.Height = view.Frame.Width - height
                trailer.Frame.Width = view.Frame.Width
                trailer.Frame.Left = view.Frame.Left + padding
            |] @> |> ignore
        view

    override x.ViewDidLoad () =
        x.View <- content
        x.Title <- film.Name