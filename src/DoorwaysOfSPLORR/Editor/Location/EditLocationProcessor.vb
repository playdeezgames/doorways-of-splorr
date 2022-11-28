Module EditLocationProcessor
    Friend Sub Run(world As IWorld, location As ILocation)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Location Id: {location.Id}")
            AnsiConsole.MarkupLine($"Location Name: {location.Name}")
            Dim hasRoutes = ShowRoutes(location)
            ShowCharacters(location)
            Select Case DoMenu("Now What?", GoBackText, ChangeNameText, If(location.CanAddRoute, AddRouteText, Nothing), If(hasRoutes, EditRoutesText, Nothing), If(location.CanDestroy, DeleteText, Nothing))
                Case AddRouteText
                    CreateRouteProcessor.Run(world, location)
                Case ChangeNameText
                    RunChangeName(location)
                Case DeleteText
                    location.Destroy()
                    Exit Do
                Case EditRoutesText
                    EditRoutesProcessor.Run(world, location)
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub ShowCharacters(location As ILocation)
        Dim characters As IEnumerable(Of ICharacter) = location.Characters
        If characters.Any Then
            AnsiConsole.MarkupLine("Characters:")
            For Each character In characters
                AnsiConsole.MarkupLine($"- {character.UniqueName}")
            Next
        End If
    End Sub

    Private Function ShowRoutes(location As ILocation) As Boolean
        If location.Routes.Any Then
            AnsiConsole.MarkupLine("Routes:")
            For Each route In location.Routes
                AnsiConsole.MarkupLine($"- {route.Direction.UniqueName} -> {route.ToLocation.UniqueName}")
            Next
            Return True
        End If
        Return False
    End Function

    Private Sub RunChangeName(location As ILocation)
        Dim newName = AnsiConsole.Ask("New Name:", String.Empty)
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        location.Name = newName
    End Sub
End Module
