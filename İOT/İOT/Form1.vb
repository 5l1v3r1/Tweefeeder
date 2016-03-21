Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices
Imports System.Net.Mail
Imports Microsoft.Win32



Public Class Form1
    Shared _continue As Boolean
    Shared _serialPort As SerialPort

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '-----------------------------------------------------------------------------------------------------------------------------
        Timer1.Start()
        WebBrowser1.Navigate("www.twitter.com/boykabasgan") ' Komutların geleceği twitter adresi.
        serialPort1.Close()
        serialPort1.PortName = "COM7" 'Arduino portunuz ile değiştirin.
        serialPort1.BaudRate = 9600 'Baud Hızı.
        serialPort1.DataBits = 8 'Gelecek data boyutu.
        serialPort1.Parity = Parity.None
        serialPort1.StopBits = StopBits.One
        serialPort1.Handshake = Handshake.None
        serialPort1.Encoding = System.Text.Encoding.Default
        '-----------------------------------------------------------------------------------------------------------------------------
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '-----------------------------------------------------------------------------------------------------------------------------
        WebBrowser1.Navigate("www.twitter.com" & TextBox1.Text) ' Komutların gelmesini istediğimiz twitter adresine yönlendirmek için. 
        '-----------------------------------------------------------------------------------------------------------------------------
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        '---------------------------------------------------------------------------------------------------
        WebBrowser1.Refresh() 'Timerin webbrowseri yenilemesini istedik burada. Fakat bu gerekli değildir.Hatta bilgisayarınızı kasabilir.Ama ben kesin olsun diye koymak istedim.
        Dim bilgi As HtmlElementCollection = WebBrowser1.Document.All
        For Each element As HtmlElement In bilgi
            If element.GetAttribute("classname").Contains("tweet-text") Then 'Twitter'den mesajları çektiğimiz bölüm.
                RichTextBox1.Text = RichTextBox1.Text & element.InnerText
            End If
        Next
        '---------------------------------------------------------------------------------------------------
        Dim a As String = ".*" & TextBox2.Text & ".*"
        Dim b As Match = Regex.Match(RichTextBox1.Text, a, RegexOptions.IgnoreCase)
        If b.Success Then
            Timer2.Start()
            serialPort1.Open()
            serialPort1.Write("1") 'Twitterden gelen mesajlarda eğer anahtar kelimemiz geçiyorsa serialporta 1 göndermemize yarıyor.
            serialPort1.Close()
        End If
        '----------------------------------------------------------------------------------------------------
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        '-----------------------------------------------------------------------------------------------------------------------------
        WebBrowser1.Refresh() ' Button2 yi çalışıyormu çalışmıyormu diye test etmek amaçlı eklemişti.m bu o kadar önemli birşey değil.İsteğe bağlı diyebiliriz.
        Dim bilgi As HtmlElementCollection = WebBrowser1.Document.All
        For Each element As HtmlElement In bilgi
            If element.GetAttribute("classname").Contains("tweet-text") Then
                RichTextBox1.Text = RichTextBox1.Text & element.InnerText
            End If
        Next

        Dim a As String = ".*" & TextBox2.Text & ".*"
        Dim b As Match = Regex.Match(RichTextBox1.Text, a, RegexOptions.IgnoreCase)
        If b.Success Then
            Timer2.Start()
            serialPort1.Open()
            serialPort1.Write("1")
            serialPort1.Close()
        End If
        '-----------------------------------------------------------------------------------------------------------------------------
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        '-----------------------------------------------------------------------------------------------------------------------------
        'bir süre sonra robotun mama kolunu kapatması için serialport'a 0 yolluyoruz.
        serialPort1.Open()
        serialPort1.Write("0")
        serialPort1.Write("0")
        serialPort1.Write("0")
        serialPort1.Close()
        TextBox2.Text = TextBox4.Text & TextBox3.Text
        Timer2.Stop()
        '-----------------------------------------------------------------------------------------------------------------------------
    End Sub

   

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox4.Text = TextBox2.Text
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        '-----------------------------------------------------------------------------------------------------------------------------
        'çalışıyormu diye serialporta 1 komutunu göndermek için eklemiştim isteğe bağlı değişebilir.
        serialPort1.Open()
        serialPort1.Write("1")
        serialPort1.Close()
        '-----------------------------------------------------------------------------------------------------------------------------
    End Sub

    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        '-----------------------------------------------------------------------------------------------------------------------------
        'çalışıyormu diye serialporta 0 komutunu göndermek için eklemiştim isteğe bağlı değişebilir.
        serialPort1.Open()
        serialPort1.Write("0")
        serialPort1.Close()
        '-----------------------------------------------------------------------------------------------------------------------------
    End Sub
End Class
