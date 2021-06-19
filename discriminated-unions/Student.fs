namespace StudentScores

type Student =
    { Surname: string
      GivenName: string
      Id: string
      MeanScore: float
      MinScore: float
      MaxScore: float }

module Student =
    let private parseName (s: string) : {| Surname: string; GivenName: string |} =
        let givenAndSurNames = s.Split(',')

        match givenAndSurNames with
        | [| surname; givenName |] -> {| Surname = surname.Trim(); GivenName = givenName.Trim() |}
        | [| surname |] -> {| Surname = surname.Trim(); GivenName = "(None)" |}
        | _ -> raise (System.FormatException("Invalid student name format"))

    let fromString (s: string) : Student =
        let elements = s.Split('\t')
        let name = parseName (elements.[0].Trim())

        let scores =
            elements
            |> Array.skip 2
            |> Array.map TestResult.fromString
            |> Array.choose TestResult.tryToEffectiveScore

        { Surname = name.Surname
          GivenName = name.GivenName
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
