namespace Classes

open System

type ConsolePrompt(message: string, maxReTries: int) =
    do
        if String.IsNullOrWhiteSpace(message) then
            raise
            <| ArgumentException("Null or empty", "message")

    let trimmedMessage = message.Trim()
    let mutable tryCount = 0

    let mutable foreground = ConsoleColor.White
    let mutable background = ConsoleColor.Black


    member val BeepOnError = false with get, set

    member this.ColorScheme
        with get (): ConsoleColor * ConsoleColor = (foreground, background)
        and set (fg: ConsoleColor, bg: ConsoleColor) =
            if (fg = bg) then
                raise
                <| ArgumentException("Foreground color and background color must be different")

            foreground <- fg
            background <- bg

    member this.GetValue() : string =
        tryCount <- tryCount + 1

        if tryCount > maxReTries then
            raise
            <| IO.IOException("You reached max number of tries")

        Console.ForegroundColor <- foreground
        Console.BackgroundColor <- background

        printf "%s: " trimmedMessage

        Console.ResetColor()

        let input = Console.ReadLine()

        if (String.IsNullOrWhiteSpace(input)) then
            if this.BeepOnError then Console.Beep()
            this.GetValue()
        else
            input
