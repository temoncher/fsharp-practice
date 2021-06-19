namespace StudentScores

module Summary =
    open System.IO

    let summarize (filePath: string) : unit =
        printfn "Processing %s..." filePath
        // Read
        let rows =
            filePath |> File.ReadAllLines |> Array.skip 1
        // Process
        let students = rows |> Array.map Student.fromString
        // Output
        printfn "Students count is %i" (students |> Array.length)

        students
        |> Array.sortByDescending (fun student -> student.MeanScore)
        |> Array.iter Student.printSummary
