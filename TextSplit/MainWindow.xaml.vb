Imports System.Windows.Media.Animation
Class MainWindow
    Dim RandomNumber1, RandomNumber2 As Integer
    Private Sub MainWindow_KeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles Me.KeyDown
        RandomNumber1 = Rnd() * (Me.Width - 100)
        RandomNumber2 = Rnd() * (Me.Width - 100)
        If RandomNumber1 > RandomNumber2 Then
            CType(Me.Content, Canvas).Children.Add(New Chararacter(e.Key.ToString, 360, 180, Rnd() * (Me.Width - 100), Rnd() * (Me.Width - 100), Me.Height, Rnd() * (Me.Height - 400), 1000))
        Else
            CType(Me.Content, Canvas).Children.Add(New Chararacter(e.Key.ToString, 180, 360, Rnd() * (Me.Width - 100), Rnd() * (Me.Width - 100), Me.Height, Rnd() * (Me.Height - 400), 1000))
        End If
    End Sub
End Class
Class Chararacter
    Inherits TextBlock
    Dim Rotate As New RotateTransform
    Dim Turn, MoveLeft, MoveTop As New DoubleAnimation
    Sub New(ByVal IChar As Char, ByVal StartAngle As Integer, ByVal EndAngle As Integer, ByVal StartLeft As Integer, ByVal EndLeft As Integer, ByVal StartTop As Integer, ByVal EndTop As Integer, ByVal Duration As Integer, Optional ByVal BackEaseEnabled As Boolean = False)
        Me.Text = IChar
        'Me.FontFamily = Fonts.SystemFontFamilies(Rnd() * (Fonts.SystemFontFamilies.Count - 1))
        'Me.Foreground = New SolidColorBrush(Color.FromRgb(Rnd() * 255, Rnd() * 255, Rnd() * 255))
        Me.FontSize = 30
        Turn.From = StartAngle
        Turn.To = EndAngle
        MoveLeft.From = StartLeft
        MoveLeft.To = EndLeft
        MoveTop.From = StartTop
        MoveTop.To = EndTop
        If BackEaseEnabled = True Then
            MoveTop.EasingFunction = New BackEase
        Else
            Turn.EasingFunction = New QuadraticEase
            MoveTop.EasingFunction = New QuadraticEase
        End If
        Turn.Duration = TimeSpan.FromMilliseconds(Duration * 2)
        MoveLeft.Duration = TimeSpan.FromMilliseconds(Duration * 2)
        MoveTop.Duration = TimeSpan.FromMilliseconds(Duration)
        MoveTop.AutoReverse = True
        Me.RenderTransform = Rotate
        AddHandler MoveTop.Completed, AddressOf Finish
        Me.BeginAnimation(Canvas.LeftProperty, MoveLeft)
        Me.BeginAnimation(Canvas.TopProperty, MoveTop)
        Rotate.BeginAnimation(RotateTransform.AngleProperty, Turn)
    End Sub
    Sub Finish()
        CType(Me.Parent, Canvas).Children.Remove(Me)
    End Sub
End Class