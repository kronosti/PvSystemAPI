Public Class Bitacora
    Private Shared Instancia As Bitacora
    Public Sub New()
    End Sub
    Public Shared Function GetInstanceBitacora() As Bitacora
        If Instancia Is Nothing Then
            Instancia = New Bitacora
        End If
        Return Instancia
    End Function

    Public Function ObtenerIP() As String
        Dim ipS As String = ""
        Dim nombreHost As String = System.Net.Dns.GetHostName
        Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(nombreHost)

        For Each ip As System.Net.IPAddress In hostInfo.AddressList
            ipS = ip.ToString
        Next

        Return ipS
    End Function
    Public Function ObtenerNombrePc() As String

        Dim nombrePC As String = ""
        Dim nombreHost As String = System.Net.Dns.GetHostName
        Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(nombreHost)

        For Each ip As System.Net.IPAddress In hostInfo.AddressList
            nombrePC = hostInfo.HostName.ToString
        Next

        Return nombrePC
    End Function
End Class
