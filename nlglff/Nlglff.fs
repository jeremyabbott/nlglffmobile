module Nlglff.Api

open System.Collections.Generic
open FSharp.Data

let baseApiUrl = "http://nlglff-dev.azurewebsites.net/api/"

let filmsUrl = baseApiUrl + "films"
let sponsorsUrl = baseApiUrl + "sponsors"

type Films = JsonProvider<"http://nlglff-dev.azurewebsites.net/api/films", RootName="Film">
type Film = Films.Film

type Sponsors = JsonProvider<"http://nlglff-dev.azurewebsites.net/api/sponsors", RootName="Sponsor">
type Sponsor = Sponsors.Sponsor

let memoize name f =
    let dict = new System.Collections.Generic.Dictionary<_,_>()

    fun () ->
        match dict.TryGetValue(name) with
        | (true, v) -> v
        | _ ->
            let temp = f()
            dict.Add(name, temp)
            temp

let loadFilms = memoize "films" (fun () ->
    Films.Load(filmsUrl))


let loadSponsors = memoize "sponsors" (fun () ->
    Sponsors.Load(sponsorsUrl))