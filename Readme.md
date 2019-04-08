<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/Q515371/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/Q515371/Controllers/HomeController.vb))
* [TreeListVirtualModeHelper.cs](./CS/Q515371/Models/TreeListVirtualModeHelper.cs) (VB: [TreeListVirtualModeHelper.vb](./VB/Q515371/Models/TreeListVirtualModeHelper.vb))
* [_TreeListPartial.cshtml](./CS/Q515371/Views/Home/_TreeListPartial.cshtml)
* [Index.cshtml](./CS/Q515371/Views/Home/Index.cshtml)
<!-- default file list end -->
# TreeList - How to load data from a database in virtual mode and implement drag-and-drop


<p>Virtual mode is useful if there is a lot of data and it's not necessary or impossible to create a tree at once. To implement virtual mode for the TreeList extension, a specifically parameterized BindToVirtualData method can be used. The method's parameters refer to delegate methods that can be declared as static within a model class. For this, a special TreeListVirtualModeHelper class with the required methods was created. To implement the drag-and-drop functionality, it's necessary to define the SettingsEditing.NodeDragDropRouteValues property. </p><p>Note that node dragging is an editing operation, because while changing the node's position, its parent row identificator changes. The MoveNode method was created to accept changes on the controller.</p>

<br/>


