Imports System.Net.Mail

Public Class ClassMail
    Private Shared ints As ClassMail
    Public Shared Function Mail() As ClassMail
        If ints Is Nothing Then
            ints = New ClassMail
        End If
        Return ints
    End Function

    Dim _isBodyHtml As Boolean = True
    Public Property IsBodyHtml() As Boolean
        Get
            Return _isBodyHtml
        End Get
        Set(ByVal value As Boolean)
            _isBodyHtml = value
        End Set
    End Property
    Dim _enviarOCCSoporteTecnico As Boolean = False
    Property EnviarOCCSoporteTecnico() As Boolean
        Get
            Return _enviarOCCSoporteTecnico
        End Get
        Set(ByVal value As Boolean)
            _enviarOCCSoporteTecnico = value
        End Set
    End Property

    Private ReadOnly emailSoporteTecnico As String = "soporte_swpos@kronosti.com"
    Private ReadOnly emailSYSTEM As String = "soporte_swpos@kronosti.com"

    Private ReadOnly emailClient As SmtpClient
    Private message As MailMessage

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ipServidorSmtp">Ejemplo: 192.168.1.1</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ipServidorSmtp As String)
        emailClient = New SmtpClient(ipServidorSmtp)
    End Sub
    ''' <summary>
    ''' Constructor vacio define por default del mail server
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        emailClient = New SmtpClient("mail.kronosti.com")

        Dim credenciales As New Net.NetworkCredential(emailSYSTEM, "gpKofszjx6wem1^t_yvchdbiHrlaqk")
        emailClient.UseDefaultCredentials = False
        emailClient.Credentials = credenciales

    End Sub

    ''' <summary>
    ''' Enviar un Email 
    ''' </summary>
    ''' <param name="vFrom">De</param>
    ''' <param name="vTo">A</param>
    ''' <param name="vAsunto">Asunto</param>
    ''' <param name="vMsg">Cuerpo del mensage</param>
    ''' <remarks></remarks>
    Public Sub SendMail(ByVal vFrom As String, ByVal vTo As String, ByVal vAsunto As String, ByVal vMsg As String)
        If vMsg.Trim.Length > 0 Then
            Try
                message = New MailMessage(vFrom, vTo, vAsunto, vMsg) With {
                    .IsBodyHtml = Me.IsBodyHtml
                }
                If Me.EnviarOCCSoporteTecnico = True Then
                    message.Bcc.Add(Me.emailSoporteTecnico)
                End If
                emailClient.Send(message)
            Catch ex As Exception
                Try

                Catch exi As Exception

                End Try
            End Try
        End If
    End Sub

    Public Sub SendEMailError(ByVal funcion As String, ByVal msg As String)
        Try
            Dim msgi As String
            msgi = ArmarMsgError(Nothing, msg, funcion)

            'SendMail(vFrom:=emailSYSTEM, vTo:=emailSoporteTecnico, vAsunto:="SWPOST {" & funcion & "}", vMsg:=msgi)
            message = New MailMessage(from:=emailSYSTEM, to:=emailSoporteTecnico, subject:="PvSystemAPI {" & funcion & "}", body:=msgi) With {
                .IsBodyHtml = True
            }

            emailClient.SendAsync(message, EndSendMailCallBack)
        Catch ex As Exception
            Try

            Catch exi As Exception

            End Try
        End Try
    End Sub
    Private Function EndSendMailCallBack() As Int64
        Return 0
    End Function
    ''' <summary>
    ''' Enviar un Email que contiene datos del error al Desarrollador del sistema
    ''' </summary>
    ''' <param name="Form">Funcion que genero el error</param>
    ''' <param name="msg">Detalle del error</param>
    ''' <remarks></remarks>
    Public Sub SendEMailError(ByVal Form As Object, ByVal msg As String)
        Dim msgi As String
        msgi = ArmarMsgError(Form, msg, "")

        message = New MailMessage(from:=emailSYSTEM, to:=emailSoporteTecnico, subject:="PvSystemAPI {" & Form.name & "}", body:=msgi) With {
            .IsBodyHtml = True
        }

        emailClient.Send(message)
    End Sub
    Private Function ArmarMsgError(ByVal frm As Object, ByVal msg As String, ByVal Funcion As String) As String
        Dim rs As String = ""

        rs = "<!DOCTYPE HTML PUBLIC " & Chr(34) & "-//W3C//DTD HTML 4.0 Transitional//ES" & Chr(34) & "><html><head></head>"
        rs &= "<body>"

        rs &= "<h3>Notificación de error en Sistema</h3>"

        rs &= "Sistema: <strong>PvSystemAPI</strong><br>"
        rs &= "Modulo: <strong>" & My.Application.Info.ProductName & "</strong><br><br>"

        rs &= "<strong>Detalle de error:</strong><br>"
        rs &= "<strong>PC: </strong>" & My.Computer.Name & "<br>"
        rs &= "<strong>IP: </strong>" & Bitacora.GetInstanceBitacora.ObtenerIP.ToString.Trim & "<br>"
        If frm IsNot Nothing Then
            rs &= "<strong>Formulario: </strong>" & frm.Name & "<br>"
        Else
            rs &= "<strong>Función: </strong>" & Funcion.Trim & "<br>"
        End If
        Dim con As New ClassConexion02
        rs &= "<strong>Fecha: </strong>[" & con.GetCurrentDate.ToString("dd/MMM/yyyy") & "] <strong>Hora: </strong>[" & con.GetCurrentTime.ToString("HH:mm:ss") & "]<br>"
        rs &= msg.ToString.Trim & "<br />"
        rs &= "</body></html>"
        Return rs
    End Function

End Class
