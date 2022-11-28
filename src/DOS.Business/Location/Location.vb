Public Class Location
    Implements ILocation
    Private worldData As WorldData
    Sub New(worldData As WorldData, id As Integer)
        Me.worldData = worldData
        Me.Id = id
    End Sub
    Public ReadOnly Property Id As Integer Implements ILocation.Id
    Public Property Name As String Implements ILocation.Name
        Get
            Return worldData.Locations(Id).Name
        End Get
        Set(value As String)
            worldData.Locations(Id).Name = value
        End Set
    End Property
    Public ReadOnly Property UniqueName As String Implements ILocation.UniqueName
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    Public ReadOnly Property Routes As IEnumerable(Of IRoute) Implements ILocation.Routes
        Get
            Return worldData.Locations(Id).Routes.Select(Function(x) New Route(worldData, Id, x.Key))
        End Get
    End Property

    Public ReadOnly Property CanAddRoute As Boolean Implements ILocation.CanAddRoute
        Get
            Return worldData.Directions.Any
        End Get
    End Property
    Public ReadOnly Property CanDestroy As Boolean Implements ILocation.CanDestroy
        Get
            Return Not worldData.Characters.Any(Function(x) x.Value.LocationId = Id)
        End Get
    End Property
    Public ReadOnly Property EnemiesOf(character As ICharacter) As IEnumerable(Of ICharacter) Implements ILocation.EnemiesOf
        Get
            Return Characters.Where(Function(x) Not x.IsDead AndAlso x.IsPlayerCharacter <> character.IsPlayerCharacter)
        End Get
    End Property
    Public ReadOnly Property Characters As IEnumerable(Of ICharacter) Implements ILocation.Characters
        Get
            Return worldData.Characters.Where(Function(x) x.Value.LocationId = Id).Select(Function(x) New Character(worldData, x.Key))
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements ILocation.Items
        Get
            Return worldData.Locations(Id).Items.Select(Function(x) New LocationItem(worldData, Id, x.Key))
        End Get
    End Property

    Public Sub Destroy() Implements ILocation.Destroy
        worldData.Locations.Remove(Id)
    End Sub
    Public Sub AddRoute(direction As IDirection, toLocation As ILocation) Implements ILocation.AddRoute
        worldData.Locations(Id).Routes(direction.Id) = New RouteData With {.ToLocationId = toLocation.Id}
    End Sub
End Class
