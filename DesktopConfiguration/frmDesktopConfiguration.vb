Imports System.Management.SelectQuery
Imports Microsoft.Win32
Imports System.IO
Imports System.Management
Imports System.Threading


'   Created By: Johnathan Martell
'   Date: 5/3/2018 (Publish Date)
'   
'   Description:    Provides a solution to Window's unintuitive methods of selecting desktop backgrounds,
'                   as this program allows you to specifically set wallpapers to specific monitors (without
'                   the need to create a folder features a range of images, and hope windows' slideshow selects
'                   the correct one.

'                   This project mainly serves as a means to demonstrate my knowledge in creating
'                   basic--yet functional--windows form applications utilizing VB.net.

Public Class frmDesktopConfiguration

    ' Declare the arrays associated to the various picture elements selected
    ' by the user
    Private photosLocationArray() As String = New String() {}
    Private resolutionWidthArray() As Integer = New Integer() {}
    Private photosArray() As Image = New Image() {}

    ' String variable used to hold the base directory of the program
    Private baseFolder As String = AppDomain.CurrentDomain.BaseDirectory.ToString

    ' Integer variable used to store the max-image hieght associated to the current layout for oriented-monitors
    Private maxImageHeight As Integer = 0

    ' form variable used to store a form object for the specific purpose of determining screen bounds.
    Private orientBegin As frmOrient = New frmOrient

    ' Integer variable used to store the current-amount of connected monitors
    Friend monitorCount As Integer = 0

    ' OpenFileDialog variable used to store a new object of the OpenFileDialog object for the purpose of allowing a user to select wallpapers
    Private fileSelection As OpenFileDialog = New OpenFileDialog()

    ' Integer variable used to store the currently selected-list box collection item index
    Private selectionValue As Integer = 0

    ' String variable used to store a local-path for the wallpaper the user wishes to set
    Private wallpaperLocalPath As String = ""

    ' String variable used to store the root-directory for the user's harddrive
    Private initialDirectoryLogged As String = "C:"

    ' Readonly String initialized to a path, specific to the wallpapers folder the program uses to handle multiple images
    Private ReadOnly localWallpaperFolderPath As String = Path.GetFullPath(Path.Combine(My.Application.Info.DirectoryPath, "..\..\wallpapers\")).ToString()

    ' String variable used to store the location for each image the user selects to build the overwall wallpaper
    Private defaultWallpaper As String = ""

    ' Timer object used to delay events which fire from this timer
    Private WithEvents tmrDelayEvent As System.Windows.Forms.Timer

    '---- Dealing with the windows INI files -------------
    Private currentSetWallpaperLocationKey As RegistryKey

    Private Const SPI_SETDESKWALLPAPER As Integer = &H14
    Private Const SPIF_UPDATEINIFILE As Integer = &H1
    Private Const SPIF_SENDWININICHANGE As Integer = &H2
    '----                                    -------------

    ' Constants

    ' Constant values for each monitor to signify their number
    Private Const MONITOR_ONE As Integer = 1
    Private Const MONITOR_TWO As Integer = 2
    Private Const MONITOR_THREE As Integer = 3

    ' Interval value which seperates the time in which a timer can fire (Every second, in this case)
    Private Const ERROR_CHECK_TIMER_INTERVAL As Integer = 1000

    ' Requirement auto function for updating the user32.dll file when changing the background image. These updates are crucial to the operation of the program
    Private Declare Auto Function SystemParametersInfo Lib "user32.dll" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer


    ' On the program load, instantiate various elements of the form
    Private Sub frmDesktopConfiguration_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        buildProgramStructure()

    End Sub

    ' Manages the changing of indexes via the lbxMonitor listbox
    Private Sub lbxMonitor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbxMonitor.SelectedIndexChanged

        selectionValue = lbxMonitor.SelectedIndex + 1

        ' If the selected-index of the listbox contains an associated image via its photosArray counterpart...
        If Not photosLocationArray(lbxMonitor.SelectedIndex).ToString = "" Then
            txtPhotoOverlay.Visible = False

            ' Set the image to the absolute file-path contained in the associated string array
            pbxPictureSelection.Image = Image.FromFile(photosLocationArray(lbxMonitor.SelectedIndex).ToString())

            ' Stretches the image to the current resolution of the Picture Selection image box 
            pbxPictureSelection.BackgroundImageLayout = ImageLayout.Stretch

        Else

            ' Set the image-value of the picture box to its default object for the Image-type, and display the overlay text
            pbxPictureSelection.Image = Nothing

            txtPhotoOverlay.Visible = True

        End If
    End Sub

    ' Selecting a picture associated to the picturebox (Click Event)
    Private Sub pbxPictureSelection_Click(sender As Object, e As EventArgs) Handles pbxPictureSelection.Click

        ' Ensure that an index of the listbox has been selected
        If lbxMonitor.SelectedIndex = -1 Then

            MessageBox.Show("Please select a monitor.")

        Else

            ' If an element has been selected, open a file selection dialog
            fileSelection.Title = "Please select an image"
            fileSelection.InitialDirectory = initialDirectoryLogged
            fileSelection.Filter = "JPG|*.jpg|Bitmap|*.bmp|PNG|*.png"
            fileSelection.ShowDialog()

            ' Grab the file-name of the selected image, and store the path(String) to the photosArray's relative index
            photosLocationArray(lbxMonitor.SelectedIndex) = fileSelection.FileName.ToString()

            ' If the user did not cancel the dialog (An image WAS selected)...
            If photosLocationArray(lbxMonitor.SelectedIndex) IsNot "" Then

                photosArray(lbxMonitor.SelectedIndex) = Image.FromFile(photosLocationArray(lbxMonitor.SelectedIndex).ToString())

                ' Set the image of the picturebox to the image associated to the filepath in the relative index of the photosArray(Currently-selected listbox index based)
                pbxPictureSelection.Image = photosArray(lbxMonitor.SelectedIndex)

                ' Save the image the user selected as a "Definition" for an image to the workingfiles directory. Signify the monitor-number at the end of the filename
                pbxPictureSelection.Image.Save(Path.GetFullPath(Path.Combine(localWallpaperFolderPath, "workingfiles\def" & lbxMonitor.SelectedIndex + 1 & ".Bmp")), System.Drawing.Imaging.ImageFormat.Bmp)

                ' Hide the overlay text for the picture box
                txtPhotoOverlay.Visible = False

                ' 
                wallpaperLocalPath = photosLocationArray(lbxMonitor.SelectedIndex).ToString().Substring(0, (photosLocationArray(lbxMonitor.SelectedIndex).LastIndexOf("\")))

                initialDirectoryLogged = wallpaperLocalPath

            End If

        End If

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        Application.Exit()

    End Sub

    ' Provides an avenue for setting a selected image to a specific monitor in the list box
    Private Sub btnConfirmSelection_Click(sender As Object, e As EventArgs) Handles btnConfirmSelection.Click
        SetWallpaper()
    End Sub

    ' Provides information for the purposes of debugging.
    Private Function ReturnDisplayInfo()

        Dim generatedString As String = ""
        Try

            Dim q As System.Management.SelectQuery = New System.Management.SelectQuery("SELECT Name, DeviceID FROM Win32_PnPEntity WHERE service='monitor'")

            Using mos As System.Management.ManagementObjectSearcher = New System.Management.ManagementObjectSearcher(q)

                For Each mo As System.Management.ManagementObject In mos.Get()

                    Dim stringComponent As String = ""

                    generatedString += "Device Name: " & mo.Properties.Item("Name").Value.ToString() & vbCrLf

                    stringComponent += mo.Properties.Item("DeviceID").Value.ToString()

                    stringComponent = stringComponent.Substring(8, stringComponent.IndexOf("\"))

                    generatedString += "Device ID: " & stringComponent & vbCrLf

                Next

            End Using

        Catch e As ManagementException

            MessageBox.Show("An error occurred while querying for WMI data: " + e.Message)
        End Try


        Return generatedString
    End Function

    ' Builds the basic structure of the program on form load, and instances which require resetting to the default-program state
    Private Sub buildProgramStructure()
        tmrDetectChanges.Start()

        Me.ControlBox = False

        ' Count the amount of monitors currently loaded using the AllScreens class, and use this value to
        ' instantiate the bounds of each array, which are inherently dependant on the amount of monitors present

        monitorCount = Screen.AllScreens.Count

        lblMonitorCount.Text = monitorCount

        resolutionWidthArray = New Integer(monitorCount - 1) {}

        photosLocationArray = New String(monitorCount - 1) {}

        photosArray = New Image(monitorCount - 1) {}

        lbxMonitor.Items.Clear()

        defaultWallpaper = Path.GetFullPath(Path.Combine(localWallpaperFolderPath, "wallpaper" & monitorCount & ".Bmp"))

        ' Allow the user to select the monitor located at each index of the Items collection within lbxMonitor
        ' by adding "Monitor" + count to each index. Also, set each index of its associated photo-path array to an empty string
        For i As Integer = 0 To monitorCount - 1

            Dim buildString As String = ""

            buildString = "Monitor " & i + 1

            If (monitorCount > MONITOR_ONE) Then

                If monitorCount = MONITOR_TWO Then

                    If (i = 0) Then

                        buildString = buildString & "(Left)"

                    Else

                        buildString = buildString & "(Right)"

                    End If

                Else

                    If (i = 0) Then

                        buildString = buildString & "(Left)"

                    ElseIf i = 1 Then

                        buildString = buildString & "(Center)"

                    Else

                        buildString = buildString & "(Right)"

                    End If

                End If

            Else

                buildString = buildString & "(Main)"

            End If

            lbxMonitor.Items.Insert(i, buildString)

            photosLocationArray(i) = ""

        Next


        ' Default the selected index of the Monitor listbox to 0 (first value)
        lbxMonitor.SelectedIndex = 0

        ' Call the ReturnDisplayInfo method to retrieve basic information for each monitor.
        rtbDebug.Text = ReturnDisplayInfo()

        ' Call the getScreenBounds method
        getScreenBounds()
    End Sub

    ' Handles the size of the form and the width/height of the current form-location's screen, and centers the form accordingly
    Private Sub getScreenBounds()

        ' Two variables to hold the width and height of all screens connected
        Dim xWidth As Integer = 0
        Dim yHeight As Integer = 0

        ' Loop through each monitor...
        For i As Integer = 0 To monitorCount - 1

            ' Set an invisible form to the first x-pixel location using the X axis contained in xWidth, adding 1 to it. Keep the location on the 0 y-Axis
            orientBegin.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(xWidth + 1, 0)

            ' If the height of the current form's screen surpasses a previously-held height...
            If Screen.FromControl(orientBegin).Bounds.Height > yHeight Then
                ' Set the height.
                yHeight = Screen.FromControl(orientBegin).Bounds.Height
            End If
            ' Set the bound of width (for the current screen) to an associated index in the resolutionWidthArray array.
            resolutionWidthArray(i) = Screen.FromControl(orientBegin).Bounds.Width
            ' Accumulate the width of the current screen to the xWidth variable
            xWidth += resolutionWidthArray(i)

        Next

        ' Set the maximum height for a new wallpaper
        maxImageHeight = yHeight

    End Sub

    ' Method used to set the background wallpaper to a generated bitmap image
    Private Sub SetWallpaper()

        ' Flag-check the photosLocationArray for any invalid data (no data)
        Dim imageLocFlag As Boolean = True

        For i As Integer = 0 To monitorCount - 1
            If photosLocationArray(i).ToString = "" Then
                imageLocFlag = False
            End If

        Next

        ' If the flag maintained its status of true
        If imageLocFlag = True Then

            ' Check the monitor count and act accordingly
            If monitorCount = 1 Then

                ' Use the overloaded method of SetWallpaper, and send an image to it
                SetWallpaper(photosArray(0))


            Else

                ' Generate a variable used to store the maximum width of the image
                Dim maxXWidth As Integer = 0

                For i As Integer = 0 To monitorCount - 1
                    maxXWidth += resolutionWidthArray(i)
                Next

                Dim wallpaperLocal As New Bitmap(maxXWidth, maxImageHeight)

                Using g As Graphics = Graphics.FromImage(wallpaperLocal)

                    Dim xCord As Integer = 0

                    For i As Integer = 0 To monitorCount - 1

                        Using image As New Bitmap(photosLocationArray(i))
                            g.DrawImage(image, xCord, 0, resolutionWidthArray(i), maxImageHeight)
                        End Using
                        xCord += resolutionWidthArray(i)
                    Next

                End Using

                SetWallpaper(wallpaperLocal)
            End If
        Else
            MsgBox("Ensure that all monitors have an associated image before proceeding.")
        End If
    End Sub

    ' Overloaded method of SetWallpaper used to set the background image to the image sent through the img Parameter
    Private Sub SetWallpaper(ByVal img As Image)

        Dim imageLocation As String = defaultWallpaper

        Try

            img.Save(defaultWallpaper, System.Drawing.Imaging.ImageFormat.Bmp)

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imageLocation, SPIF_UPDATEINIFILE Or SPIF_SENDWININICHANGE)

        Catch Ex As Exception

            MsgBox("There was an error setting the wallpaper: " & Ex.Message)

        End Try

    End Sub

    ' Timer user to detect changes which are crucial to the basic functionality of the program (E.G: Disconnecting a monitor during runtime)
    Private Sub tmrDetectChanges_Tick(sender As Object, e As EventArgs) Handles tmrDetectChanges.Tick

        If monitorCount <> Screen.AllScreens.Count() Then

            tmrDetectChanges.Stop()

            tmrDelayEvent = New System.Windows.Forms.Timer

            tmrDelayEvent.Interval = ERROR_CHECK_TIMER_INTERVAL

            tmrDelayEvent.Start()

        End If

    End Sub

    ' Fires on the tick-event of the timer associated with it
    Private Sub fireOnWait() Handles tmrDelayEvent.Tick

        tmrDelayEvent.Stop()

        buildProgramStructure()

        monitorCount = Screen.AllScreens.Count()

        fireOnUnexpectedDisconnect()
    End Sub

    ' Fired through the fireOnWait method, used to handle the event in which a monitor is unexpectedly disconnected
    Private Sub fireOnUnexpectedDisconnect()
        Dim generatedImage As String = Path.GetFullPath(Path.Combine(localWallpaperFolderPath, "generatedImage.bmp"))

        For i As Integer = 0 To monitorCount - 1

            photosLocationArray(i) = generatedImage

            photosArray(i) = Image.FromFile(photosLocationArray(i).ToString())

            ' Set the image of the picturebox to the image associated to the filepath in the relative index of the photosArray(Currently-selected listbox index based)
            pbxPictureSelection.Image = photosArray(i)

            ' Hide the overlay text for the picture box
            txtPhotoOverlay.Visible = False


        Next


        SetWallpaper()
    End Sub
End Class
