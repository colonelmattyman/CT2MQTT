Imports System.Text
Imports Microsoft.VisualBasic
Imports System
Imports System.Security.Permissions
Imports Microsoft.Win32

Public Class frmCoreTempToMQTT

    Dim CTInfo As New GetCoreTempInfoNET.CoreTempInfo
    Dim TLAvg As New TempLoad
    Dim ConnectMQTT As Object
    Dim OverallCoreCount As Integer
    Dim AverageTick As Integer
    Dim PasswordEdit As Boolean
    Dim YAMLConfig As String = ""

    Dim CPUName As String
    Dim PhysicalCPUs As String
    Dim CoresPerCPU As String
    Dim FSBSpeed As String
    Dim Multiplier As String
    Dim CPUSpeed As String
    Dim CPUSpeedText As String
    Dim CPUVID As String
    Dim CPUVIDText As String
    Dim TempType As Char

    Dim CTRunOnce As Boolean = False
    Dim CurrentRunOnce As Boolean = False
    Dim AverageRunOnce As Boolean = False
    Dim HostName As String = Net.Dns.GetHostName
    Dim SensorNames(1000) As String
    Dim SensorHeading As String = "sensor/" & HostName.ToLower
    Dim sensorcount As Integer


    Private Sub frmCoreTempToMQTT_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Load data from registry if it exists
        PopulateForm()

        PasswordEdit = False

        If cbxStartPolling.Checked Then
            StartPolling()
        End If

        If cbxMinimise.Checked Then
            ToggleHide()
        End If

        cmdSave.Enabled = False
        cmdCancel.Enabled = False

    End Sub

    Private Sub PopulateForm()
        Dim Address As String
        Dim Port As String
        Dim Username As String
        Dim Password As String
        Dim PollCoreTemp As String
        Dim PollMQTTCurrent As String
        Dim PollMQTTAverage As String
        Dim RunAtStartup As Boolean
        Dim MinimiseOnStart As Boolean
        Dim StartPollingOnStart As Boolean

        Address = EditRegistry.GetAddress()
        Port = EditRegistry.GetPort()
        Username = EditRegistry.GetUsername()
        Password = "********"  'EditRegistry.GetPassword()
        PollCoreTemp = EditRegistry.GetPollCoreTemp()
        PollMQTTCurrent = EditRegistry.GetPollMQTTCurrent()
        PollMQTTAverage = EditRegistry.GetPollMQTTAverage()
        RunAtStartup = EditRegistry.GetRunAtStartup()
        MinimiseOnStart = EditRegistry.GetMinimiseOnStart()
        StartPollingOnStart = EditRegistry.GetStartPollingOnStart()

        txtAddress.Text = Address
        txtPort.Text = Port
        txtUsername.Text = Username
        txtPassword.Text = Password
        txtPollCoreTemp.Text = PollCoreTemp
        txtPollMQTTCurrent.Text = PollMQTTCurrent
        txtPollMQTTAverage.Text = PollMQTTAverage
        cbxRunAtStart.Checked = RunAtStartup
        cbxMinimise.Checked = MinimiseOnStart
        cbxStartPolling.Checked = StartPollingOnStart

    End Sub

    Private Sub PushCurrent_tick(sender As Object, e As EventArgs) Handles PushCurrent.Tick

        Dim tempsensor As String
        Dim loadsensor As String
        Dim mqttoutheader As String
        Dim mqttouttemp As String
        Dim mqttoutload As String
        Dim mqttoutaverages As String
        Dim CurrentTemp As Integer
        Dim CurrentLoad As Integer
        Dim TempSum As Double
        Dim LoadSum As Double
        Dim TempSensorArray(PhysicalCPUs * CoresPerCPU) As String
        Dim LoadSensorArray(PhysicalCPUs * CoresPerCPU) As String
        Dim Counter As Integer


        If Not ConnectMQTT.IsConnected Then
            On Error Resume Next
            ConnectMQTT.Connect(Guid.NewGuid().ToString(), EditRegistry.GetUsername(), EditRegistry.GetPassword())
        End If
        TempSum = 0
        LoadSum = 0

        mqttoutaverages = ""
        mqttoutload = ""
        mqttouttemp = ""
        mqttoutheader = "MQTT Output" & vbCrLf
        mqttoutheader = mqttoutheader & "Connected: " & ConnectMQTT.IsConnected & vbCrLf
        mqttoutheader = mqttoutheader & "Port: " & ConnectMQTT.Settings.Port & vbCrLf

        ConnectMQTT.Publish(SensorNames(4), Encoding.UTF8.GetBytes(FSBSpeed))
        ConnectMQTT.Publish(SensorNames(5), Encoding.UTF8.GetBytes(Multiplier))
        ConnectMQTT.Publish(SensorNames(6), Encoding.UTF8.GetBytes(CPUSpeed))
        ConnectMQTT.Publish(SensorNames(7), Encoding.UTF8.GetBytes(CPUSpeedText))
        ConnectMQTT.Publish(SensorNames(8), Encoding.UTF8.GetBytes(CPUVID))
        ConnectMQTT.Publish(SensorNames(9), Encoding.UTF8.GetBytes(CPUVIDText))
        ConnectMQTT.Publish(SensorNames(10), Encoding.UTF8.GetBytes(TempType))



        For cpu As Integer = 0 To CTInfo.GetCPUCount - 1
            For core As Integer = 0 To CTInfo.GetCoreCount - 1
                'Generate sensor topics for MQTT
                tempsensor = SensorHeading & "/cpu" & cpu + 1 & "/core" & core + 1 & "/temperature"
                loadsensor = SensorHeading & "/cpu" & cpu + 1 & "/core" & core + 1 & "/load"

                If Not (CurrentRunOnce) Then
                    TempSensorArray(((cpu + 1) * (core + 1)) - 1) = tempsensor
                    LoadSensorArray(((cpu + 1) * (core + 1)) - 1) = loadsensor
                End If

                'Grab sensor data from TempLoad.vb
                CurrentTemp = TLAvg.GetTempCurrent(cpu, core)
                CurrentLoad = TLAvg.GetLoadCurrent(cpu, core)
                TempSum = TempSum + CurrentTemp
                LoadSum = LoadSum + CurrentLoad
                'Publish data to MQTT
                ConnectMQTT.Publish(tempsensor, Encoding.UTF8.GetBytes(CurrentTemp))
                ConnectMQTT.Publish(loadsensor, Encoding.UTF8.GetBytes(CurrentLoad))

                'Print data to app window
                mqttouttemp = mqttouttemp & tempsensor & " = " & CurrentTemp & vbCrLf
                mqttoutload = mqttoutload & loadsensor & " = " & CurrentLoad & vbCrLf
            Next
        Next

        tempsensor = SensorHeading & "/allcores/temperatureaverage"
        loadsensor = SensorHeading & "/allcores/loadaverage"
        TempSum = Math.Round(TempSum / CTInfo.GetCoreCount, 1)
        LoadSum = Math.Round(LoadSum / CTInfo.GetCoreCount, 1)
        If Not (CurrentRunOnce) Then
            CurrentRunOnce = True

            For i = 0 To TempSensorArray.Count - 2
                SensorNames(sensorcount) = TempSensorArray(i)
                sensorcount = sensorcount + 1
            Next
            SensorNames(sensorcount) = tempsensor
            sensorcount = sensorcount + 1
            For i = 0 To LoadSensorArray.Count - 2
                SensorNames(sensorcount) = LoadSensorArray(i)
                sensorcount = sensorcount + 1
            Next
            SensorNames(sensorcount) = loadsensor
            sensorcount = sensorcount + 1
        End If

        ConnectMQTT.Publish(tempsensor, Encoding.UTF8.GetBytes(TempSum))
        ConnectMQTT.Publish(loadsensor, Encoding.UTF8.GetBytes(LoadSum))

        mqttoutaverages = tempsensor & " = " & TempSum & vbCrLf & loadsensor & " = " & LoadSum & vbCrLf

        txtCurrentSensors.Text = mqttoutheader & mqttouttemp & mqttoutload & mqttoutaverages

    End Sub

    Private Sub PushAverages_tick(sender As Object, e As EventArgs) Handles PushAverages.Tick

        Dim averagetempsensor As String
        Dim averageloadsensor As String
        Dim mqttouttemp As String
        Dim mqttoutload As String
        Dim AverageTemp As Integer
        Dim AverageLoad As Integer
        Dim TempSensorArray(PhysicalCPUs * CoresPerCPU) As String
        Dim LoadSensorArray(PhysicalCPUs * CoresPerCPU) As String

        If Not ConnectMQTT.IsConnected Then
            On Error Resume Next
            ConnectMQTT.Connect(Guid.NewGuid().ToString(), EditRegistry.GetUsername(), EditRegistry.GetPassword())

        End If
        mqttoutload = ""
        mqttouttemp = "MQTT Output" & vbCrLf
        mqttouttemp = mqttouttemp & "Connected: " & ConnectMQTT.IsConnected & vbCrLf
        mqttouttemp = mqttouttemp & "Port: " & ConnectMQTT.Settings.Port & vbCrLf

        For cpu As Integer = 0 To CTInfo.GetCPUCount - 1
            For core As Integer = 0 To CTInfo.GetCoreCount - 1
                'Generate sensor topics for MQTT
                averagetempsensor = SensorHeading & "/cpu" & cpu + 1 & "/core" & core + 1 & "/temperature/average"
                averageloadsensor = SensorHeading & "/cpu" & cpu + 1 & "/core" & core + 1 & "/load/average"

                If Not (AverageRunOnce) Then
                    TempSensorArray(((cpu + 1) * (core + 1)) - 1) = averagetempsensor
                    LoadSensorArray(((cpu + 1) * (core + 1)) - 1) = averageloadsensor
                End If

                'Grab sensor data from TempLoad.vb
                AverageTemp = TLAvg.GetTempAverage(cpu, core)
                AverageLoad = TLAvg.GetLoadAverage(cpu, core)

                'Publish data to MQTT
                ConnectMQTT.Publish(averagetempsensor, Encoding.UTF8.GetBytes(AverageTemp))
                ConnectMQTT.Publish(averageloadsensor, Encoding.UTF8.GetBytes(AverageLoad))

                'Print data to app window
                mqttouttemp = mqttouttemp & averagetempsensor & " = " & AverageTemp & vbCrLf
                mqttoutload = mqttoutload & averageloadsensor & " = " & AverageLoad & vbCrLf

            Next
        Next

        If Not (AverageRunOnce) Then
            AverageRunOnce = True
            For i = 0 To TempSensorArray.Count - 2
                SensorNames(sensorcount) = TempSensorArray(i)
                sensorcount = sensorcount + 1
            Next
            'sensorcount = sensorcount - 1
            For i = 0 To LoadSensorArray.Count - 2
                SensorNames(sensorcount) = LoadSensorArray(i)
                sensorcount = sensorcount + 1
            Next
            sensorcount = sensorcount - 1
            '           For i = 0 To sensorcount
            '           System.Diagnostics.Debug.WriteLine(SensorNames(i))
            '           Next
            ShowConnectMQTTSensorConfig()
        End If

        txtAverageSensors.Text = mqttouttemp & mqttoutload

    End Sub

    Private Sub ShowConnectMQTTSensorConfig()


        YAMLConfig = "sensor:" & vbCrLf

        For i = 0 To sensorcount
            YAMLConfig = YAMLConfig &
                         "  - platform: mqtt" & vbCrLf &
                         "    state_topic: """ & SensorNames(i) & """" & vbCrLf &
                         "    name: " & HostName.ToLower & "_" & SensorNames(i).Replace(SensorHeading & "/", "").Replace("/", "_") & vbCrLf
            If SensorNames(i).Replace(SensorHeading, "") = "/cpu/count" Then
                YAMLConfig = YAMLConfig & "    unit_of_measurement: 'CPU(s)'" & vbCrLf
            ElseIf SensorNames(i).Replace(SensorHeading, "") = "/cpu/core/count" Then
                YAMLConfig = YAMLConfig & "    unit_of_measurement: 'cores'" & vbCrLf
            ElseIf SensorNames(i).Replace(SensorHeading, "") = "/cpu/speed" Then
                YAMLConfig = YAMLConfig & "    unit_of_measurement: 'MHz'" & vbCrLf
            ElseIf SensorNames(i).Replace(SensorHeading, "") = "/cpu/voltage" Then
                YAMLConfig = YAMLConfig & "    unit_of_measurement: 'v'" & vbCrLf
            ElseIf SensorNames(i).IndexOf("temperature") >= 1 Then
                YAMLConfig = YAMLConfig & "    unit_of_measurement: '°" & TempType & "'" & vbCrLf
            ElseIf SensorNames(i).IndexOf("load") >= 1 Then
                YAMLConfig = YAMLConfig & "    unit_of_measurement: '%'" & vbCrLf
            End If
        Next

        txtYAML.Text = YAMLConfig
    End Sub

    Private Sub RefreshInfo_Tick(sender As Object, e As EventArgs) Handles RefreshInfo.Tick
        Dim TextOut As String
        Dim bReadSuccess As Boolean

        TextOut = "Core Temp shared memory reader:" & vbCrLf

        bReadSuccess = CTInfo.GetData()

        If bReadSuccess Then
            'Check whether temp is Celsius or dumb
            If CTInfo.IsFahrenheit Then
                TempType = "F"
            Else
                TempType = "C"
            End If

            FSBSpeed = CTInfo.GetFSBSpeed
            Multiplier = CTInfo.GetMultiplier
            CPUSpeed = CTInfo.GetCPUSpeed
            CPUSpeedText = CPUSpeed & "MHz (" & FSBSpeed & " x " & Multiplier & ")"
            CPUVID = CTInfo.GetVID
            CPUVIDText = CTInfo.GetVID & "v"

            If Not (CTRunOnce) Then
                CPUName = CTInfo.GetCPUName
                PhysicalCPUs = CTInfo.GetCPUCount
                CoresPerCPU = CTInfo.GetCoreCount

                SensorNames(0) = SensorHeading & "/cpu/hostname"
                SensorNames(1) = SensorHeading & "/cpu/name"
                SensorNames(2) = SensorHeading & "/cpu/count"
                SensorNames(3) = SensorHeading & "/cpu/core/count"
                SensorNames(4) = SensorHeading & "/cpu/fsbspeed"
                SensorNames(5) = SensorHeading & "/cpu/multiplier"
                SensorNames(6) = SensorHeading & "/cpu/speed"
                SensorNames(7) = SensorHeading & "/cpu/speed/text"
                SensorNames(8) = SensorHeading & "/cpu/voltage"
                SensorNames(9) = SensorHeading & "/cpu/voltage/text"
                SensorNames(10) = SensorHeading & "/cpu/temptype"
                sensorcount = 11
                CTRunOnce = True
            End If

            ' Publish Static info 
            ConnectMQTT.Publish(SensorNames(0), Encoding.UTF8.GetBytes(HostName))
            ConnectMQTT.Publish(SensorNames(1), Encoding.UTF8.GetBytes(CPUName))
            ConnectMQTT.Publish(SensorNames(2), Encoding.UTF8.GetBytes(PhysicalCPUs))
            ConnectMQTT.Publish(SensorNames(3), Encoding.UTF8.GetBytes(CoresPerCPU))



            CPUName = CTInfo.GetCPUName
            PhysicalCPUs = CTInfo.GetCPUCount
            CoresPerCPU = CTInfo.GetCoreCount

            'Output CPU and PC stats to app window
            TextOut = TextOut & "CPU Name: " & CPUName & vbCrLf
            TextOut = TextOut & "CPU Speed: " & CPUSpeed & vbCrLf
            TextOut = TextOut & "CPU VID: " & CPUVID & vbCrLf
            TextOut = TextOut & "Physical CPUs: " & PhysicalCPUs & vbCrLf
            TextOut = TextOut & "Cores per CPU: " & CoresPerCPU & vbCrLf

            'Loop through each CPU
            For cpu As Integer = 0 To CTInfo.GetCPUCount - 1
                'Output CPU# and Temp max to app window
                TextOut = TextOut & "CPU #" & (cpu + 1) & vbCrLf
                TextOut = TextOut & "Tj.Max: " & CTInfo.GetTjMax(cpu) & "°" & TempType & vbCrLf
                'Loop through each CORE
                For core As Integer = 0 To CTInfo.GetCoreCount - 1
                    OverallCoreCount = core + (cpu * CTInfo.GetCoreCount)
                    If (CTInfo.IsDistanceToTjMax) Then
                        TextOut = TextOut & "Core #" & OverallCoreCount + 1 & ": " & CTInfo.GetTemp(OverallCoreCount) & "°" & TempType & " to TjMax, " & CTInfo.GetCoreLoad(OverallCoreCount) & "% Load" & vbCrLf
                    Else
                        TextOut = TextOut & "Core #" & OverallCoreCount + 1 & ": " & CTInfo.GetTemp(OverallCoreCount) & "°" & TempType & ", " & CTInfo.GetCoreLoad(OverallCoreCount) & "% Load" & vbCrLf
                    End If

                    'Push new values TempLoad.vb
                    TLAvg.AddTempLoad(cpu, core, CTInfo.GetTemp(OverallCoreCount), CTInfo.GetCoreLoad(OverallCoreCount))

                Next
            Next
        txtCoreTempData.Text = TextOut
        End If
    End Sub

    Private Sub cmdStart_Click(sender As Object, e As EventArgs) Handles cmdStart.Click
        StartPolling()
    End Sub

    Private Sub StartPolling()
        ConnectMQTT = New uPLibrary.Networking.M2Mqtt.MqttClient(EditRegistry.GetAddress())
        ConnectMQTT.Connect(Guid.NewGuid().ToString(), EditRegistry.GetUsername(), EditRegistry.GetPassword())

        RefreshInfo.Interval = EditRegistry.GetPollCoreTemp()
        PushAverages.Interval = EditRegistry.GetPollMQTTAverage()
        PushCurrent.Interval = EditRegistry.GetPollMQTTCurrent()

        RefreshInfo.Start()
        PushAverages.Start()
        PushCurrent.Start()
        cmdStart.Enabled = False
        cmdStop.Enabled = True
        LockFields(False)
    End Sub

    Private Sub cmdStop_Click(sender As Object, e As EventArgs) Handles cmdStop.Click
        StopPolling()
    End Sub

    Private Sub StopPolling()
        RefreshInfo.Stop()
        PushAverages.Stop()
        PushCurrent.Stop()
        cmdStop.Enabled = False
        cmdStart.Enabled = True
        LockFields(True)
    End Sub

    Private Sub LockFields(TorF As Boolean)
        txtAddress.Enabled = TorF
        txtPort.Enabled = TorF
        txtUsername.Enabled = TorF
        txtPassword.Enabled = TorF
        txtPollCoreTemp.Enabled = TorF
        txtPollMQTTAverage.Enabled = TorF
        txtPollMQTTCurrent.Enabled = TorF
        cbxMinimise.Enabled = TorF
        cbxRunAtStart.Enabled = TorF
        cbxStartPolling.Enabled = TorF

    End Sub

    Private Sub Edited(sender As Object, e As EventArgs)
        cmdStop_Click(sender, e)
        cmdSave.Enabled = True
        cmdCancel.Enabled = True
    End Sub

    Private Sub txtAddress_LostFocus(sender As Object, e As EventArgs) Handles txtAddress.LostFocus
        Dim ip As System.Net.IPAddress
        Dim example As String = txtAddress.Text
        Dim IP_is_valid As Boolean = System.Net.IPAddress.TryParse(example, ip)
        Dim URL_is_valid As Boolean = ValidateUrl(example)

        If Not (IP_is_valid) And Not (URL_is_valid) Then
            MsgBox("Address is invalid.")
            txtAddress.Select()
        End If
    End Sub

    Private Sub txtAddress_TextChanged(sender As Object, e As EventArgs) Handles txtAddress.TextChanged

        Edited(sender, e)

    End Sub

    Private Function ValidateUrl(ByVal url As String) As Boolean
        Dim validatedUri As Uri = Nothing

        If Uri.TryCreate(url, UriKind.Absolute, validatedUri) Then
            Return (validatedUri.Scheme = Uri.UriSchemeHttp OrElse validatedUri.Scheme = Uri.UriSchemeHttps)
        End If

        Return False
    End Function

    Private Sub txtPort_TextChanged(sender As Object, e As EventArgs) Handles txtPort.TextChanged
        Edited(sender, e)
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged
        Edited(sender, e)
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        If txtPassword.Text <> "********" Then
            Edited(sender, e)
            PasswordEdit = True
        End If
    End Sub

    Private Sub txtUpdateTime_TextChanged(sender As Object, e As EventArgs) Handles txtPollCoreTemp.TextChanged
        Edited(sender, e)
    End Sub

    Private Sub txtMQTTCurrent_TextChanged(sender As Object, e As EventArgs) Handles txtPollMQTTCurrent.TextChanged
        Edited(sender, e)
    End Sub

    Private Sub txtMQTTAverage_TextChanged(sender As Object, e As EventArgs) Handles txtPollMQTTAverage.TextChanged
        Edited(sender, e)
    End Sub

    Private Sub cbxMinimise_CheckedChanged(sender As Object, e As EventArgs) Handles cbxMinimise.CheckedChanged
        Edited(sender, e)
    End Sub

    Private Sub cbxStartPolling_CheckedChanged(sender As Object, e As EventArgs) Handles cbxStartPolling.CheckedChanged
        Edited(sender, e)
    End Sub

    Private Sub cbxRunAtStart_CheckedChanged(sender As Object, e As EventArgs) Handles cbxRunAtStart.CheckedChanged
        Edited(sender, e)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim HKCUStartupPath As String
        HKCUStartupPath = "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"

        PasswordEdit = False
        EditRegistry.SaveAddress(txtAddress.Text)
        EditRegistry.SavePort(txtPort.Text)
        EditRegistry.SaveUsername(txtUsername.Text)
        If txtPassword.Text <> "********" Then
            EditRegistry.SavePassword(txtPassword.Text)
            txtPassword.Text = "********"
        End If
        EditRegistry.SavePollCoreTemp(txtPollCoreTemp.Text)
        EditRegistry.SavePollMQTTCurrent(txtPollMQTTCurrent.Text)
        EditRegistry.SavePollMQTTAverage(txtPollMQTTAverage.Text)
        EditRegistry.SaveRunAtStartup(cbxRunAtStart.Checked)
        EditRegistry.SaveMinimiseOnStart(cbxMinimise.Checked)
        EditRegistry.SaveStartPollingOnStart(cbxStartPolling.Checked)
        cmdSave.Enabled = False
        cmdCancel.Enabled = False

    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        PopulateForm()
        cmdSave.Enabled = False
        cmdCancel.Enabled = False
    End Sub

    Private Sub ntfyCT2MQTT_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ntfyCT2MQTT.MouseDoubleClick
        ToggleHide()
    End Sub

    Private Sub ntfyCT2MQTT_MouseClick(sender As Object, e As MouseEventArgs) Handles ntfyCT2MQTT.MouseClick

    End Sub

    Sub ToggleHide()
        If Me.WindowState = FormWindowState.Normal Then
            Me.ShowInTaskbar = False
            Me.WindowState = FormWindowState.Minimized
            Me.Hide()
        Else
            Me.Show()
            Me.ShowInTaskbar = True
            Me.WindowState = FormWindowState.Normal
            Me.Width = 932
            Me.Height = 543
        End If
    End Sub

    Private Sub tsmExit_Click(sender As Object, e As EventArgs) Handles tsmExit.Click
        Me.Close()
        End
    End Sub

    Private Sub tsmShowHide_Click(sender As Object, e As EventArgs) Handles tsmShowHide.Click
        ToggleHide()
    End Sub

    Private Sub frmCoreTempToMQTT_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        End
    End Sub

    Private Sub cmdShowYAML_Click(sender As Object, e As EventArgs) Handles cmdShowYAML.Click
        txtYAML.Visible = Not (txtYAML.Visible)
        If txtYAML.Visible = False Then
            cmdShowYAML.Text = "Show YAML Config"
        Else
            cmdShowYAML.Text = "Hide YAML Config"
        End If
    End Sub
End Class
