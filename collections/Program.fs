// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
open System.IO

type Student =
    { Name: string
      Id: string
      MeanScore: float
      MinScore: float
      MaxScore: float }

module Student =
    let fromString (s: string) : Student =
        let elements = s.Split('\t')

        let scores =
            elements |> Array.skip 2 |> Array.map float

        { Name = elements.[0]
          Id = elements.[1]
          MeanScore = scores |> Array.average
          MinScore = scores |> Array.min
          MaxScore = scores |> Array.max }

    let printSummary (student: Student) =
        printfn
            "%s\t%s\t%0.1f\t%0.1f\t%0.1f"
            student.Name
            student.Id
            student.MeanScore
            student.MinScore
            student.MaxScore

let printMeanStudentsScores filePath =
    printfn "Processing %s..." filePath
    // Read
    let rows = File.ReadAllLines filePath
    // Process
    let students =
        rows
        |> Array.skip 1
        |> Array.map Student.fromString
    // Output
    printfn "Students count is %i" (students |> Array.length)
    students
        |> Array.sortByDescending (fun student -> student.MeanScore)
        |> Array.iter Student.printSummary

[<EntryPoint>]
let main argv =
    if (argv |> Array.length) = 1 then
        let filePath = argv.[0]

        if File.Exists filePath then
            printMeanStudentsScores filePath
            0
        else
            printfn "Such file doesn't exist"
            2
    else
        printfn "Please specify a file"
        1
