using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Q515371.Models
{
    public class TreeListVirtualModeHelper
    {
        // Fields...
        
            
       static NorthwindDataContext nwd = new NorthwindDataContext();
        public static void VirtualModeCreateChildren(TreeListVirtualModeCreateChildrenEventArgs e) {           
            Employee parentEmployee = e.NodeObject as Employee;
            if (parentEmployee == null)
            {
                e.Children = nwd.Employees.Where(empl=>empl.ReportsTo==null).ToList();
            }
            else {
                e.Children = (from empl in nwd.Employees where empl.ReportsTo == parentEmployee.EmployeeID select empl).ToList();
            }          
        }
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
        public static void MoveNode(int EmployeeID, int? ReportsTo)
        {
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
        private static Employee GetEmployee(int EmployeeID)
        {
            return nwd.Employees.Where(x => x.EmployeeID == EmployeeID).FirstOrDefault();
        }     
    }
}