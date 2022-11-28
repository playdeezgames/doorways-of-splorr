Public Class LocationData
    Public Property Name As String
    Public Property Routes As New Dictionary(Of Integer, RouteData)
    Public Property Items As New Dictionary(Of Integer, ItemData)
End Class
