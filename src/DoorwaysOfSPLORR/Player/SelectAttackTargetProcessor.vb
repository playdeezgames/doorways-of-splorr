Module SelectAttackTargetProcessor
    Friend Sub Run(character As ICharacter)
        Dim enemies = character.Location.EnemiesOf(character)
        If enemies.Count = 1 Then
            character.Attack(enemies.Single)
            Return
        End If
        Dim table = enemies.ToDictionary(Function(x) x.UniqueName, Function(x) x)
        Dim menuItems As New List(Of String) From {NeverMindText}
        menuItems.AddRange(table.Keys)
        Dim answer = DoMenu("Attack Whom?", menuItems.ToArray)
        Select Case answer
            Case NeverMindText
                Return
            Case Else
                character.Attack(table(answer))
                Return
        End Select
    End Sub
End Module
