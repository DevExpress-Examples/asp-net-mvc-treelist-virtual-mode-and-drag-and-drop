Imports Microsoft.VisualBasic
Imports DevExpress.Web.ASPxTreeList
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web

Namespace Q515371.Models
	Public Class TreeListVirtualModeHelper
		' Fields...


	   Private Shared nwd As New NorthwindDataContext()
		Public Shared Sub VirtualModeCreateChildren(ByVal e As TreeListVirtualModeCreateChildrenEventArgs)
			Dim parentEmployee As Employee = TryCast(e.NodeObject, Employee)
			If parentEmployee Is Nothing Then
				e.Children = nwd.Employees.Where(Function(empl) empl.ReportsTo Is Nothing).ToList()
			Else
				e.Children = ( _
						From empl In nwd.Employees _
						Where empl.ReportsTo.Equals(parentEmployee.EmployeeID) _
						Select empl).ToList()
			End If
		End Sub
		Public Shared Sub VirtualModeNodeCreating(ByVal e As TreeListVirtualModeNodeCreatingEventArgs)
			Dim empl As Employee = TryCast(e.NodeObject, Employee)

			If empl Is Nothing Then
				Return
			End If
			e.NodeKeyValue = empl.EmployeeID
			Dim childEmployeeCount As Integer = nwd.Employees.Where(Function(x) x.ReportsTo.Equals(empl.EmployeeID)).ToList().Count
			e.IsLeaf = Not(childEmployeeCount > 0)
			e.SetNodeValue("FirstName", empl.FirstName)
			e.SetNodeValue("LastName", empl.LastName)
			e.SetNodeValue("Address", empl.Address)
			e.SetNodeValue("City", empl.City)
			e.SetNodeValue("HireDate", empl.HireDate)
			e.SetNodeValue("BirthDate", empl.BirthDate)
			e.SetNodeValue("ReportsTo", empl.ReportsTo)
			e.SetNodeValue("Title", empl.Title)
		End Sub
		Public Shared Sub MoveNode(ByVal EmployeeID As Integer, ByVal ReportsTo? As Integer)
			Dim newParentID As Integer = Convert.ToInt32(ReportsTo)
			Dim empl As Employee = GetEmployee(EmployeeID)
			If empl.EmployeeID = newParentID Then
				Return
			End If
			If newParentID = 0 Then
				empl.ReportsTo = Nothing
			Else
				empl.ReportsTo = newParentID
			End If
			nwd.SubmitChanges()

		End Sub
		Private Shared Function GetEmployee(ByVal EmployeeID As Integer) As Employee
			Return nwd.Employees.Where(Function(x) x.EmployeeID = EmployeeID).FirstOrDefault()
		End Function
	End Class
End Namespace