namespace Classes

open System

type Person(name: string, favoriteColor: option<string>) =
    do
        if String.IsNullOrWhiteSpace(name) then
            raise
            <| ArgumentException("Null or empty", "name")

    member this.Description() : string =
        let color =
            match favoriteColor with
            | Some color -> color.ToString()
            | None -> "(None)"

        sprintf "%s, favorite color: %s" name color
