Public Interface IDirection
    ReadOnly Property Id As Integer
    Property Name As String
    ReadOnly Property UniqueName As String
    Sub Destroy()
    ReadOnly Property CanDestroy As Boolean
End Interface
