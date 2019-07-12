Option Strict On

Public Class frmCarInventory

    Private carList As New SortedList
    Private currentCarID As String = String.Empty
    Private editMode As Boolean = False

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
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

                ' update the data in the specific object
                ' from the controls
                car.CarMake = cmbMake.Text
                car.CarModel = txtModel.Text
                car.CarYear = cmbYear.Text
                car.IsNewCar = chkNew.Checked
                car.CarPrice = carPrice
            End If

            lvwCars.Items.Clear()

            For Each customerEntry As DictionaryEntry In carList

                ' instantiate a new ListViewItem
                carItem = New ListViewItem()

                ' get the customer from the list
                car = CType(customerEntry.Value, Car)

                ' assign the values to the ckecked control
                ' and the subitems
                carItem.Checked = car.IsNewCar
                carItem.SubItems.Add(car.IdentificationNumber.ToString())
                carItem.SubItems.Add(car.CarMake)
                carItem.SubItems.Add(car.CarModel)
                carItem.SubItems.Add(car.CarYear)
                carItem.SubItems.Add("$" + carPrice)

                ' add the new instantiated and populated ListViewItem
                ' to the listview control
                lvwCars.Items.Add(carItem)

            Next customerEntry

            Reset()

            ' set the edit flag to false
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

            ' If not set the error message
            outputMessage += "Please select the customer's title." & vbCrLf

            ' And, set the return value to false
            returnValue = False

        End If

        If cmbYear.SelectedIndex = -1 Then

            ' If not set the error message
            outputMessage += "Please select the car's year." & vbCrLf

            ' And, set the return value to false
            returnValue = False

        End If

        Dim price As Double
        ' check if the first name has been entered
        If txtModel.Text.Trim.Length = 0 Then

            ' If not set the error message
            outputMessage += "Please enter the car's model." & vbCrLf

            ' And, set the return value to false
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





        ' check to see if any value
        ' did not validate
        If returnValue = False Then

            ' show the message(s) to the user
            lbResult.Text = "ERRORS" & vbCrLf & outputMessage

        End If

        ' return the boolean value
        ' true if it passed validation
        ' false if it did not pass validation
        Return returnValue

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub lvwCars_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvwCars.ItemCheck

        ' if it is not in edit mode
        If editMode = False Then

            ' the new value to the current value
            ' so it cannot be set in the listview by the user
            e.NewValue = e.CurrentValue

        End If

    End Sub


    ''' <summary>
    ''' lvwCustomers_SelectedIndexChanged - when the user selected a row in the list it will populate the fields for editing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lvwCars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwCars.SelectedIndexChanged

        ' constant that represents the index of the subitem in the list that
        ' holds the customer identification number 
        Const identificationSubItemIndex As Integer = 1

        ' Get the customer identification number 
        currentCarID = lvwCars.Items(lvwCars.FocusedItem.Index).SubItems(identificationSubItemIndex).Text

        ' Use the customer identification number to get the customer from the collection object
        Dim car As Car = CType(carList.Item(currentCarID), Car)

        ' set the controls on the form
        txtModel.Text = car.CarModel
        txtPrice.Text = car.CarPrice
        cmbMake.Text = car.CarMake
        cmbYear.Text = car.CarYear
        chkNew.Checked = car.IsNewCar

        lbResult.Text = car.GetCarData()


    End Sub

End Class
