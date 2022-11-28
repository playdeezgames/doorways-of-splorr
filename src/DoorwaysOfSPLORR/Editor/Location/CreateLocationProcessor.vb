Module CreateLocationProcessor
    Friend Sub Run(world As IWorld)
        Dim name = AnsiConsole.Ask("Location Name:", String.Empty)
        If String.IsNullOrWhiteSpace(name) Then
            Return
        End If
        EditLocationProcessor.Run(world, world.CreateLocation(name))
    End Sub
End Module
