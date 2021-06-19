open System.IO
open StudentScores

[<EntryPoint>]
let main argv =
    if (argv |> Array.length) = 1 then
        let filePath = argv.[0]

        if File.Exists filePath then
            try
                Summary.summarize filePath
                0
            with
            | :? System.FormatException as e ->
                printfn "Error: %s" e.Message
                printfn "The file is not in the expected format"
                1
            | :? IOException as e ->
                printfn "Error: %s" e.Message
                printfn "The file is open in another program, please close it"
                2
        else
            printfn "Such file doesn't exist"
            3
    else
        printfn "Please specify a file"
        4
