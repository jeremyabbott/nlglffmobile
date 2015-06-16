module Nlglff.Api

open FSharp.Data

let baseApiUrl = "http://nlglff-dev.azurewebsites.net/api/"


let filmsUrl = baseApiUrl + "films"

type Films = JsonProvider<"http://nlglff-dev.azurewebsites.net/api/films", RootName="Film">
type Film = Films.Film

let loadFilms () =
    Films.Load(filmsUrl)