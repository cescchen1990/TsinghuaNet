﻿Imports TsinghuaNet.Helper

Public Class SettingsViewModel
    Inherits NetObservableBase

    Public ReadOnly Property Packages As IReadOnlyList(Of PackageBox) = New List(Of PackageBox) From
    {
        New PackageBox("Eto.Forms", "BSD-3许可证"),
        New PackageBox("HtmlAgilityPack", "MIT许可证"),
        New PackageBox("Newtonsoft.Json", "MIT许可证"),
        New PackageBox("Refractored.MvvmHelpers", "MIT许可证")
    }
End Class