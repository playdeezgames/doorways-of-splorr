Public Class World
    Implements IWorld
    Private worldData As WorldData
    Sub New()
        worldData = New WorldData
    End Sub
    Sub New(worldData As WorldData)
        Me.worldData = worldData
    End Sub
    Sub New(filename As String)
        worldData = JsonSerializer.Deserialize(Of WorldData)(File.ReadAllText(filename))
    End Sub
    Public ReadOnly Property Directions As IEnumerable(Of IDirection) Implements IWorld.Directions
        Get
            Return worldData.Directions.Select(Function(x) New Direction(worldData, x.Key))
        End Get
    End Property

    Public ReadOnly Property Locations As IEnumerable(Of ILocation) Implements IWorld.Locations
        Get
            Return worldData.Locations.Select(Function(x) New Location(worldData, x.Key))
        End Get
    End Property

    Public ReadOnly Property Characters As IEnumerable(Of ICharacter) Implements IWorld.Characters
        Get
            Return worldData.Characters.Select(Function(x) New Character(worldData, x.Key))
        End Get
    End Property

    Public ReadOnly Property CanPlay As Boolean Implements IWorld.CanPlay
        Get
            Return worldData.PlayerCharacterId.HasValue AndAlso Not PlayerCharacter.IsDead
        End Get
    End Property

    Public ReadOnly Property PlayerCharacter As ICharacter Implements IWorld.PlayerCharacter
        Get
            Return If(worldData.PlayerCharacterId.HasValue, New Character(worldData, worldData.PlayerCharacterId.Value), Nothing)
        End Get
    End Property

    Public ReadOnly Property EnemiesOf(character As ICharacter) As IEnumerable(Of ICharacter) Implements IWorld.EnemiesOf
        Get
            Return Characters.Where(Function(x) Not x.IsDead AndAlso x.IsPlayerCharacter <> character.IsPlayerCharacter)
        End Get
    End Property

    Public Function CreateDirection(name As String) As IDirection Implements IWorld.CreateDirection
        Dim id = If(worldData.Directions.Any, worldData.Directions.Keys.Max + 1, 1)
        worldData.Directions.Add(id,
                                 New DirectionData With
                                 {
                                    .Name = name
                                 })
        Return New Direction(worldData, id)
    End Function

    Public Sub Save(filename As String) Implements IWorld.Save
        File.WriteAllText(filename, JsonSerializer.Serialize(worldData))
    End Sub

    Shared Function Create() As IWorld
        Return New World
    End Function

    Public Function CreateLocation(name As String) As ILocation Implements IWorld.CreateLocation
        Dim id = If(worldData.Locations.Any, worldData.Locations.Keys.Max + 1, 1)
        worldData.Locations.Add(id,
                                 New LocationData With
                                 {
                                    .Name = name
                                 })
        Return New Location(worldData, id)
    End Function

    Public Function CreateCharacter(name As String, location As ILocation, wounds As Integer, health As Integer, attackDice As String, defendDice As String) As ICharacter Implements IWorld.CreateCharacter
        Dim id = If(worldData.Characters.Any, worldData.Characters.Keys.Max + 1, 1)
        worldData.Characters.Add(id, New CharacterData With
                                 {
                                    .Name = name,
                                    .LocationId = location.Id,
                                    .Wounds = wounds,
                                    .Health = health,
                                    .AttackDice = attackDice,
                                    .DefendDice = defendDice
                                 })
        Return New Character(worldData, id)
    End Function
End Class
