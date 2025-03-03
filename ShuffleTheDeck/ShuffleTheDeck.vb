'Brandon Barrera
'RCET 2265
'Spring 2025
'Shuffle The Deck
'https://github.com/BrandLeBar/ShuffleTheDeck.git

Option Compare Text
Option Strict On
Option Explicit On
Module ShuffleTheDeck
    Sub Main()
        Dim userInput As String

        Do
            Console.Clear()
            DisplayBoard()

            Console.WriteLine("Enter a ""A"" to auto draw all Cards & Enter a ""D"" to draw or ""C"" to Clear. ""Q"" to quit.")
            userInput = Console.ReadLine()

            If userInput = "D" Then
                DrawCard()
            ElseIf userInput = "C" Then
                CardTracker(0, 0, True)
                DrawCard(True)
            ElseIf userInput = "A" Then
                For i = 0 To 51
                    DrawCard()
                Next
            Else
                Console.WriteLine("No can do boss")
            End If
        Loop Until userInput = "Q"
    End Sub

    Sub DrawCard(Optional clearCount As Boolean = False)
        Dim temp(,) As Boolean = CardTracker(0, 0)
        Dim currentNumber As Integer
        Dim currentSuit As Integer
        Static cardCounter As Integer

        If clearCount Then
            cardCounter = 0

        ElseIf cardCounter = 52 Then
            CardTracker(0, 0, True)
            DrawCard(True)
        Else
            Do
                currentNumber = RandomNumberGenerator(0, 12) 'Gets Number
                currentSuit = RandomNumberGenerator(0, 3) 'Gets Suit

            Loop Until temp(currentNumber, currentSuit) = False Or cardCounter >= 52

            CardTracker(currentNumber, currentSuit,, True)
            cardCounter += 1
        End If

    End Sub

    Function RandomNumberGenerator(min As Integer, max As Integer) As Integer
        Randomize()
        Return CInt(Math.Ceiling(max - min) * Rnd() + min)
    End Function

    Sub DisplayBoard()
        Dim temp As String = "X  |"
        Dim tracker(,) As Boolean = CardTracker(0, 0)
        Dim heading() As String = {"Club", " Diamond", "  Hearts", "  Spades"}
        Dim columnWidth As Integer = 10

        For Each suit In heading
            Console.Write(suit.PadLeft(CInt(Math.Ceiling(columnWidth / 1.6))).PadRight(columnWidth))
        Next

        Console.WriteLine()
        Console.WriteLine(StrDup(columnWidth * 4, "_"))

        For currentNumber = 0 To 12

            For currentSuit = 0 To 3
                If tracker(currentNumber, currentSuit) Then
                    temp = $"{CardType(currentNumber, currentSuit)}    |"
                Else
                    temp = "|"
                End If

                Console.Write(temp.PadLeft(columnWidth))
            Next

            Console.WriteLine()
        Next

    End Sub

    Function CardTracker(cardNumber As Integer, cardSuit As Integer, Optional clear As Boolean = False, Optional update As Boolean = False) As Boolean(,)
        Static _cardTracker(12, 3) As Boolean

        If update Then
            _cardTracker(cardNumber, cardSuit) = True
        End If
        If clear Then
            ReDim _cardTracker(12, 3)
        End If

        Return _cardTracker
    End Function

    Function CardType(cardNumber As Integer, cardSuit As Integer) As String
        Dim _cardType As String

        Select Case cardNumber
            Case 0
                _cardType = "A"
            Case 1 To 9
                _cardType = CStr(cardNumber + 1)
            Case 10 To 12
                If cardNumber = 10 Then
                    _cardType = "J"
                ElseIf cardNumber = 11 Then
                    _cardType = "Q"
                ElseIf cardNumber = 12 Then
                    _cardType = "K"
                End If
        End Select

        Return _cardType
    End Function

    Function CardHouse(cardNumber As Integer, cardSuit As Integer) As String
        Dim _cardHouse As String

        Select Case cardSuit
            Case 0
                _cardHouse = " of Clubs "
            Case 1
                _cardHouse = " of Diamonds "
            Case 2
                _cardHouse = " of Hearts "
            Case 3
                _cardHouse = " of Spades "
        End Select

        Return _cardHouse
    End Function

End Module
