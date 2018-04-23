Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc
Imports Q515371.Models

Namespace Q515371.Controllers
	Public Class HomeController
		Inherits Controller
		'
		' GET: /Home/

		Public Function Index() As ActionResult
			Return View()
		End Function


		<ValidateInput(False)> _
		Public Function TreeListPartial() As ActionResult

			Return PartialView("_TreeListPartial")
		End Function
		Public Function MoveNodePartial(ByVal EmployeeID As Integer, ByVal ReportsTo? As Integer) As ActionResult

			TreeListVirtualModeHelper.MoveNode(EmployeeID, ReportsTo)
			Return PartialView("_TreeListPartial")
		End Function


	End Class
End Namespace
