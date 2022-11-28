Module OpenWorldProcessor
    Friend Sub Run()
        Dim filename = AnsiConsole.Ask("Filename:", String.Empty)
        If String.IsNullOrWhiteSpace(filename) Then
            Return
        End If
        Dim world As IWorld = New World(filename)
        WorldModeMenuProcessor.Run(world)
    End Sub
End Module
