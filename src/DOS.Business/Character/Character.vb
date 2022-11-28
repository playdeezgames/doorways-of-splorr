Public Class Character
    Implements ICharacter
    Private worldData As WorldData
    Sub New(worldData As WorldData, id As Integer)
        Me.worldData = worldData
        Me.Id = id
    End Sub
    Public ReadOnly Property Id As Integer Implements ICharacter.Id
    Public Property Name As String Implements ICharacter.Name
        Get
            Return worldData.Characters(Id).Name
        End Get
        Set(value As String)
            worldData.Characters(Id).Name = value
        End Set
    End Property
    Public ReadOnly Property UniqueName As String Implements ICharacter.UniqueName
        Get
            Return $"{Name}(#{Id})" & If(IsPlayerCharacter, "(PC)", String.Empty)
        End Get
    End Property
    Public Property Location As ILocation Implements ICharacter.Location
        Get
            Return New Location(worldData, worldData.Characters(Id).LocationId)
        End Get
        Set(value As ILocation)
            worldData.Characters(Id).LocationId = value.Id
        End Set
    End Property
    Public ReadOnly Property CanDestroy As Boolean Implements ICharacter.CanDestroy
        Get
            Return Not worldData.PlayerCharacterId.HasValue OrElse worldData.PlayerCharacterId.Value <> Id
        End Get
    End Property
    Public ReadOnly Property IsPlayerCharacter As Boolean Implements ICharacter.IsPlayerCharacter
        Get
            Return worldData.PlayerCharacterId.HasValue AndAlso worldData.PlayerCharacterId.Value = Id
        End Get
    End Property
    Public ReadOnly Property Messages As IEnumerable(Of String) Implements ICharacter.Messages
        Get
            Return If(IsPlayerCharacter, worldData.Messages.AsEnumerable, Array.Empty(Of String))
        End Get
    End Property
    Public Property Wounds As Integer Implements ICharacter.Wounds
        Get
            Return worldData.Characters(Id).Wounds
        End Get
        Set(value As Integer)
            worldData.Characters(Id).Wounds = value
        End Set
    End Property
    Public Property AttackDice As String Implements ICharacter.AttackDice
        Get
            Return worldData.Characters(Id).AttackDice
        End Get
        Set(value As String)
            worldData.Characters(Id).AttackDice = value
        End Set
    End Property
    Public Property DefendDice As String Implements ICharacter.DefendDice
        Get
            Return worldData.Characters(Id).DefendDice
        End Get
        Set(value As String)
            worldData.Characters(Id).DefendDice = value
        End Set
    End Property
    Public Property MaximumHealth As Integer Implements ICharacter.MaximumHealth
        Get
            Return worldData.Characters(Id).Health
        End Get
        Set(value As Integer)
            worldData.Characters(Id).Health = value
        End Set
    End Property
    Public ReadOnly Property IsDead As Boolean Implements ICharacter.IsDead
        Get
            Return Wounds >= MaximumHealth
        End Get
    End Property

    Public ReadOnly Property World As IWorld Implements ICharacter.World
        Get
            Return New World(worldData)
        End Get
    End Property

    Public ReadOnly Property HasWon As Boolean Implements ICharacter.HasWon
        Get
            Return World.EnemiesOf(Me).All(Function(x) x.IsDead)
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements ICharacter.Items
        Get
            Return worldData.Characters(Id).Items.Select(Function(x) New CharacterItem(worldData, Id, x.Key))
        End Get
    End Property

    Public Sub Destroy() Implements ICharacter.Destroy
        worldData.Characters.Remove(Id)
    End Sub
    Public Sub SetPlayerCharacter() Implements ICharacter.SetPlayerCharacter
        worldData.PlayerCharacterId = Id
    End Sub
    Public Sub AddMessage(text As String) Implements ICharacter.AddMessage
        If IsPlayerCharacter Then
            worldData.Messages.Add(text)
        End If
    End Sub
    Public Sub ClearMessages() Implements ICharacter.ClearMessages
        If IsPlayerCharacter Then
            worldData.Messages.Clear()
        End If
    End Sub
    Public Sub AddDamage(damage As Integer) Implements ICharacter.AddDamage
        worldData.Characters(Id).Wounds += damage
    End Sub
    Private Sub DoCounterAttacks()
        Dim enemies = Location.EnemiesOf(Me)
        For Each enemy In enemies
            enemy.Attack(Me)
        Next
    End Sub

    Public Sub Attack(defender As ICharacter) Implements ICharacter.Attack
        AddMessage($"{Name} attacks {defender.Name}!")
        defender.AddMessage($"{defender.Name} is attacked by {Name}!")
        Dim attackRoll As Integer = RollAttack()
        AddMessage($"{Name} rolls an attack of {attackRoll}!")
        defender.AddMessage($"{Name} rolls an attack of {attackRoll}!")
        Dim defenseRoll As Integer = defender.RollDefense()
        AddMessage($"{defender.Name} rolls a defense of {defenseRoll}!")
        defender.AddMessage($"{defender.Name} rolls a defense of {defenseRoll}!")
        If defenseRoll >= attackRoll Then
            AddMessage($"{Name} misses!")
            defender.AddMessage($"{Name} misses!")
            GoTo CounterAttack
        End If
        Dim damage = attackRoll - defenseRoll
        AddMessage($"{defender.Name} takes {damage} damage!")
        defender.AddMessage($"{defender.Name} takes {damage} damage!")
        defender.AddDamage(damage)
        If defender.IsDead Then
            AddMessage($"{Name} kills {defender.Name}!")
            defender.AddMessage($"{defender.Name} is killed by {Name}!")
        End If
CounterAttack:
        If IsPlayerCharacter Then
            DoCounterAttacks()
        End If
    End Sub

    Public Function Flee() As Boolean Implements ICharacter.Flee
        Dim routes = Location.Routes
        If Not routes.Any Then
            AddMessage($"{Name} tries to flee, but has no where to flee to!")
            If IsPlayerCharacter Then
                DoCounterAttacks()
            End If
            Return False
        End If
        Dim route = RNG.FromEnumerable(routes)
        AddMessage($"{Name} flees by going {route.Direction.Name}!")
        Location = route.ToLocation
        Return True
    End Function

    Public Function RollAttack() As Integer Implements ICharacter.RollAttack
        Return RNG.RollDice(worldData.Characters(Id).AttackDice)
    End Function
    Public Function RollDefense() As Integer Implements ICharacter.RollDefense
        Return RNG.RollDice(worldData.Characters(Id).DefendDice)
    End Function

    Public Function Clone() As ICharacter Implements ICharacter.Clone
        Return World.CreateCharacter(Name, Location, Wounds, MaximumHealth, AttackDice, DefendDice)
    End Function
End Class
