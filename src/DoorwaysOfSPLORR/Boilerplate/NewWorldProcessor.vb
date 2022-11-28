Module NewWorldProcessor
    Friend Sub Run()
        Dim newWorld As IWorld = World.Create()
        WorldModeMenuProcessor.Run(newWorld)
    End Sub
End Module
