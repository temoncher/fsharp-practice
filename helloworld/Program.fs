// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

// Define a function to construct a message to print
let from whom = $"from {whom} program"

let greetAPerson person =
    let message = $"{person}, " + from "F#" // Call the function
    printfn $"Hello, {message}"

let greetPeople (people: string array) =
    Array.iter greetAPerson people
    printfn "Nice to meet you all!"

[<EntryPoint>]
let main argv =
    match argv with
    | _ when argv.Length > 1 ->
        greetPeople argv
        0
    | _ when argv.Length = 1 ->
        greetAPerson argv.[0]
        printfn "It's dangerous to go alone!"
        0
    | _ ->
        greetAPerson "Anonymus"
        0
