Module EditLocationsProcessor
    Friend Sub Run(world As IWorld)
        Do
            AnsiConsole.Clear()
            Dim list As New List(Of String) From
                {
                    GoBackText,
                    CreateLocationText
                }
            Dim table = world.Locations.ToDictionary(Function(x) x.UniqueName, Function(x) x)
            list.AddRange(table.Keys)
            Dim answer = DoMenu("Edit Locations:", list.ToArray)
            Select Case answer
                Case GoBackText
                    Exit Do
                Case CreateLocationText
                    CreateLocationProcessor.Run(world)
                Case Else
                    EditLocationProcessor.Run(world, table(answer))
            End Select
        Loop
    End Sub
End Module
