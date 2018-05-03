<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDesktopConfiguration
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pbxPictureSelection = New System.Windows.Forms.PictureBox()
        Me.btnConfirmSelection = New System.Windows.Forms.Button()
        Me.lbxMonitor = New System.Windows.Forms.ListBox()
        Me.lblMonitorPretext = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblMonitorCountPretext = New System.Windows.Forms.Label()
        Me.lblMonitorCount = New System.Windows.Forms.Label()
        Me.lblDebug = New System.Windows.Forms.Label()
        Me.rtbDebug = New System.Windows.Forms.TextBox()
        Me.txtPhotoOverlay = New System.Windows.Forms.TextBox()
        Me.tmrDetectChanges = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pbxPictureSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbxPictureSelection
        '
        Me.pbxPictureSelection.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.pbxPictureSelection.Location = New System.Drawing.Point(12, 27)
        Me.pbxPictureSelection.Name = "pbxPictureSelection"
        Me.pbxPictureSelection.Size = New System.Drawing.Size(407, 235)
        Me.pbxPictureSelection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxPictureSelection.TabIndex = 0
        Me.pbxPictureSelection.TabStop = False
        '
        'btnConfirmSelection
        '
        Me.btnConfirmSelection.Location = New System.Drawing.Point(258, 273)
        Me.btnConfirmSelection.Name = "btnConfirmSelection"
        Me.btnConfirmSelection.Size = New System.Drawing.Size(161, 23)
        Me.btnConfirmSelection.TabIndex = 1
        Me.btnConfirmSelection.Text = "Set Background"
        Me.btnConfirmSelection.UseVisualStyleBackColor = True
        '
        'lbxMonitor
        '
        Me.lbxMonitor.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbxMonitor.FormattingEnabled = True
        Me.lbxMonitor.ItemHeight = 18
        Me.lbxMonitor.Location = New System.Drawing.Point(62, 273)
        Me.lbxMonitor.Name = "lbxMonitor"
        Me.lbxMonitor.Size = New System.Drawing.Size(190, 22)
        Me.lbxMonitor.TabIndex = 2
        '
        'lblMonitorPretext
        '
        Me.lblMonitorPretext.AutoSize = True
        Me.lblMonitorPretext.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonitorPretext.Location = New System.Drawing.Point(9, 274)
        Me.lblMonitorPretext.Name = "lblMonitorPretext"
        Me.lblMonitorPretext.Size = New System.Drawing.Size(53, 17)
        Me.lblMonitorPretext.TabIndex = 3
        Me.lblMonitorPretext.Text = "Set to: "
        '
        'btnExit
        '
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Location = New System.Drawing.Point(12, 301)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(407, 32)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lblMonitorCountPretext
        '
        Me.lblMonitorCountPretext.AutoSize = True
        Me.lblMonitorCountPretext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonitorCountPretext.Location = New System.Drawing.Point(12, 8)
        Me.lblMonitorCountPretext.Name = "lblMonitorCountPretext"
        Me.lblMonitorCountPretext.Size = New System.Drawing.Size(105, 13)
        Me.lblMonitorCountPretext.TabIndex = 5
        Me.lblMonitorCountPretext.Text = "Number of Monitors: "
        '
        'lblMonitorCount
        '
        Me.lblMonitorCount.AutoSize = True
        Me.lblMonitorCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMonitorCount.Location = New System.Drawing.Point(119, 7)
        Me.lblMonitorCount.MinimumSize = New System.Drawing.Size(50, 17)
        Me.lblMonitorCount.Name = "lblMonitorCount"
        Me.lblMonitorCount.Size = New System.Drawing.Size(50, 17)
        Me.lblMonitorCount.TabIndex = 6
        '
        'lblDebug
        '
        Me.lblDebug.AutoSize = True
        Me.lblDebug.Location = New System.Drawing.Point(428, 11)
        Me.lblDebug.Name = "lblDebug"
        Me.lblDebug.Size = New System.Drawing.Size(97, 13)
        Me.lblDebug.TabIndex = 8
        Me.lblDebug.Text = "Debug Information:"
        '
        'rtbDebug
        '
        Me.rtbDebug.Enabled = False
        Me.rtbDebug.Location = New System.Drawing.Point(425, 27)
        Me.rtbDebug.Multiline = True
        Me.rtbDebug.Name = "rtbDebug"
        Me.rtbDebug.ReadOnly = True
        Me.rtbDebug.Size = New System.Drawing.Size(237, 306)
        Me.rtbDebug.TabIndex = 9
        '
        'txtPhotoOverlay
        '
        Me.txtPhotoOverlay.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.txtPhotoOverlay.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPhotoOverlay.Enabled = False
        Me.txtPhotoOverlay.Location = New System.Drawing.Point(29, 38)
        Me.txtPhotoOverlay.Multiline = True
        Me.txtPhotoOverlay.Name = "txtPhotoOverlay"
        Me.txtPhotoOverlay.ReadOnly = True
        Me.txtPhotoOverlay.Size = New System.Drawing.Size(376, 209)
        Me.txtPhotoOverlay.TabIndex = 7
        Me.txtPhotoOverlay.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click to select an Image..."
        Me.txtPhotoOverlay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tmrDetectChanges
        '
        '
        'frmDesktopConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(674, 341)
        Me.Controls.Add(Me.rtbDebug)
        Me.Controls.Add(Me.lblDebug)
        Me.Controls.Add(Me.txtPhotoOverlay)
        Me.Controls.Add(Me.lblMonitorCount)
        Me.Controls.Add(Me.lblMonitorCountPretext)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblMonitorPretext)
        Me.Controls.Add(Me.lbxMonitor)
        Me.Controls.Add(Me.btnConfirmSelection)
        Me.Controls.Add(Me.pbxPictureSelection)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmDesktopConfiguration"
        Me.Text = "Desktop Management"
        CType(Me.pbxPictureSelection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbxPictureSelection As System.Windows.Forms.PictureBox
    Friend WithEvents btnConfirmSelection As System.Windows.Forms.Button
    Friend WithEvents lbxMonitor As System.Windows.Forms.ListBox
    Friend WithEvents lblMonitorPretext As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblMonitorCountPretext As System.Windows.Forms.Label
    Friend WithEvents lblMonitorCount As System.Windows.Forms.Label
    Friend WithEvents lblDebug As System.Windows.Forms.Label
    Friend WithEvents rtbDebug As System.Windows.Forms.TextBox
    Friend WithEvents txtPhotoOverlay As System.Windows.Forms.TextBox
    Friend WithEvents tmrDetectChanges As System.Windows.Forms.Timer

End Class
