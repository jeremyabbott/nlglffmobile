module Nlglff.Api

open FSharp.Data

let baseApiUrl = "http://nlglff-dev.azurewebsites.net/api/"

let filmsUrl = baseApiUrl + "films"
let sponsorsUrl = baseApiUrl + "sponsors"

type Films = JsonProvider<"http://nlglff-dev.azurewebsites.net/api/films", RootName="Film">
type Film = Films.Film

type Sponsors = JsonProvider<"http://nlglff-dev.azurewebsites.net/api/sponsors", RootName="Sponsor">
type Sponsor = Sponsors.Sponsor


let loadFilms () =
    Films.Load(filmsUrl)

let loadSponsors () =
    Sponsors.Load(sponsorsUrl)