Public Class WorldData
    Public Property Directions As New Dictionary(Of Integer, DirectionData)
    Public Property Locations As New Dictionary(Of Integer, LocationData)
    Public Property Characters As New Dictionary(Of Integer, CharacterData)
    Public Property PlayerCharacterId As Integer?
    Public Property Messages As New List(Of String)
End Class
