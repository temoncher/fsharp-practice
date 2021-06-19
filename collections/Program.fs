// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
open System.IO

let getMeanScore (row: string) =
    let elements = row.Split('\t')
    let name = elements.[0]
    let id = elements.[1]
    let meanScore =
        elements
        |> Array.skip 2
        |> Array.averageBy float
    (name, id, meanScore)


let processRows rows = (rows |> Array.length, rows |> Array.map getMeanScore)

let printMeanScore (name, id, meanScore) = printfn "%s\t%s\t%0.1f" name id meanScore

[<EntryPoint>]
let main argv =
    if (argv |> Array.length) = 1 then
        let filePath = argv.[0]

        if File.Exists filePath then
            printfn "Processing %s..." filePath
            let rows = File.ReadAllLines filePath
            let (studentsCount, meanScores) = processRows (rows |> Array.skip 1)
            printfn "Students count is %i" studentsCount
            Array.iter printMeanScore meanScores
            0
        else
            printfn "Such file doesn't exist"
            2
    else
        printfn "Please specify a file"
        1
