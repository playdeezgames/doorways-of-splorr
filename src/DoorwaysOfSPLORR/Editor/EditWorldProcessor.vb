Module EditWorldProcessor
    Friend Sub Run(world As IWorld)
        Do
            AnsiConsole.Clear()
            Select Case Utility.DoMenu("Edit World:", EditCharactersText, EditDirectionsText, EditLocationsText, DoneEditingText)
                Case EditDirectionsText
                    EditDirectionsProcessor.Run(world)
                Case EditLocationsText
                    EditLocationsProcessor.Run(world)
                Case EditCharactersText
                    EditCharactersProcessor.Run(world)
                Case DoneEditingText
                    If Confirm("Do you want to finish editing without saving?") Then
                        Exit Do
                    Else
                        If SaveWorldProcessor.Run(world) Then
                            Exit Do
                        End If
                    End If
            End Select
        Loop
    End Sub
End Module
