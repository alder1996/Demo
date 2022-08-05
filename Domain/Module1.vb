Imports System.Net

Module Module1

    Sub Main()
        Dim s As String = Dns.GetHostEntry(Dns.GetHostName()).AddressList _
        .Where(Function(a As IPAddress) Not a.IsIPv6LinkLocal AndAlso Not a.IsIPv6Multicast AndAlso Not a.IsIPv6SiteLocal) _
        .First() _
        .ToString()
        Console.Write(Dns.GetHostName() & vbCrLf)
        Console.Write(s)
        Console.ReadKey()
        Console.Write(Dns.GetHostEntry(s).AddressList)
        Console.ReadKey()

    End Sub

End Module
