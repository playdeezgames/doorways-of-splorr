Module TitleProcessor
    Friend Sub Run()
        AnsiConsole.Clear()
        Utility.WriteFiglet(GameTitle, Color.Fuchsia, Justify.Center)
        Utility.OkPrompt()
    End Sub
End Module
