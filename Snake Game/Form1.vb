Public Class Form1
    Dim btn(19, 19), h As Button
    Dim body, body2 As Queue
    Dim x, y, d, food, fx, fy, timing As Integer
    Dim time As String
    Dim r As Random
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Select Case d
            Case 1
                y -= 1
            Case 2
                y += 1
            Case 3
                x -= 1
            Case 4
                x += 1
        End Select
        If (x < 0 Or x > 19 Or y < 0 Or y > 19) Then
            Timer1.Stop()
            Timer2.Stop()
            MessageBox.Show("遊戲結束" + vbNewLine + "遊戲時間 : " + time)
            clear()
        ElseIf body.Count = 399 Then
            Timer1.Stop()
            Timer2.Stop()
            MessageBox.Show("你贏了!" + vbNewLine + "遊戲時間 : " + time)
            clear()
        ElseIf btn(x, y).BackColor = Color.blue Then
            Timer1.Stop()
            Timer2.Stop()
            MessageBox.Show("遊戲結束" + vbNewLine + "遊戲時間 : " + time)
            clear()
        Else
            body.Enqueue(btn(x, y))
            h = body.Peek
            h.BackColor = Color.WhiteSmoke
            body.Dequeue()
            draw()
        End If
        If btn(fx, fy).BackColor = Color.WhiteSmoke Or btn(fx, fy).BackColor = Color.Blue Then
            r = New Random
            fx = r.Next(0, 19)
            fy = r.Next(0, 19)
            btn(fx, fy).BackColor = Color.Red
        End If
        If btn(fx, fy).BackColor = Color.Green Then
            body.Enqueue(btn(x, y))
            Label1.Text = "長度 : " + body.Count.ToString("000")
        End If
    End Sub
    Sub clear()
        Label1.Text = "長度 : 05"
        Label2.Text = "時間 : 00:00:00"
        time = 0
        body.Clear()
        cs()
        draw()
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        timing += 1
        time = (timing \ 3600).ToString("00") + ":" + (timing \ 60).ToString("00") + ":" + (timing Mod 60).ToString("00")
        Label2.Text = "時間 : " + time
    End Sub
    Private Sub Form1_Keydown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Timer1.Start()
        Timer2.Start()
        Select Case e.KeyCode
            Case Keys.Up
                If d <> 2 Then
                    d = 1
                End If
            Case Keys.Down
                If d <> 1 Then
                    d = 2
                End If
            Case Keys.Left
                If d <> 4 Then
                    d = 3
                End If
            Case Keys.Right
                If d <> 3 Then
                    d = 4
                End If
        End Select
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        body = New Queue
        d = 1
        For j = 0 To 19
            For i = 0 To 19
                btn(i, j) = New Button
                btn(i, j).Size = New Size(16, 16)
                btn(i, j).Enabled = False
                btn(i, j).BackColor = Color.WhiteSmoke
                btn(i, j).Location = New Point(20 + i * 20, 100 + j * 20)
                Me.Controls.Add(btn(i, j))
            Next
        Next
        cs()
        draw()
    End Sub
    Sub cs()
        x = 10
        y = 10
        For j = 0 To 19
            For i = 0 To 19
                btn(i, j).BackColor = Color.WhiteSmoke
            Next
        Next
        For l = 4 To 0 Step -1
            body.Enqueue(btn(x, y + l))
        Next
    End Sub
    Sub draw()
        body2 = body.Clone
        Do
            h = body2.Dequeue
            h.BackColor = Color.Blue
        Loop Until body2.Count = 1
        h = body2.Dequeue
        h.BackColor = Color.Green
    End Sub
End Class
