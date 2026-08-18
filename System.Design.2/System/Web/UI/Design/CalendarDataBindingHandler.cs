using System;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000093 RID: 147
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class CalendarDataBindingHandler : DataBindingHandler
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x0001404C File Offset: 0x0001224C
		public override void DataBindControl(IDesignerHost designerHost, Control control)
		{
			Calendar calendar = (Calendar)control;
			DataBinding dataBinding = ((IDataBindingsAccessor)calendar).DataBindings["SelectedDate"];
			if (dataBinding != null)
			{
				calendar.SelectedDate = DateTime.Today;
			}
		}
	}
}
