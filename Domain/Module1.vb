Imports System.Net
Imports System.Text

Module Module1
    Private processOutput As StringBuilder = New StringBuilder()

    Sub Main()
        Dim s As String = Dns.GetHostEntry(Dns.GetHostName()).AddressList _
        .Where(Function(a As IPAddress) Not a.IsIPv6LinkLocal AndAlso Not a.IsIPv6Multicast AndAlso Not a.IsIPv6SiteLocal) _
        .First() _
        .ToString()

        Dim line As String = ("/k nslookup " + s)
        Dim objP As New System.Diagnostics.Process()
        Dim objPi As ProcessStartInfo = New ProcessStartInfo()
        With objPi
            .FileName = "cmd.exe"
            .Arguments = line
            .RedirectStandardOutput = True
            .RedirectStandardError = True
            .RedirectStandardInput = True
            .UseShellExecute = False
            .WindowStyle = ProcessWindowStyle.Hidden
            .CreateNoWindow = False

        End With
        objP.StartInfo = objPi
        AddHandler objP.OutputDataReceived, AddressOf OutputHandler

        objP.Start()
        objP.BeginOutputReadLine()
        objP.WaitForExit(1000)

        Console.ReadKey()

    End Sub
    Private Sub OutputHandler(sendingProcess As Object, outLine As DataReceivedEventArgs)
        If Not String.IsNullOrEmpty(outLine.Data) Then
            If outLine.Data.Contains("Server") Or outLine.Data.Contains("Serveur") Then
                'processOutput.AppendLine(outLine.Data)
                Dim server As String = outLine.Data.Replace("Server", "").Replace("Serveur :", "")
                If server.ToUpper.Contains("PROD.MRQ") Then
                    Console.WriteLine("Revenu quebec")

                Else
                    Console.WriteLine("Sin acceso")

                    Console.WriteLine("Serveur : " + server)
                End If
            End If
        End If
    End Sub

End Module
