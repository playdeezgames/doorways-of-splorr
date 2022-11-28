Public Interface ICharacter
    ReadOnly Property Id As Integer
    ReadOnly Property World As IWorld
    Property Name As String
    ReadOnly Property UniqueName As String
    Property Location As ILocation
    ReadOnly Property CanDestroy As Boolean
    Sub Destroy()
    ReadOnly Property IsPlayerCharacter As Boolean
    Sub SetPlayerCharacter()
    Sub AddMessage(text As String)
    ReadOnly Property Messages As IEnumerable(Of String)
    Sub ClearMessages()
    Function RollAttack() As Integer
    Function RollDefense() As Integer
    Sub AddDamage(damage As Integer)
    Function Flee() As Boolean
    Sub Attack(defender As ICharacter)
    Function Clone() As ICharacter
    ReadOnly Property HasWon As Boolean
    Property Wounds As Integer
    Property AttackDice As String
    Property DefendDice As String
    Property MaximumHealth As Integer
    ReadOnly Property IsDead As Boolean
    ReadOnly Property Items As IEnumerable(Of IItem)
End Interface
