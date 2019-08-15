<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCoreTempToMQTT
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCoreTempToMQTT))
        Me.txtCoreTempData = New System.Windows.Forms.TextBox()
        Me.RefreshInfo = New System.Windows.Forms.Timer(Me.components)
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.lblPollCoreTemp = New System.Windows.Forms.Label()
        Me.txtPollCoreTemp = New System.Windows.Forms.TextBox()
        Me.PushAverages = New System.Windows.Forms.Timer(Me.components)
        Me.PushCurrent = New System.Windows.Forms.Timer(Me.components)
        Me.lblPollMQTTCurrent = New System.Windows.Forms.Label()
        Me.txtPollMQTTCurrent = New System.Windows.Forms.TextBox()
        Me.lblPollMQTTAverage = New System.Windows.Forms.Label()
        Me.txtPollMQTTAverage = New System.Windows.Forms.TextBox()
        Me.txtCurrentSensors = New System.Windows.Forms.TextBox()
        Me.txtAverageSensors = New System.Windows.Forms.TextBox()
        Me.lblCoreTempData = New System.Windows.Forms.Label()
        Me.lblCurrentSensors = New System.Windows.Forms.Label()
        Me.lblAverageSensors = New System.Windows.Forms.Label()
        Me.cbxRunAtStart = New System.Windows.Forms.CheckBox()
        Me.cbxMinimise = New System.Windows.Forms.CheckBox()
        Me.cbxStartPolling = New System.Windows.Forms.CheckBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.ntfyCT2MQTT = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.mnuTray = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmShowHide = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdShowYAML = New System.Windows.Forms.Button()
        Me.txtYAML = New System.Windows.Forms.TextBox()
        Me.mnuTray.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCoreTempData
        '
        Me.txtCoreTempData.Location = New System.Drawing.Point(12, 26)
        Me.txtCoreTempData.Multiline = True
        Me.txtCoreTempData.Name = "txtCoreTempData"
        Me.txtCoreTempData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCoreTempData.Size = New System.Drawing.Size(443, 206)
        Me.txtCoreTempData.TabIndex = 0
        '
        'RefreshInfo
        '
        Me.RefreshInfo.Interval = 1000
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(591, 40)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(124, 20)
        Me.txtAddress.TabIndex = 1
        Me.txtAddress.Tag = "IP address of your MQTT broker (ie mosquitto)"
        Me.txtAddress.Text = "0.0.0.0"
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(591, 66)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(124, 20)
        Me.txtUsername.TabIndex = 2
        Me.txtUsername.Tag = "Username for MQTT broker"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(591, 92)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(124, 20)
        Me.txtPassword.TabIndex = 3
        Me.txtPassword.Tag = "Password for MQTT broker"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(736, 40)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(43, 20)
        Me.txtPort.TabIndex = 4
        Me.txtPort.Tag = "Port number of your MQTT broker (default 1883)"
        Me.txtPort.Text = "1883"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(540, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Address"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(530, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Username"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(530, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Password"
        '
        'cmdStart
        '
        Me.cmdStart.Location = New System.Drawing.Point(736, 141)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(124, 22)
        Me.cmdStart.TabIndex = 8
        Me.cmdStart.Tag = "Starts grabbing data from Core Temp and pushes it to MQTT"
        Me.cmdStart.Text = "Start polling"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'cmdStop
        '
        Me.cmdStop.Location = New System.Drawing.Point(736, 168)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(124, 22)
        Me.cmdStop.TabIndex = 9
        Me.cmdStop.Tag = "Stops grabbing data from Core Temp and pushes it to MQTT"
        Me.cmdStop.Text = "Stop polling"
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'lblPollCoreTemp
        '
        Me.lblPollCoreTemp.AutoSize = True
        Me.lblPollCoreTemp.Location = New System.Drawing.Point(484, 121)
        Me.lblPollCoreTemp.Name = "lblPollCoreTemp"
        Me.lblPollCoreTemp.Size = New System.Drawing.Size(101, 13)
        Me.lblPollCoreTemp.TabIndex = 11
        Me.lblPollCoreTemp.Text = "Poll Core Temp (ms)"
        '
        'txtPollCoreTemp
        '
        Me.txtPollCoreTemp.Location = New System.Drawing.Point(591, 118)
        Me.txtPollCoreTemp.Name = "txtPollCoreTemp"
        Me.txtPollCoreTemp.Size = New System.Drawing.Size(124, 20)
        Me.txtPollCoreTemp.TabIndex = 10
        Me.txtPollCoreTemp.Tag = "Time in milliseconds to poll Core Temp for new data (default 1000)"
        Me.txtPollCoreTemp.Text = "1000"
        '
        'PushAverages
        '
        '
        'PushCurrent
        '
        '
        'lblPollMQTTCurrent
        '
        Me.lblPollMQTTCurrent.AutoSize = True
        Me.lblPollMQTTCurrent.Location = New System.Drawing.Point(488, 147)
        Me.lblPollMQTTCurrent.Name = "lblPollMQTTCurrent"
        Me.lblPollMQTTCurrent.Size = New System.Drawing.Size(97, 13)
        Me.lblPollMQTTCurrent.TabIndex = 13
        Me.lblPollMQTTCurrent.Text = "MQTT Current (ms)"
        '
        'txtPollMQTTCurrent
        '
        Me.txtPollMQTTCurrent.Location = New System.Drawing.Point(591, 144)
        Me.txtPollMQTTCurrent.Name = "txtPollMQTTCurrent"
        Me.txtPollMQTTCurrent.Size = New System.Drawing.Size(124, 20)
        Me.txtPollMQTTCurrent.TabIndex = 12
        Me.txtPollMQTTCurrent.Tag = "Time in milliseconds to push current data to MQTT (default 5000)"
        Me.txtPollMQTTCurrent.Text = "5000"
        '
        'lblPollMQTTAverage
        '
        Me.lblPollMQTTAverage.AutoSize = True
        Me.lblPollMQTTAverage.Location = New System.Drawing.Point(482, 174)
        Me.lblPollMQTTAverage.Name = "lblPollMQTTAverage"
        Me.lblPollMQTTAverage.Size = New System.Drawing.Size(103, 13)
        Me.lblPollMQTTAverage.TabIndex = 15
        Me.lblPollMQTTAverage.Text = "MQTT Average (ms)"
        '
        'txtPollMQTTAverage
        '
        Me.txtPollMQTTAverage.Location = New System.Drawing.Point(591, 170)
        Me.txtPollMQTTAverage.Name = "txtPollMQTTAverage"
        Me.txtPollMQTTAverage.Size = New System.Drawing.Size(124, 20)
        Me.txtPollMQTTAverage.TabIndex = 14
        Me.txtPollMQTTAverage.Tag = "Time in milliseconds to push average data to MQTT (default 10000)"
        Me.txtPollMQTTAverage.Text = "10000"
        '
        'txtCurrentSensors
        '
        Me.txtCurrentSensors.Location = New System.Drawing.Point(12, 254)
        Me.txtCurrentSensors.Multiline = True
        Me.txtCurrentSensors.Name = "txtCurrentSensors"
        Me.txtCurrentSensors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCurrentSensors.Size = New System.Drawing.Size(443, 217)
        Me.txtCurrentSensors.TabIndex = 16
        '
        'txtAverageSensors
        '
        Me.txtAverageSensors.Location = New System.Drawing.Point(461, 254)
        Me.txtAverageSensors.Multiline = True
        Me.txtAverageSensors.Name = "txtAverageSensors"
        Me.txtAverageSensors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAverageSensors.Size = New System.Drawing.Size(443, 217)
        Me.txtAverageSensors.TabIndex = 17
        '
        'lblCoreTempData
        '
        Me.lblCoreTempData.AutoSize = True
        Me.lblCoreTempData.Location = New System.Drawing.Point(12, 9)
        Me.lblCoreTempData.Name = "lblCoreTempData"
        Me.lblCoreTempData.Size = New System.Drawing.Size(85, 13)
        Me.lblCoreTempData.TabIndex = 18
        Me.lblCoreTempData.Text = "Core Temp Data"
        '
        'lblCurrentSensors
        '
        Me.lblCurrentSensors.AutoSize = True
        Me.lblCurrentSensors.Location = New System.Drawing.Point(12, 238)
        Me.lblCurrentSensors.Name = "lblCurrentSensors"
        Me.lblCurrentSensors.Size = New System.Drawing.Size(82, 13)
        Me.lblCurrentSensors.TabIndex = 19
        Me.lblCurrentSensors.Text = "Current Sensors"
        '
        'lblAverageSensors
        '
        Me.lblAverageSensors.AutoSize = True
        Me.lblAverageSensors.Location = New System.Drawing.Point(458, 238)
        Me.lblAverageSensors.Name = "lblAverageSensors"
        Me.lblAverageSensors.Size = New System.Drawing.Size(88, 13)
        Me.lblAverageSensors.TabIndex = 20
        Me.lblAverageSensors.Text = "Average Sensors"
        '
        'cbxRunAtStart
        '
        Me.cbxRunAtStart.AutoSize = True
        Me.cbxRunAtStart.Location = New System.Drawing.Point(736, 72)
        Me.cbxRunAtStart.Name = "cbxRunAtStart"
        Me.cbxRunAtStart.Size = New System.Drawing.Size(158, 17)
        Me.cbxRunAtStart.TabIndex = 21
        Me.cbxRunAtStart.Text = "Run app at windows startup"
        Me.cbxRunAtStart.UseVisualStyleBackColor = True
        '
        'cbxMinimise
        '
        Me.cbxMinimise.AutoSize = True
        Me.cbxMinimise.Location = New System.Drawing.Point(736, 95)
        Me.cbxMinimise.Name = "cbxMinimise"
        Me.cbxMinimise.Size = New System.Drawing.Size(104, 17)
        Me.cbxMinimise.TabIndex = 22
        Me.cbxMinimise.Text = "Minimise on start"
        Me.cbxMinimise.UseVisualStyleBackColor = True
        '
        'cbxStartPolling
        '
        Me.cbxStartPolling.AutoSize = True
        Me.cbxStartPolling.Checked = True
        Me.cbxStartPolling.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbxStartPolling.Location = New System.Drawing.Point(736, 118)
        Me.cbxStartPolling.Name = "cbxStartPolling"
        Me.cbxStartPolling.Size = New System.Drawing.Size(131, 17)
        Me.cbxStartPolling.TabIndex = 23
        Me.cbxStartPolling.Text = "Start polling on startup"
        Me.cbxStartPolling.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = False
        Me.cmdSave.Location = New System.Drawing.Point(736, 199)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(59, 22)
        Me.cmdSave.TabIndex = 24
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Enabled = False
        Me.cmdCancel.Location = New System.Drawing.Point(801, 199)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(59, 22)
        Me.cmdCancel.TabIndex = 25
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'ntfyCT2MQTT
        '
        Me.ntfyCT2MQTT.ContextMenuStrip = Me.mnuTray
        Me.ntfyCT2MQTT.Icon = CType(resources.GetObject("ntfyCT2MQTT.Icon"), System.Drawing.Icon)
        Me.ntfyCT2MQTT.Text = "CT2MQTT"
        Me.ntfyCT2MQTT.Visible = True
        '
        'mnuTray
        '
        Me.mnuTray.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmShowHide, Me.tsmExit})
        Me.mnuTray.Name = "mnuTray"
        Me.mnuTray.Size = New System.Drawing.Size(137, 48)
        '
        'tsmShowHide
        '
        Me.tsmShowHide.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsmShowHide.Name = "tsmShowHide"
        Me.tsmShowHide.Size = New System.Drawing.Size(136, 22)
        Me.tsmShowHide.Text = "Show/Hide"
        '
        'tsmExit
        '
        Me.tsmExit.Name = "tsmExit"
        Me.tsmExit.Size = New System.Drawing.Size(136, 22)
        Me.tsmExit.Text = "Exit"
        '
        'cmdShowYAML
        '
        Me.cmdShowYAML.Location = New System.Drawing.Point(780, 477)
        Me.cmdShowYAML.Name = "cmdShowYAML"
        Me.cmdShowYAML.Size = New System.Drawing.Size(124, 22)
        Me.cmdShowYAML.TabIndex = 26
        Me.cmdShowYAML.Tag = "Shows YAML config setup for sensors in HASSIO "
        Me.cmdShowYAML.Text = "Show YAML Config"
        Me.cmdShowYAML.UseVisualStyleBackColor = True
        '
        'txtYAML
        '
        Me.txtYAML.Location = New System.Drawing.Point(12, 9)
        Me.txtYAML.Multiline = True
        Me.txtYAML.Name = "txtYAML"
        Me.txtYAML.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtYAML.Size = New System.Drawing.Size(892, 462)
        Me.txtYAML.TabIndex = 27
        Me.txtYAML.Visible = False
        '
        'frmCoreTempToMQTT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(916, 504)
        Me.Controls.Add(Me.txtYAML)
        Me.Controls.Add(Me.cmdShowYAML)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cbxStartPolling)
        Me.Controls.Add(Me.cbxMinimise)
        Me.Controls.Add(Me.cbxRunAtStart)
        Me.Controls.Add(Me.lblAverageSensors)
        Me.Controls.Add(Me.lblCurrentSensors)
        Me.Controls.Add(Me.lblCoreTempData)
        Me.Controls.Add(Me.txtAverageSensors)
        Me.Controls.Add(Me.txtCurrentSensors)
        Me.Controls.Add(Me.lblPollMQTTAverage)
        Me.Controls.Add(Me.txtPollMQTTAverage)
        Me.Controls.Add(Me.lblPollMQTTCurrent)
        Me.Controls.Add(Me.txtPollMQTTCurrent)
        Me.Controls.Add(Me.lblPollCoreTemp)
        Me.Controls.Add(Me.txtPollCoreTemp)
        Me.Controls.Add(Me.cmdStop)
        Me.Controls.Add(Me.cmdStart)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.txtCoreTempData)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCoreTempToMQTT"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "CT2MQTT"
        Me.mnuTray.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtCoreTempData As TextBox
    Friend WithEvents RefreshInfo As Timer
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtPort As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmdStart As Button
    Friend WithEvents cmdStop As Button
    Friend WithEvents lblPollCoreTemp As Label
    Friend WithEvents txtPollCoreTemp As TextBox
    Friend WithEvents PushAverages As Timer
    Friend WithEvents PushCurrent As Timer
    Friend WithEvents lblPollMQTTCurrent As Label
    Friend WithEvents txtPollMQTTCurrent As TextBox
    Friend WithEvents lblPollMQTTAverage As Label
    Friend WithEvents txtPollMQTTAverage As TextBox
    Friend WithEvents txtCurrentSensors As TextBox
    Friend WithEvents txtAverageSensors As TextBox
    Friend WithEvents lblCoreTempData As Label
    Friend WithEvents lblCurrentSensors As Label
    Friend WithEvents lblAverageSensors As Label
    Friend WithEvents cbxRunAtStart As CheckBox
    Friend WithEvents cbxMinimise As CheckBox
    Friend WithEvents cbxStartPolling As CheckBox
    Friend WithEvents cmdSave As Button
    Friend WithEvents cmdCancel As Button
    Friend WithEvents ntfyCT2MQTT As NotifyIcon
    Friend WithEvents tsmShowHide As ToolStripMenuItem
    Friend WithEvents tsmExit As ToolStripMenuItem
    Friend WithEvents mnuTray As ContextMenuStrip
    Friend WithEvents cmdShowYAML As Button
    Friend WithEvents txtYAML As TextBox
End Class
