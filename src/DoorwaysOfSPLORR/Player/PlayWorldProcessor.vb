Module PlayWorldProcessor
    Friend Sub Run(world As IWorld)
        Do While world.CanPlay
            Dim character As ICharacter = world.PlayerCharacter
            If character.IsDead Then
                ShowGameOver(world)
                OkPrompt()
                Exit Do
            ElseIf character.HasWon Then
                ShowWin(world)
                OkPrompt()
            End If
            AnsiConsole.Clear()
            ShowMessages(character)
            ShowCharacter(character)
            AnsiConsole.MarkupLine($"Location: {character.Location.Name}")
            Dim menuItems As New List(Of String)
            If ShowEnemies(character) Then
                menuItems.Add(AttackText)
                menuItems.Add(FleeText)
            Else
                If ShowExits(character) Then
                    menuItems.Add(MoveText)
                End If
            End If
            menuItems.Add(SaveGameText)
            menuItems.Add(DonePlayingText)
            Dim answer = Utility.DoMenu("Now What?", menuItems.ToArray)
            Select Case answer
                Case AttackText
                    SelectAttackTargetProcessor.Run(character)
                Case DonePlayingText
                    If Confirm("Do you want to finish playing without saving?") Then
                        Exit Do
                    Else
                        If SaveWorldProcessor.Run(world) Then
                            Exit Do
                        End If
                    End If
                Case FleeText
                    character.Flee()
                Case MoveText
                    MoveProcessor.Run(character)
                Case SaveGameText
                    SaveWorldProcessor.Run(world)
            End Select
        Loop
    End Sub

    Private Sub ShowWin(world As IWorld)
        AnsiConsole.Clear()
        ShowMessages(world.PlayerCharacter)
        AnsiConsole.MarkupLine($"[green]{world.PlayerCharacter.Name} has won![/]")
        OkPrompt()
    End Sub

    Private Sub ShowGameOver(world As IWorld)
        AnsiConsole.Clear()
        ShowMessages(world.PlayerCharacter)
        AnsiConsole.MarkupLine($"[red]{world.PlayerCharacter.Name} is dead![/]")
        OkPrompt()
    End Sub
    Private Sub ShowCharacter(character As ICharacter)
        AnsiConsole.MarkupLine($"Name: {character.Name}")
        AnsiConsole.MarkupLine($"Attack: {RNG.MaximumRoll(character.AttackDice)}")
        AnsiConsole.MarkupLine($"Defend: {RNG.MaximumRoll(character.DefendDice)}")
        AnsiConsole.MarkupLine($"Health: {character.MaximumHealth - character.Wounds}/{character.MaximumHealth}")
    End Sub

    Private Sub ShowMessages(character As ICharacter)
        For Each message In character.Messages
            AnsiConsole.MarkupLine(message)
        Next
        character.ClearMessages()
    End Sub

    Private Function ShowExits(character As ICharacter) As Boolean
        Dim routes = character.Location.Routes
        If routes.Any Then
            AnsiConsole.MarkupLine($"Exits: {String.Join(", ", routes.Select(Function(x) x.Direction.Name))}")
            Return True
        End If
        Return False
    End Function
    Private Function ShowEnemies(character As ICharacter) As Boolean
        Dim characters = character.Location.EnemiesOf(character)
        If characters.Any Then
            AnsiConsole.MarkupLine($"Enemies: {String.Join(", ", characters.Select(Function(x) x.Name))}")
        End If
        Return characters.Any(Function(x) x.Id <> character.Id)
    End Function
End Module
