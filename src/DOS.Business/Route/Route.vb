Public Class Route
    Implements IRoute
    Private worldData As WorldData
    Private locationId As Integer
    Private directionId As Integer
    Sub New(worldData As WorldData, locationId As Integer, directionId As Integer)
        Me.worldData = worldData
        Me.locationId = locationId
        Me.directionId = directionId
    End Sub

    Public ReadOnly Property Direction As IDirection Implements IRoute.Direction
        Get
            Return New Direction(worldData, directionId)
        End Get
    End Property

    Public Property ToLocation As ILocation Implements IRoute.ToLocation
        Get
            Return New Location(worldData, worldData.Locations(locationId).Routes(directionId).ToLocationId)
        End Get
        Set(value As ILocation)
            worldData.Locations(locationId).Routes(directionId).ToLocationId = value.Id
        End Set
    End Property

    Public ReadOnly Property FromLocation As ILocation Implements IRoute.FromLocation
        Get
            Return New Location(worldData, locationId)
        End Get
    End Property

    Public Sub Destroy() Implements IRoute.Destroy
        worldData.Locations(locationId).Routes.Remove(directionId)
    End Sub
End Class
