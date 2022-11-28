Public Interface IWorld
    ReadOnly Property Directions As IEnumerable(Of IDirection)
    Function CreateDirection(name As String) As IDirection
    ReadOnly Property Locations As IEnumerable(Of ILocation)
    Function CreateLocation(name As String) As ILocation
    ReadOnly Property Characters As IEnumerable(Of ICharacter)
    Function CreateCharacter(name As String, location As ILocation, wounds As Integer, health As Integer, attackDice As String, defendDice As String) As ICharacter
    ReadOnly Property PlayerCharacter As ICharacter
    Sub Save(filename As String)
    ReadOnly Property EnemiesOf(character As ICharacter) As IEnumerable(Of ICharacter)
    ReadOnly Property CanPlay As Boolean
End Interface
