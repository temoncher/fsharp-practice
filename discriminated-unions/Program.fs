// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
open System.IO
open StudentScores

[<EntryPoint>]
let main argv =
    if (argv |> Array.length) = 1 then
        let filePath = argv.[0]

        if File.Exists filePath then
            Summary.summarize filePath
            0
        else
            printfn "Such file doesn't exist"
            2
    else
        printfn "Please specify a file"
        1
