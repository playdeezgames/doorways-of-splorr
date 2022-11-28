Module CreateDirectionProcessor
    Friend Sub Run(world As IWorld)
        Dim name = AnsiConsole.Ask("Direction Name:", String.Empty)
        If String.IsNullOrWhiteSpace(name) Then
            Return
        End If
        EditDirectionProcessor.Run(world.CreateDirection(name))
    End Sub
End Module
