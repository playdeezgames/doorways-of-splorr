Module MainMenuProcessor
    Friend Sub Run()
        Do
            AnsiConsole.Clear()
            Select Case Utility.DoMenu("Main Menu:", NewWorldText, OpenWorldText, QuitText)
                Case NewWorldText
                    NewWorldProcessor.Run()
                Case OpenWorldText
                    OpenWorldProcessor.Run()
                Case QuitText
                    If Confirm("Are you sure you want to quit?") Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module
