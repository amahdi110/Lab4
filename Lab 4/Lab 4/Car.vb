Option Strict On

''' <summary>
''' Author: Ali Hassan Mahdi
''' Date: July 12, 2019
''' Description: This class holds the properties and methods of Car
''' </summary>
Public Class Car

    Private make As String = String.Empty
    Private model As String = String.Empty
    Private year As String = String.Empty
    Private price As String = String.Empty
    Private isNew As Boolean = False
    Private Shared carCount As Integer
    Private carID As Integer = 0

    ''' <summary>
    ''' Constructor - Default - creates a new car object
    ''' </summary>
    Public Sub New()

        carCount += 1
        carID = carCount

    End Sub

    ''' <summary>
    ''' Constructor - Parameterized - creates a new car object
    ''' </summary>

    Public Sub New(carMake As String, carModel As String, carYear As String, carPrice As String, isCarNew As Boolean)

        Me.New()


        make = carMake
        model = carModel
        price = carPrice
        year = carYear
        isNew = isCarNew

    End Sub

    Public Property CarMake() As String
        Get
            Return make
        End Get
        Set(ByVal value As String)
            make = value
        End Set
    End Property

    Public Property CarModel() As String
        Get
            Return model
        End Get
        Set(ByVal value As String)
            model = value
        End Set
    End Property

    Public Property CarPrice() As String
        Get
            Return price
        End Get
        Set(ByVal value As String)
            price = value
        End Set
    End Property

    Public Property CarYear() As String
        Get
            Return year
        End Get
        Set(ByVal value As String)
            year = value
        End Set
    End Property

    Public ReadOnly Property IdentificationNumber() As Integer
        Get
            Return carID
        End Get
    End Property

    Public Property IsNewCar() As Boolean
        Get
            Return isNew
        End Get
        Set(ByVal value As Boolean)
            isNew = value
        End Set
    End Property

    Public Function GetCarData() As String

        Return "Details:" + vbCrLf +
        "Car: " + " " + make + " " + model + " " + year + vbCrLf +
        "Price: $" + price + vbCrLf +
        "Condition: " + IIf(isNew = True, "New", "Used").ToString()
    End Function

End Class
