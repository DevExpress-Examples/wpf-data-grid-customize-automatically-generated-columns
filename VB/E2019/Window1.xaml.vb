Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Documents
Imports DevExpress.Xpf.Grid
Imports System.Windows.Data
Imports System.Windows.Media

Namespace E2019
    Partial Public Class Window1
        Inherits Window

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = IssueList.GetData()
        End Sub
        Private Sub OnColumnsGenerated(ByVal sender As Object, ByVal e As RoutedEventArgs)
            For Each column As GridColumn In grid.Columns
                Select Case column.FieldName
                    Case "IssueName"
                        column.CellTemplate = TryCast(Application.Current.MainWindow.Resources("IssueNameTemplate"), DataTemplate)
                        column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                    Case "IssueType"
                        column.CellTemplate = TryCast(Application.Current.MainWindow.Resources("IssueTypeTemplate"), DataTemplate)
                    Case "ID"
                        column.Visible = False
                End Select
            Next column
        End Sub
        Public Class IssueList
            Public Shared Function GetData() As List(Of IssueDataObject)
                Dim data As New List(Of IssueDataObject)()
                data.Add(New IssueDataObject() With {.ID = 0, .IssueName = "Transaction History", .IssueType = "Bug", .IsPrivate = True})
                data.Add(New IssueDataObject() With {.ID = 1, .IssueName = "Ledger: Inconsistency", .IssueType = "Bug", .IsPrivate = False})
                data.Add(New IssueDataObject() With {.ID = 2, .IssueName = "Data Import", .IssueType = "Request", .IsPrivate = False})
                data.Add(New IssueDataObject() With {.ID = 3, .IssueName = "Data Archiving", .IssueType = "Request", .IsPrivate = True})
                Return data
            End Function
        End Class
        Public Class IssueDataObject
            Public Property ID() As Integer
            Public Property IssueName() As String
            Public Property IssueType() As String
            Public Property IsPrivate() As Boolean
        End Class
    End Class

    Public Class IssueTypeForegroundConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then
                Return Nothing
            End If

            Dim issueType As String = value.ToString()
            If issueType = "Bug" Then
                Return New SolidColorBrush(Colors.Red)
            End If

            Return New SolidColorBrush(Colors.Black)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class

End Namespace
