Imports System.ComponentModel.Design

Module EditDirectionsProcessor
    Friend Sub Run(world As IWorld)
        Do
            AnsiConsole.Clear()
            Dim list As New List(Of String) From
                {
                    GoBackText,
                    CreateDirectionText
                }
            Dim table = world.Directions.ToDictionary(Function(x) x.UniqueName, Function(x) x)
            list.AddRange(table.Keys)
            Dim answer = DoMenu("Edit Directions:", list.ToArray)
            Select Case answer
                Case GoBackText
                    Exit Do
                Case CreateDirectionText
                    CreateDirectionProcessor.Run(world)
                Case Else
                    EditDirectionProcessor.Run(table(answer))
            End Select
        Loop
    End Sub
End Module
