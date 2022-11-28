Module CreateCharacterProcessor
    Friend Sub Run(world As IWorld)
        AnsiConsole.Clear()
        Dim name = AnsiConsole.Ask("Character Name?", String.Empty)
        If String.IsNullOrWhiteSpace(name) Then
            Return
        End If
        Dim wounds = AnsiConsole.Ask(Of Integer)("Wounds?", 0)
        Dim health = AnsiConsole.Ask(Of Integer)("Health?", 0)
        Dim attackDice = AnsiConsole.Ask("Attack Dice?", String.Empty)
        If Not RNG.ValidateDice(attackDice) Then
            Return
        End If
        Dim defendDice = AnsiConsole.Ask("Defend Dice?", String.Empty)
        If Not RNG.ValidateDice(defendDice) Then
            Return
        End If
        Dim location = Utility.ChooseLocation("Character Location?", world)
        EditCharacterProcessor.Run(world, world.CreateCharacter(name, location, wounds, health, attackDice, defendDice))
    End Sub
End Module
