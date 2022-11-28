Module EditCharacterProcessor
    Friend Sub Run(world As IWorld, ByRef character As ICharacter)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Character Id: {character.Id}")
            AnsiConsole.MarkupLine($"Character Name: {character.Name}")
            AnsiConsole.MarkupLine($"Location: {character.Location.UniqueName}")
            AnsiConsole.MarkupLine($"Wounds: {character.Wounds}")
            AnsiConsole.MarkupLine($"Health: {character.MaximumHealth}")
            AnsiConsole.MarkupLine($"Attack Dice: {character.AttackDice}")
            AnsiConsole.MarkupLine($"Defend Dice: {character.DefendDice}")
            If character.IsPlayerCharacter Then
                AnsiConsole.MarkupLine($"This is the player character.")
            End If
            Select Case DoMenu("Now What?", GoBackText, ChangeNameText, ChangeLocationText, ChangeWoundsText, ChangeHealthText, ChangeAttackDiceText, ChangeDefendDiceText, If(Not character.IsPlayerCharacter, SetPlayerCharacterText, Nothing), CloneText, If(character.CanDestroy, DeleteText, Nothing))
                Case ChangeAttackDiceText
                    RunChangeAttackDice(character)
                Case ChangeDefendDiceText
                    RunChangeDefendDice(character)
                Case ChangeHealthText
                    RunChangeHealth(character)
                Case ChangeNameText
                    RunChangeName(character)
                Case ChangeLocationText
                    character.Location = Utility.ChooseLocation("New Location?", world)
                Case ChangeWoundsText
                    RunChangeWounds(character)
                Case CloneText
                    character = character.Clone()
                Case DeleteText
                    character.Destroy()
                    Exit Do
                Case GoBackText
                    Exit Do
                Case SetPlayerCharacterText
                    character.SetPlayerCharacter()
            End Select
        Loop
    End Sub
    Private Sub RunChangeWounds(character As ICharacter)
        character.Wounds = AnsiConsole.Ask(Of Integer)("New Wounds Value?", 0)
    End Sub
    Private Sub RunChangeHealth(character As ICharacter)
        character.MaximumHealth = AnsiConsole.Ask(Of Integer)("New Health Value?", 0)
    End Sub

    Private Sub RunChangeDefendDice(character As ICharacter)
        Dim diceText = AnsiConsole.Ask(Of String)("New Attack Dice Value?", String.Empty)
        If RNG.ValidateDice(diceText) Then
            character.DefendDice = diceText
        End If
    End Sub

    Private Sub RunChangeAttackDice(character As ICharacter)
        Dim diceText = AnsiConsole.Ask(Of String)("New Defend Dice Value?", String.Empty)
        If RNG.ValidateDice(diceText) Then
            character.AttackDice = diceText
        End If
    End Sub

    Private Sub RunChangeName(character As ICharacter)
        Dim newName = AnsiConsole.Ask("New Name?", String.Empty)
        If String.IsNullOrWhiteSpace(newName) Then
            Return
        End If
        character.Name = newName
    End Sub
End Module
