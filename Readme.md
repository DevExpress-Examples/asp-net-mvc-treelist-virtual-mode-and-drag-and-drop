<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4837)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Tree List for ASP.NET MVC - How to load data from a database in virtual mode and implement drag-and-drop functionality

Use the [BindToVirtualData](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.TreeListExtension.BindToVirtualData(DevExpress.Web.Mvc.TreeListVirtualModeCreateChildrenMethod-DevExpress.Web.Mvc.TreeListVirtualModeNodeCreatingMethod)) method to implement virtual mode for the [TreeList](https://docs.devexpress.com/AspNetMvc/13765/components/tree-list) extension. The method requires the following two delegate methods as parameters:

* `createChildrenMethod` - Handle this method to create a list of child nodes owned by the processed node. Assign the list to the event parameter’s `Children` property. The `NodeObject` property returns the node currently being processed. If the event is raised for the root node, the `NodeObject` property returns `null` (Nothing for VB).
  ```cs
  public static void VirtualModeCreateChildren(TreeListVirtualModeCreateChildrenEventArgs e) {           
    Employee parentEmployee = e.NodeObject as Employee;
    if (parentEmployee == null) {
        e.Children = nwd.Employees.Where(empl=>empl.ReportsTo==null).ToList();
    }
    else {
        e.Children = (from empl in nwd.Employees where empl.ReportsTo == parentEmployee.EmployeeID select empl).ToList();
    }          
  }
  ```
* `nodeCreatingMethod` - Handle this method to initialize a node in a tree. You should specify a node key value (the `NodeKeyValue` property) and cell values. If the processed node has no child nodes, set the `IsLeaf` property to `true`.
  ```cs
  public static void VirtualModeNodeCreating(TreeListVirtualModeNodeCreatingEventArgs e) {
      Employee empl = e.NodeObject as Employee;
      if (empl == null)
          return;
      e.NodeKeyValue = empl.EmployeeID;
      int childEmployeeCount = nwd.Employees.Where(x => x.ReportsTo == empl.EmployeeID).ToList().Count;
      e.IsLeaf = !(childEmployeeCount > 0);           
      e.SetNodeValue("FirstName", empl.FirstName);
      e.SetNodeValue("LastName", empl.LastName);
      e.SetNodeValue("Address", empl.Address);
      e.SetNodeValue("City", empl.City);
      e.SetNodeValue("HireDate", empl.HireDate);
      e.SetNodeValue("BirthDate", empl.BirthDate);
      e.SetNodeValue("ReportsTo", empl.ReportsTo);
      e.SetNodeValue("Title", empl.Title);
  }
  ```

These methods are implemented in the `TreeListVirtualModeHelper` class.

To implement the drag-and-drop functionality, define the [SettingsEditing.NodeDragDropRouteValues](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.MVCxTreeListSettingsEditing.NodeDragDropRouteValues) property.

```cs
settings.SettingsEditing.NodeDragDropRouteValues = new { Controller = "Home", Action = "MoveNodePartial" };
```

Moving a row is an edit operation, because the node's parent node is changed. In this example, the `MoveNode` method accepts changes in the controller.</p>

```cs
public static void MoveNode(int EmployeeID, int? ReportsTo) {
    int newParentID = Convert.ToInt32(ReportsTo);
    Employee empl = GetEmployee(EmployeeID);
    if (empl.EmployeeID == newParentID)
        return;
    if (newParentID == 0)
        empl.ReportsTo = null;
    else
        empl.ReportsTo = newParentID;
    nwd.SubmitChanges();
}
```

## Files to Review

* [HomeController.cs](./CS/Q515371/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/Q515371/Controllers/HomeController.vb))
* [TreeListVirtualModeHelper.cs](./CS/Q515371/Models/TreeListVirtualModeHelper.cs) (VB: [TreeListVirtualModeHelper.vb](./VB/Q515371/Models/TreeListVirtualModeHelper.vb))
* [_TreeListPartial.cshtml](./CS/Q515371/Views/Home/_TreeListPartial.cshtml)
