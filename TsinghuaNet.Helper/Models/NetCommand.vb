﻿Imports System.ComponentModel
Imports System.Windows.Input

Public Class NetCommand
    Implements ICommand

    Private ReadOnly model As NetObservableBase
    Private executor As Func(Of IConnect, Task(Of LogResponse))

    Public Sub New(model As NetObservableBase, executor As Func(Of IConnect, Task(Of LogResponse)))
        Me.model = model
        Me.executor = executor
        AddHandler model.PropertyChanged, AddressOf OnModelPropertyChanged
        AddHandler model.Credential.PropertyChanged, AddressOf OnModelPropertyChanged
    End Sub

    Private Sub OnModelPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If e.PropertyName = "State" OrElse e.PropertyName = "IsBusy" Then
            OnCanExecuteChanged()
        End If
    End Sub

    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
    Protected Overridable Sub OnCanExecuteChanged()
        RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return Not model.IsBusy AndAlso model.Credential.State <> NetState.Unknown
    End Function

    Public Async Sub Execute(parameter As Object) Implements ICommand.Execute
        model.IsBusy = True
        Try
            Dim helper = model.Credential.GetHelper()
            Await executor(helper)
        Finally
            model.IsBusy = False
        End Try
    End Sub
End Class
