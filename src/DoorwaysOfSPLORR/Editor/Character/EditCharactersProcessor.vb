Module EditCharactersProcessor
    Friend Sub Run(world As IWorld)
        Do
            AnsiConsole.Clear()
            Dim list As New List(Of String) From
                {
                    GoBackText,
                    CreateCharacterText
                }
            Dim table = world.Characters.ToDictionary(Function(x) $"{x.UniqueName} @ {x.Location.UniqueName}", Function(x) x)
            list.AddRange(table.Keys)
            Dim answer = DoMenu("Edit Characters:", list.ToArray)
            Select Case answer
                Case GoBackText
                    Exit Do
                Case CreateCharacterText
                    CreateCharacterProcessor.Run(world)
                Case Else
                    EditCharacterProcessor.Run(world, table(answer))
            End Select
        Loop
    End Sub
End Module
