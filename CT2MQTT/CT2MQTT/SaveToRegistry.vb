Imports Microsoft.VisualBasic
Imports System
Imports System.Security.Permissions
Imports Microsoft.Win32
Imports System.Text

Public Class EditRegistry
    Shared CT2MQTT As RegistryKey
    Shared Software As RegistryKey
    Shared Settings As RegistryKey

    Public Shared Sub SaveAddress(ByVal Address As String)

        OpenKeys()
        Settings.SetValue("Address", Address)
        CloseKeys()
    End Sub

    Public Shared Sub SavePort(ByVal Port As String)

        OpenKeys()
        Settings.SetValue("Port", Port)
        CloseKeys()
    End Sub

    Public Shared Sub SaveUsername(ByVal Username As String)

        OpenKeys()
        Settings.SetValue("Username", Username)
        CloseKeys()
    End Sub

    Public Shared Sub SavePassword(ByVal Password As String)
        Dim encoded() As Byte
        OpenKeys()
        encoded = Security.Cryptography.ProtectedData.Protect(Encoding.Unicode.GetBytes(Password), Encoding.Unicode.GetBytes(Net.Dns.GetHostName.ToLower), Security.Cryptography.DataProtectionScope.CurrentUser)
        Settings.SetValue("Password", encoded)
        CloseKeys()
    End Sub

    Public Shared Sub SavePollCoreTemp(ByVal Time As String)

        OpenKeys()
        Settings.SetValue("PollCoreTemp", Time)
        CloseKeys()
    End Sub

    Public Shared Sub SavePollMQTTCurrent(ByVal Time As String)

        OpenKeys()
        Settings.SetValue("PollMQTTCurrent", Time)
        CloseKeys()
    End Sub

    Public Shared Sub SavePollMQTTAverage(ByVal Time As String)

        OpenKeys()
        Settings.SetValue("PollMQTTAverage", Time)
        CloseKeys()
    End Sub

    Public Shared Sub SaveRunAtStartup(ByVal TorF As Boolean)

        OpenKeys()
        Settings.SetValue("RunAtStartup", TorF)
        CloseKeys()

        Dim Software As RegistryKey
        Dim Microsoft As RegistryKey
        Dim Windows As RegistryKey
        Dim CurrentVersion As RegistryKey
        Dim Run As RegistryKey

        ' Create a subkey named CT2MQTT under HKEY_CURRENT_USER.
        Software = Registry.CurrentUser.CreateSubKey("Software")

        ' Create Settings subkey under HKEY_CURRENT_USER\Microsoft\Windows\CurrentVersion\Run.
        Microsoft = Software.CreateSubKey("Microsoft")
        Windows = Microsoft.CreateSubKey("Windows")
        CurrentVersion = Windows.CreateSubKey("CurrentVersion")
        Run = CurrentVersion.CreateSubKey("Run")
        If TorF Then
            Run.SetValue(Application.ProductName, Application.ExecutablePath)
        Else
            If Run.GetValue(Application.ProductName, "Not Exist") <> "Not Exist" Then
                Run.DeleteValue(Application.ProductName)
            End If
        End If
        Run.Close()
        CurrentVersion.Close()
        Windows.Close()
        Microsoft.Close()
    End Sub

    Public Shared Sub SaveMinimiseOnStart(ByVal TorF As Boolean)

        OpenKeys()
        Settings.SetValue("MinimiseOnStart", TorF)
        CloseKeys()
    End Sub

    Public Shared Sub SaveStartPollingOnStart(ByVal TorF As Boolean)

        OpenKeys()
        Settings.SetValue("StartPollingOnStart", TorF)
        CloseKeys()
    End Sub

    Public Shared Sub OpenKeys()
        ' Create a subkey named CT2MQTT under HKEY_CURRENT_USER.
        Software = Registry.CurrentUser.CreateSubKey("Software")

        ' Create Settings subkey under HKEY_CURRENT_USER\CT2MQTT.
        CT2MQTT = Software.CreateSubKey("CT2MQTT")
        Settings = CT2MQTT.CreateSubKey("Settings")


    End Sub

    Public Shared Sub CloseKeys()
        Settings.Close()
        Software.Close()
        CT2MQTT.Close()
    End Sub

    Public Shared Function GetAddress() As String

        Dim Address As String
        OpenKeys()
        Address = Settings.GetValue("Address", "0.0.0.0")
        CloseKeys()
        Return Address

    End Function

    Public Shared Function GetPort() As String

        Dim Port As String
        OpenKeys()
        Port = Settings.GetValue("Port", "1883")
        CloseKeys()
        Return Port

    End Function

    Public Shared Function GetUsername() As String

        Dim Username As String
        OpenKeys()
        Username = Settings.GetValue("Username", "")
        CloseKeys()
        Return Username

    End Function

    Public Shared Function GetPassword() As String

        Dim Password As String
        Dim PasswordByte() As Byte
        OpenKeys()
        PasswordByte = Settings.GetValue("Password", New Byte() {})
        If PasswordByte Is New Byte() {} Then
            Password = ""
        Else
            Password = Encoding.Unicode.GetString(Security.Cryptography.ProtectedData.Unprotect(PasswordByte, Encoding.Unicode.GetBytes(Net.Dns.GetHostName.ToLower), Security.Cryptography.DataProtectionScope.CurrentUser))
        End If
        CloseKeys()
        Return Password

    End Function

    Public Shared Function GetPollCoreTemp() As String

        Dim PollCoreTemp As String
        OpenKeys()
        PollCoreTemp = Settings.GetValue("PollCoreTemp", "1000")
        CloseKeys()
        Return PollCoreTemp

    End Function

    Public Shared Function GetPollMQTTCurrent() As String

        Dim PollMQTTCurrent As String
        OpenKeys()
        PollMQTTCurrent = Settings.GetValue("PollMQTTCurrent", "5000")
        CloseKeys()
        Return PollMQTTCurrent

    End Function

    Public Shared Function GetPollMQTTAverage() As String

        Dim PollMQTTAverage As String
        OpenKeys()
        PollMQTTAverage = Settings.GetValue("PollMQTTAverage", "10000")
        CloseKeys()
        Return PollMQTTAverage

    End Function

    Public Shared Function GetRunAtStartup() As Boolean

        Dim RunAtStartup As Boolean
        OpenKeys()
        RunAtStartup = Settings.GetValue("RunAtStartup", False)
        CloseKeys()
        Return RunAtStartup

    End Function

    Public Shared Function GetMinimiseOnStart() As Boolean

        Dim MinimiseOnStart As Boolean
        OpenKeys()
        MinimiseOnStart = Settings.GetValue("MinimiseOnStart", False)
        CloseKeys()
        Return MinimiseOnStart

    End Function

    Public Shared Function GetStartPollingOnStart() As Boolean

        Dim StartPollingOnStart As Boolean
        OpenKeys()
        StartPollingOnStart = Settings.GetValue("StartPollingOnStart", False)
        CloseKeys()
        Return StartPollingOnStart

    End Function

End Class
