namespace StudentScores

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
            elements
            |> Array.skip 2
            |> Array.map TestResult.fromString
            |> Array.choose TestResult.tryToEffectiveScore

        { Name = elements.[0]
          Id = elements.[1]
          MeanScore = scores |> Array.average
          MinScore = scores |> Array.min
          MaxScore = scores |> Array.max }

    let printSummary (student: Student) : unit =
        printfn
            "%s\t%s\t%0.1f\t%0.1f\t%0.1f"
            student.Name
            student.Id
            student.MeanScore
            student.MinScore
            student.MaxScore
