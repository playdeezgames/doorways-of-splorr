Friend Class Direction
    Implements IDirection
    Private worldData As WorldData
    Sub New(worldData As WorldData, id As Integer)
        Me.Id = id
        Me.worldData = worldData
    End Sub
    Public ReadOnly Property Id As Integer Implements IDirection.Id
    Public Property Name As String Implements IDirection.Name
        Get
            Return worldData.Directions(Id).Name
        End Get
        Set(value As String)
            worldData.Directions(Id).Name = value
        End Set
    End Property

    Public ReadOnly Property UniqueName As String Implements IDirection.UniqueName
        Get
            Return $"{worldData.Directions(Id).Name}(#{Id})"
        End Get
    End Property

    Public ReadOnly Property CanDestroy As Boolean Implements IDirection.CanDestroy
        Get
            Return Not worldData.Locations.Any(Function(x) x.Value.Routes.ContainsKey(Id))
        End Get
    End Property

    Public Sub Destroy() Implements IDirection.Destroy
        worldData.Directions.Remove(Id)
    End Sub
End Class
