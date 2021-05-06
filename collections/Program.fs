// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO

let getMeanScore (row: string) =
    let elements = row.Split('\t')
    let name = elements.[0]
    let id = elements.[1]

    let meanScore =
        elements
        |> Array.skip 2
        |> Array.map float
        |> Array.average

    sprintf "%s	%s	%f" name id meanScore


let processRows (rows) =
    let meanScores = Array.map getMeanScore rows
    (rows |> Array.length, meanScores)

[<EntryPoint>]
let main argv =
    if (argv |> Array.length) = 1 then
        let filePath = argv.[0]

        if File.Exists filePath then
            printfn "Processing %s..." filePath
            let rows = File.ReadAllLines filePath
            let (studentsCount, meanScores) = processRows rows
            printfn "Students count is %i" studentsCount
            Array.iter (printfn "%s") meanScores
            0
        else
            printfn "Such file doesn't exist"
            2
    else
        printfn "Please specify a file"
        1
