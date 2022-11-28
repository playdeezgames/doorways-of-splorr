Friend Class CharacterItem
    Implements IItem
    Private worldData As WorldData
    Private characterId As Integer
    Private itemId As Integer

    Public Sub New(worldData As WorldData, characterId As Integer, itemId As Integer)
        Me.worldData = worldData
        Me.characterId = characterId
        Me.itemId = itemId
    End Sub

    Public ReadOnly Property Name As String Implements IItem.Name
        Get
            Return worldData.Characters(characterId).Items(itemId).Name
        End Get
    End Property
End Class
