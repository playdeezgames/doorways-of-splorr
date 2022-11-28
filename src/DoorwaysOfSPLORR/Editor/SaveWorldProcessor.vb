Module SaveWorldProcessor
    Friend Function Run(world As IWorld) As Boolean
        Dim filename = AnsiConsole.Ask(Of String)("Filename:", String.Empty)
        If String.IsNullOrWhiteSpace(filename) Then
            Return False
        End If
        world.Save(filename)
        Return True
    End Function
End Module
