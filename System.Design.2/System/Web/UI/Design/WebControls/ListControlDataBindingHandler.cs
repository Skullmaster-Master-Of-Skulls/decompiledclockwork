using System;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000D8 RID: 216
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ListControlDataBindingHandler : DataBindingHandler
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00028860 File Offset: 0x00026A60
		public override void DataBindControl(IDesignerHost designerHost, Control control)
		{
			DataBinding dataBinding = ((IDataBindingsAccessor)control).DataBindings["DataSource"];
			if (dataBinding != null)
			{
				ListControl listControl = (ListControl)control;
				listControl.Items.Clear();
				listControl.Items.Add(SR.GetString("Sample_Databound_Text"));
			}
		}
	}
}
