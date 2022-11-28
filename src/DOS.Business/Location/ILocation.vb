Public Interface ILocation
    ReadOnly Property Id As Integer
    Property Name As String
    ReadOnly Property UniqueName As String
    Sub Destroy()
    Sub AddRoute(direction As IDirection, toLocation As ILocation)
    ReadOnly Property CanAddRoute As Boolean
    ReadOnly Property Routes As IEnumerable(Of IRoute)
    ReadOnly Property CanDestroy As Boolean
    ReadOnly Property Characters As IEnumerable(Of ICharacter)
    ReadOnly Property EnemiesOf(character As ICharacter) As IEnumerable(Of ICharacter)
    ReadOnly Property Items As IEnumerable(Of IItem)
End Interface
