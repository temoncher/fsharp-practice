open System
open Classes

[<EntryPoint>]
let main argv =
    let namePrompt =
        ConsolePrompt("Please enter your name", 3)

    namePrompt.BeepOnError <- true
    namePrompt.ColorScheme <- (ConsoleColor.Cyan, ConsoleColor.Black)

    let colorPrompt =
        ConsolePrompt("What is your favorite color? (press Enter if you don't have one)", 1)

    colorPrompt.ColorScheme <- (ConsoleColor.Cyan, ConsoleColor.Black)

    try
        let name = namePrompt.GetValue()
        printfn "Hello %s" name

        let person =
            try
                let favoriteColor = colorPrompt.GetValue()
                Person(name, Some favoriteColor)
            with
            | :? IO.IOException -> Person(name, None)

        printfn "%s" (person.Description())
        0
    with
    | :? IO.IOException as e ->
        printfn "Error: %s" e.Message
        1
