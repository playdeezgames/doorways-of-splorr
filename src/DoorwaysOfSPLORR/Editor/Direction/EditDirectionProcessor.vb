Module EditDirectionProcessor
    Friend Sub Run(direction As IDirection)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Direction Id: {direction.Id}")
            AnsiConsole.MarkupLine($"Direction Name: {direction.Name}")
            Select Case DoMenu("Now What?", GoBackText, ChangeNameText, If(direction.CanDestroy, DeleteText, Nothing))
                Case ChangeNameText
                    RunChangeName(direction)
                Case DeleteText
                    direction.Destroy()
                    Exit Do
                Case GoBackText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub RunChangeName(direction As IDirection)
        Dim newName = AnsiConsole.Ask("New Name:", String.Empty)
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        direction.Name = newName
    End Sub
End Module
