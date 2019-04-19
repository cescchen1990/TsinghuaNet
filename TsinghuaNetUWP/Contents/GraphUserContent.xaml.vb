﻿Imports Berrysoft.Tsinghua.Net

Public NotInheritable Class GraphUserContent
    Inherits UserControl
    Implements IUserContent

    Public Shared ReadOnly UserProperty As DependencyProperty = DependencyProperty.Register(NameOf(User), GetType(FluxUserBox), GetType(GraphUserContent), New PropertyMetadata(Nothing, AddressOf UserPropertyChanged))
    Public Property User As FluxUserBox Implements IUserContent.User
        Get
            Return GetValue(UserProperty)
        End Get
        Set(value As FluxUserBox)
            SetValue(UserProperty, value)
        End Set
    End Property
    Private Shared Sub UserPropertyChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim content As GraphUserContent = d
        Dim flux As FluxUserBox = e.NewValue
        If flux IsNot Nothing Then
            content.OnlineTime = flux.OnlineTime
            'Dim maxf = UserHelper.GetMaxFlux(flux.Flux, flux.Balance)
            'content.FluxAnimation.To = flux.Flux / maxf
            'content.FreeAnimation.To = UserHelper.BaseFlux / maxf
        End If
    End Sub

    Public Shared ReadOnly OnlineTimeProperty As DependencyProperty = DependencyProperty.Register(NameOf(OnlineTime), GetType(TimeSpan), GetType(GraphUserContent), New PropertyMetadata(TimeSpan.Zero))
    Public Property OnlineTime As TimeSpan Implements IUserContent.OnlineTime
        Get
            Return GetValue(OnlineTimeProperty)
        End Get
        Set(value As TimeSpan)
            SetValue(OnlineTimeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly FreeOffsetProperty As DependencyProperty = DependencyProperty.Register(NameOf(FreeOffset), GetType(Double), GetType(GraphUserContent), New PropertyMetadata(0.0))
    Public Property FreeOffset As Double Implements IUserContent.FreeOffset
        Get
            Return GetValue(FreeOffsetProperty)
        End Get
        Set(value As Double)
            SetValue(FreeOffsetProperty, value)
        End Set
    End Property

    Public Shared ReadOnly FluxOffsetProperty As DependencyProperty = DependencyProperty.Register(NameOf(FluxOffset), GetType(Double), GetType(GraphUserContent), New PropertyMetadata(0.0))
    Public Property FluxOffset As Double Implements IUserContent.FluxOffset
        Get
            Return GetValue(FluxOffsetProperty)
        End Get
        Set(value As Double)
            SetValue(FluxOffsetProperty, value)
        End Set
    End Property

    Public Property IsProgressActive As Boolean Implements IUserContent.IsProgressActive
        Get
            Return Progress.IsActive
        End Get
        Set(value As Boolean)
            Progress.IsActive = value
        End Set
    End Property

    Public Sub BeginAnimation() Implements IUserContent.BeginAnimation
        'FluxStoryboard.Begin()
    End Sub

    Public Function AddOneSecond() As Boolean Implements IUserContent.AddOneSecond
        If User Is Nothing OrElse String.IsNullOrEmpty(User.Username) Then
            Return False
        Else
            OnlineTime += TimeSpan.FromSeconds(1)
            Return True
        End If
    End Function

    Public ReadOnly Property Details As New ObservableCollection(Of NetDetailBox)

    Friend Async Function RefreshDetails(username As String, password As String) As Task
        If Not String.IsNullOrEmpty(username) Then
            Dim helper As New UseregHelper(username, password)
            Await helper.LoginAsync()
            Dim ds = Await helper.GetDetailsAsync()
            Details.Clear()
            For Each b In From d In ds
                          Group By d.OnlineDate.Day Into gr = Group
                          Aggregate g In gr Into s = Sum(g.Flux)
                          Select New NetDetailBox() With {.[Date] = Day, .Flux = s / 1000000000}
                Details.Add(b)
            Next
        End If
    End Function
End Class