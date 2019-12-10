Public Class Game
    ' structure for all things bullet
    Structure bulletType
        Dim Xvel As Single
        Dim Yvel As Single
        Dim pic As PictureBox
    End Structure

    Const numBullets As Integer = 10
    Private Bullet(numBullets) As bulletType ' Create an array

    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up the bullet
        Bullet(0).pic = picBullet
        Bullet(1).pic = PictureBox1
        Bullet(2).pic = PictureBox2
        Bullet(3).pic = PictureBox3
        Bullet(4).pic = PictureBox4
        Bullet(5).pic = PictureBox5
        Bullet(6).pic = PictureBox6
        Bullet(7).pic = PictureBox7
        Bullet(8).pic = PictureBox8
        Bullet(9).pic = PictureBox9
    End Sub

    Private Sub Game_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        ' Work out the player's center
        Dim intPlayerCenterX As Integer = picPlayer.Left + picPlayer.Width / 2
        Dim intPlayerCenterY As Integer = picPlayer.Top + picPlayer.Height / 2

        ' Work out the angle between mouse pointer and player
        Dim xDiff As Integer = e.X - intPlayerCenterX
        Dim yDiff As Integer = e.Y - intPlayerCenterY
        Dim dblAngle As Double = Math.Atan2(yDiff, xDiff)

        'Find a bullet not in play (not visible)
        Dim i As Integer
        For i = 0 To numBullets - 1
            If Bullet(i).pic.Visible = False Then
                ' Give the bullet velocity towards the mouse pointer 
                Bullet(i).Xvel = 20 * +Math.Cos(dblAngle)
                Bullet(i).Yvel = 20 * +Math.Sin(dblAngle)

                ' The bullet's starting point
                Bullet(i).pic.Left = intPlayerCenterX
                Bullet(i).pic.Top = intPlayerCenterY
                Bullet(i).pic.Visible = True
                Exit For
            End If
        Next
    End Sub

    Private Sub MoveBullet()
        ' Invisable bullets are not in play
        Dim i As Integer
        For i = 0 To numBullets - 1
            If Bullet(i).pic.Visible = True Then
                Bullet(i).pic.Left += Bullet(i).Xvel
                Bullet(i).pic.Top += Bullet(i).Yvel

                ' Check if the bullet has gone off the screen and needs recycling
                If Bullet(i).pic.Top < 0 Or Bullet(i).pic.Top > Me.Height Or Bullet(i).pic.Left < 0 Or Bullet(i).pic.Left > Me.Width Then
                    Bullet(i).pic.Visible = False ' Recycle the bullet
                End If
            End If
        Next
    End Sub
    Private Sub TmrGameLoop_Tick(sender As Object, e As EventArgs) Handles tmrGameLoop.Tick
        Call MoveBullet()
    End Sub
End Class