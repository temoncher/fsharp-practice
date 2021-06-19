namespace StudentScores

type TestResult =
    | Absent
    | Excused
    | Voided
    | Scored of float

module TestResult =
    let fromString (s: string) : TestResult =
        match s with
        | "A"
        | "N/A" -> Absent
        | "E" -> Excused
        | "V" -> Voided
        | _ -> Scored(s |> float)

    let tryToEffectiveScore (testResult: TestResult) : option<float> =
        match testResult with
        | Absent -> Some 0.0
        | Excused
        | Voided -> None
        | Scored score -> Some score
