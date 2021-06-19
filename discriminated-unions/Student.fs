namespace StudentScores

type Student =
    { Surname: string
      GivenName: string
      Id: string
      MeanScore: float
      MinScore: float
      MaxScore: float }

module Student =
    let private parseName (s: string) : (string * string) =
        let givenAndSurNames = s.Split(',')

        match givenAndSurNames with
        | [| sur; given |] -> (sur.Trim(), given.Trim())
        | _ -> raise (System.FormatException("Invalid student name format"))

    let fromString (s: string) : Student =
        let elements = s.Split('\t')
        let (sur, given) = parseName(elements.[0].Trim())

        let scores =
            elements
            |> Array.skip 2
            |> Array.map TestResult.fromString
            |> Array.choose TestResult.tryToEffectiveScore

        { Surname = sur
          GivenName = given
          Id = elements.[1].Trim()
          MeanScore = scores |> Array.average
          MinScore = scores |> Array.min
          MaxScore = scores |> Array.max }

    let printSummary (student: Student) : unit =
        printfn
            "%s, %s\t%s\t%0.1f\t%0.1f\t%0.1f"
            student.Surname
            student.GivenName
            student.Id
            student.MeanScore
            student.MinScore
            student.MaxScore
