Public Interface IRoute
    ReadOnly Property Direction As IDirection
    Property ToLocation As ILocation
    ReadOnly Property FromLocation As ILocation
    Sub Destroy()
End Interface
