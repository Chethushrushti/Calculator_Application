Public Class form1

    ' 計算を行うための 2 つの数字
    Dim num1, num2 As Double
    ' オペレーターが初めてクリックされたかどうかを確認する
    Dim oprClickCount As Integer = 0
    ' オペレータがクリックされたかどうかを確認する 
    Dim isOprClick As Boolean = False
    ' 前に等しいがクリックされたかどうかを確認してください
    Dim isEqualClick As Boolean = False
    ' 演算子を取得します
    Dim opr As String

    Private Sub calculator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        TxtOutput.Select()
        ' フォームのすべてのボタンにクリック イベントを追加する
        For Each c As Control In Controls
            ' コントロールがボタンの場合
            If c.GetType() = GetType(Button) Then
                If Not c.Text.Equals("C") Then
                    ' ボタンにアクションを追加 
                    AddHandler c.Click, AddressOf btn_Click
                End If
            End If
        Next
    End Sub
    Private Sub TxtOutput_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtOutput.KeyDown
        If e.KeyCode = Keys.Escape Then
            num1 = 0
            num2 = 0
            opr = ""
            oprClickCount = 0
            isOprClick = False
            isEqualClick = False
            TxtOutput.Text = ""
        End If
    End Sub


    ' ボタンクリックイベントを作る
    Private Sub btn_Click(sender As Object, e As EventArgs)

        Dim button As Button = sender

        If Not isOperator(button) Then
            ' 場合番号
            If isOprClick Then
                ' opr がクリックされた場合
                ' ダブル テキスト ボックス テキストを取得して変換します
                num1 = Double.Parse(TxtOutput.Text)
                ' テキストボックスのテキストをクリア
                TxtOutput.Text = ""
            End If

            If Not TxtOutput.Text.Contains(".") Then

                ' "."がまだテキストボックスにない場合
                If TxtOutput.Text.Equals("0") AndAlso Not button.Text.Equals(".") Then
                    TxtOutput.Text = button.Text
                    isOprClick = False
                Else
                    TxtOutput.Text += button.Text
                    isOprClick = False
                End If

            ElseIf Not button.Text.Equals(".") Then
                ' ボタンが "." でない場合
                TxtOutput.Text += button.Text
                isOprClick = False
            End If

        Else
            ' if 演算子
            If oprClickCount = 0 Then
                ' 演算子を初めてクリックした場合
                oprClickCount += 1
                num1 = Double.Parse(TxtOutput.Text)
                opr = button.Text
                isOprClick = True

            Else
                If Not button.Text.Equals("=") Then
                    ' ボタンが "=" でない場合
                    If Not isEqualClick Then
                        ' 前に「=」をクリックしない場合
                        num2 = Double.Parse(TxtOutput.Text)
                        TxtOutput.Text = Convert.ToString(calc(opr, num1, num2))
                        num2 = Double.Parse(TxtOutput.Text)
                        opr = button.Text
                        isOprClick = True
                        isEqualClick = False
                    Else
                        isEqualClick = False
                        opr = button.Text

                    End If
                Else
                    num2 = Double.Parse(TxtOutput.Text)
                    TxtOutput.Text = Convert.ToString(calc(opr, num1, num2))
                    num1 = Double.Parse(TxtOutput.Text)
                    isOprClick = True
                    isEqualClick = True
                End If
            End If

        End If


    End Sub

    ' ボタンが数字か演算子かをチェックする関数を作成する
    Function isOperator(ByVal btn As Button) As Boolean

        Dim btnText As String
        btnText = btn.Text

        If (btnText.Equals("+") Or btnText.Equals("-") Or btnText.Equals("/") Or
            btnText.Equals("*") Or btnText.Equals("=")) Then
            Return True
        Else
            Return False
        End If

    End Function

    ' 電卓を実行する関数を作成します
    Function calc(ByVal op As String, ByVal n1 As Double, ByVal n2 As Double) As Double

        Dim result As Double
        result = 0

        Select Case op

            Case "+"
                result = n1 + n2
            Case "-"
                result = n1 - n2
            Case "*"
                result = n1 * n2
            Case "/"
                If n2 <> 0 Then
                    result = n1 / n2
                End If

        End Select

        Return result

    End Function

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button_Clear.Click
        num1 = 0
        num2 = 0
        opr = ""
        oprClickCount = 0
        isOprClick = False
        isEqualClick = False
        TxtOutput.Text = "0"
    End Sub
End Class
