Module MoveProcessor
    Friend Sub Run(character As ICharacter)
        Dim table = character.Location.Routes.ToDictionary(Function(x) x.Direction.UniqueName, Function(x) x)
        Dim menuItems As New List(Of String)
        menuItems.Add(NeverMindText)
        menuItems.AddRange(table.Keys)
        Dim answer = Utility.DoMenu("Which Way?", menuItems.ToArray)
        Select Case answer
            Case NeverMindText
                Return
            Case Else
                character.Location = table(answer).ToLocation
        End Select
    End Sub
End Module
