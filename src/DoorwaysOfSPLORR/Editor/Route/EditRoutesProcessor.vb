Imports System.ComponentModel.Design

Module EditRoutesProcessor
    Friend Sub Run(world As IWorld, location As ILocation)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]Routes from {location.UniqueName}[/]"}
            prompt.AddChoice(GoBackText)
            Dim table = location.Routes.ToDictionary(Function(x) $"{x.Direction.UniqueName} -> {x.ToLocation.UniqueName}", Function(x) x)
            prompt.AddChoices(table.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case GoBackText
                    Exit Do
                Case Else
                    EditRouteProcessor.Run(world, table(answer))
            End Select
        Loop
    End Sub
End Module
