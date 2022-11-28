Module Utility
    Friend Sub WriteFiglet(text As String, color As Color, justify As Justify)
        Dim figlet As New FigletText(text) With
            {
                .Alignment = justify,
                .Color = color
            }
        AnsiConsole.Write(figlet)
    End Sub
    Friend Sub OkPrompt()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = String.Empty}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
    Friend Function DoMenu(title As String, ParamArray items As String()) As String
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
        prompt.AddChoices(items.Where(Function(x) x IsNot Nothing))
        Return AnsiConsole.Prompt(prompt)
    End Function

    Friend Function Confirm(title As String) As Boolean
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[red]{title}[/]"}
        prompt.AddChoices(NoText, YesText)
        Return AnsiConsole.Prompt(prompt) = Yestext
    End Function

    Friend Function ChooseLocation(title As String, world As IWorld) As ILocation
        Dim table = world.Locations.ToDictionary(Function(x) x.UniqueName, Function(x) x)
        Return table(Utility.DoMenu(title, table.Keys.ToArray))
    End Function

    Friend Function ChooseDirection(title As String, world As IWorld) As IDirection
        Dim table = world.Directions.ToDictionary(Function(x) x.UniqueName, Function(x) x)
        Return table(Utility.DoMenu(title, table.Keys.ToArray))
    End Function
End Module
