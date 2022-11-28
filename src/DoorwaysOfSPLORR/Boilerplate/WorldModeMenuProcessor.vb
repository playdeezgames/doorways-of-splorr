Module WorldModeMenuProcessor
    Friend Sub Run(world As IWorld)
        Do
            AnsiConsole.Clear()
            Select Case DoMenu("Now What?", PlayWorldText, EditWorldText, GoBackText)
                Case EditWorldText
                    EditWorldProcessor.Run(world)
                Case GoBackText
                    Exit Do
                Case PlayWorldText
                    PlayWorldProcessor.Run(world)
            End Select
        Loop
    End Sub
End Module
