Module EditRouteProcessor
    Friend Sub Run(world As IWorld, route As IRoute)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"From Location: {route.FromLocation.UniqueName}")
            AnsiConsole.MarkupLine($"Direction: {route.Direction.UniqueName}")
            AnsiConsole.MarkupLine($"Destination: {route.ToLocation.UniqueName}")
            Select Case Utility.DoMenu(GoBackText, ChangeDestinationText, DeleteText)
                Case GoBackText
                    Exit Do
                Case DeleteText
                    route.Destroy()
                    Exit Do
                Case ChangeDestinationText
                    route.ToLocation = Utility.ChooseLocation("New Destination?", world)
            End Select
        Loop
    End Sub
End Module
