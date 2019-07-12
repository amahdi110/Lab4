Option Strict On

''' <summary>
''' Author: Ali Hassan Mahdi
''' Date: July 12
''' Description: This application allows the user to enter their car inventory
''' Disclaimer: This project has been completed with help from CustomersList example project
''' </summary>

Public Class frmCarInventory

    Private carList As New SortedList
    Private currentCarID As String = String.Empty
    Private editMode As Boolean = False

    ''' <summary>
    ''' This method validates the user's input and enters the entry into the car list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        Dim car As Car
        Dim carItem As ListViewItem

        If IsValidInput() = True Then

            editMode = True

            lbResult.Text = "It worked!"

            Dim price As Double
            Double.TryParse(txtPrice.Text.Trim, price)
            price = Math.Round(price, 2)
            Dim carPrice As String = CStr(price)

            If currentCarID.Trim.Length = 0 Then

                car = New Car(cmbMake.Text, txtModel.Text, cmbYear.Text, carPrice, chkNew.Checked)
                carList.Add(car.IdentificationNumber.ToString(), car)

            Else

                car = CType(carList.Item(currentCarID), Car)

                car.CarMake = cmbMake.Text
                car.CarModel = txtModel.Text
                car.CarYear = cmbYear.Text
                car.IsNewCar = chkNew.Checked
                car.CarPrice = carPrice
            End If

            lvwCars.Items.Clear()

            For Each customerEntry As DictionaryEntry In carList

                carItem = New ListViewItem()

                car = CType(customerEntry.Value, Car)

                carItem.Checked = car.IsNewCar
                carItem.SubItems.Add(car.IdentificationNumber.ToString())
                carItem.SubItems.Add(car.CarMake)
                carItem.SubItems.Add(car.CarModel)
                carItem.SubItems.Add(car.CarYear)
                carItem.SubItems.Add("$" + car.CarPrice)

                lvwCars.Items.Add(carItem)

            Next customerEntry

            Reset()

            editMode = False

        End If

    End Sub

    ''' <summary>
    ''' Reset - set the controls back to their default state.
    ''' </summary>
    Private Sub Reset()


        txtModel.Text = String.Empty
        txtPrice.Text = String.Empty
        cmbMake.SelectedIndex = -1
        cmbYear.SelectedIndex = -1
        lbResult.Text = String.Empty
        chkNew.Checked = False

        currentCarID = String.Empty

    End Sub

    ''' <summary>
    ''' IsValidInput - validates the data in each control to ensure that the user has entered appropriate values
    ''' </summary>
    ''' <returns>Boolean</returns>
    Private Function IsValidInput() As Boolean

        Dim returnValue As Boolean = True
        Dim outputMessage As String = String.Empty

        If cmbMake.SelectedIndex = -1 Then

            outputMessage += "Please select the customer's title." & vbCrLf
            returnValue = False

        End If

        If cmbYear.SelectedIndex = -1 Then

            outputMessage += "Please select the car's year." & vbCrLf
            returnValue = False

        End If

        Dim price As Double

        If txtModel.Text.Trim.Length = 0 Then

            outputMessage += "Please enter the car's model." & vbCrLf

            returnValue = False
        End If

        If txtPrice.Text.Trim.Length = 0 Then

            outputMessage += "Please enter the car's price in real number." & vbCrLf

            returnValue = False

        ElseIf Not Double.TryParse(txtPrice.Text.Trim, price) Then

            outputMessage += "Please enter the car's price in real number." & vbCrLf

            returnValue = False

        ElseIf price < 0 Then

            outputMessage += "Car's price must be greater than 0." & vbCrLf

            returnValue = False

        End If

        If returnValue = False Then

            lbResult.Text = "ERRORS" & vbCrLf & outputMessage

        End If

        Return returnValue

    End Function


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub lvwCars_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvwCars.ItemCheck

        If editMode = False Then

            e.NewValue = e.CurrentValue

        End If

    End Sub


    ''' <summary>
    ''' lvwCustomers_SelectedIndexChanged - when the user selected a row in the list it will populate the fields for editing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lvwCars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwCars.SelectedIndexChanged

        Const identificationSubItemIndex As Integer = 1

        currentCarID = lvwCars.Items(lvwCars.FocusedItem.Index).SubItems(identificationSubItemIndex).Text

        Dim car As Car = CType(carList.Item(currentCarID), Car)

        txtModel.Text = car.CarModel
        txtPrice.Text = car.CarPrice
        cmbMake.Text = car.CarMake
        cmbYear.Text = car.CarYear
        chkNew.Checked = car.IsNewCar

        lbResult.Text = car.GetCarData()


    End Sub

End Class
