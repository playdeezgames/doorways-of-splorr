Module CreateRouteProcessor
    Friend Sub Run(world As IWorld, location As ILocation)
        location.AddRoute(Utility.ChooseDirection("Which Direction?", world), Utility.ChooseLocation("Which Destination?", world))
    End Sub
End Module
