Public Class LocationItem
    Implements IItem
    Private worldData As WorldData
    Private locationId As Integer
    Private itemId As Integer
    Sub New(worldData As WorldData, locationId As Integer, itemId As Integer)
        Me.worldData = worldData
        Me.locationId = locationId
        Me.itemId = itemId
    End Sub
    Public ReadOnly Property Name As String Implements IItem.Name
        Get
            Return worldData.Locations(locationId).Items(itemId).Name
        End Get
    End Property
End Class
