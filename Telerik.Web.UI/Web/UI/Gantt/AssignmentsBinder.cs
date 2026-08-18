using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002EA RID: 746
	[ToolboxItem(false)]
	public class AssignmentsBinder : DataBoundControl
	{
		// Token: 0x060019CE RID: 6606 RVA: 0x00054BBC File Offset: 0x00052DBC
		public new DataSourceView GetData()
		{
			return base.GetData();
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00054BC4 File Offset: 0x00052DC4
		public static IAssignment BindAssignments(object dataItem, IAssignmentsDataBindings bindings)
		{
			return new Assignment
			{
				ID = DataBinder.Eval(dataItem, bindings.IdField),
				TaskID = DataBinder.Eval(dataItem, bindings.TaskIdField),
				ResourceID = DataBinder.Eval(dataItem, bindings.ResourceIdField),
				Units = DataBinder.Eval(dataItem, bindings.UnitsField)
			};
		}
	}
}
