Imports System.Collections.ObjectModel
Imports System.Text

Module Program
    Class xPlayer
        Public id As Integer
        Public name As String
    End Class
    Class xGame
        Public GameID As Integer
        Public Player1 As xPlayer
        Public Player2 As xPlayer
    End Class
    Class xRound
        Public RoundNum As Integer
        Public Court As Integer
        Public Game As xGame
    End Class
    Sub Main()
        Dim intPlayers As Integer = 12


        Dim colPlayers As New Collection(Of xPlayer)
        Dim intRounds As Integer = intPlayers - 1
        Dim intCourtsPerRound As Integer = intPlayers / 4
        Dim lstRounds As New List(Of xRound)

        'Load Players
        For intLoop As Integer = 1 To intPlayers
            Dim clsPlayer As New xPlayer With {.id = intLoop, .name = Chr(intLoop + 64)}
            colPlayers.Add(clsPlayer)
        Next

        Dim colPairs As New Collection(Of (PLayer1 As xPlayer, PLayer2 As xPlayer))

        ' ONly works when number of players is equaly divided by four
        If intPlayers Mod 4 = 0 Then
            ' each game will get it's own number to identify it later this incriments
            Dim intGameNum = 1

            Dim colGames As New Collection(Of xGame)

            'build a list of of Pairs
            For intPLoop As Integer = 0 To intPlayers - 1

                Dim intOpponents As Integer = intPlayers - 1
                For intOLoop As Integer = intOpponents To 0 Step -1
                    'cant partner with your self and either side of the partnership gets checked for unique
                    If intPLoop <> intOLoop And
                                colPairs.Contains((colPlayers(intPLoop), colPlayers(intOLoop))) = False And
                                colPairs.Contains((colPlayers(intOLoop), colPlayers(intPLoop))) = False Then

                        colPairs.Add((colPlayers(intPLoop), colPlayers(intOLoop)))
                        Dim game As New xGame With {.GameID = intGameNum, .Player1 = colPlayers(intPLoop), .Player2 = colPlayers(intOLoop)}
                        colGames.Add(game)
                        intGameNum = intGameNum + 1

                    End If
                Next 'next opponent
            Next 'next player

            'build rounds using the sets of pairs
            For intRoundLoop As Integer = 1 To intRounds
                Dim CourtInRound As Integer = 1

                For Each rndGame As xGame In colGames
                        'inner vairable to prevent lambda issues
                        Dim intROundCheck As Integer = intRoundLoop

                        'before we add the game to this round, check that each player is already not plaing in round
                        ' and that the UNique GamieID is also not been assigned to any round
                        Dim chk1 As Integer = lstRounds.FindIndex(Function(p) p.RoundNum = intROundCheck And p.Game.Player1.id = rndGame.Player1.id)
                        Dim chk2 As Integer = lstRounds.FindIndex(Function(p) p.RoundNum = intROundCheck And p.Game.Player2.id = rndGame.Player1.id)
                        Dim chk3 As Integer = lstRounds.FindIndex(Function(p) p.RoundNum = intROundCheck And p.Game.Player1.id = rndGame.Player2.id)
                        Dim chk4 As Integer = lstRounds.FindIndex(Function(p) p.RoundNum = intROundCheck And p.Game.Player2.id = rndGame.Player2.id)
                        Dim chk5 As Integer = lstRounds.FindIndex(Function(p) p.Game.GameID = rndGame.GameID) 'Game is not already used

                        If chk1 = -1 And chk2 = -1 And chk3 = -1 And chk4 = -1 And chk5 = -1 Then
                        Dim Newrnd As New xRound With {.RoundNum = intRoundLoop, .Court = CourtInRound, .Game = rndGame}
                        lstRounds.Add(Newrnd)

                            '1 court in each count can only be assigned a total of 2 game pairs
                            'once two unique pairs are set increment the court
                            Dim chk6 = lstRounds.FindAll(Function(c) c.RoundNum = intROundCheck And c.Court = CourtInRound).Count
                            If chk6 = 2 Then
                                CourtInRound = CourtInRound + 1
                            End If
                        End If
                    Next
                Next


                For Each WriteRound As xRound In lstROunds
                Dim strOut As New StringBuilder
                strOut.Append("Round: ")
                strOut.Append(WriteRound.RoundNum)
                strOut.Append(" Court: ")
                strOut.Append(WriteRound.Court)
                strOut.Append(" Player 1: ")
                strOut.Append(WriteRound.Game.Player1.id)
                strOut.Append(" Player 2: ")
                strOut.Append(WriteRound.Game.Player2.id)
                Console.WriteLine(strOut)
            Next



        End If
    End Sub
End Module
