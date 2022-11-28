Public Class CharacterData
    Public Property Name As String
    Public Property LocationId As Integer
    Public Property Wounds As Integer
    Public Property Health As Integer
    Public Property AttackDice As String
    Public Property DefendDice As String
    Public Property Items As New Dictionary(Of Integer, ItemData)
End Class
