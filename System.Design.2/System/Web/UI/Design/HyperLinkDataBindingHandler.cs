using System;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000094 RID: 148
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class HyperLinkDataBindingHandler : DataBindingHandler
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00014080 File Offset: 0x00012280
		public override void DataBindControl(IDesignerHost designerHost, Control control)
		{
			DataBindingCollection dataBindings = ((IDataBindingsAccessor)control).DataBindings;
			DataBinding dataBinding = dataBindings["Text"];
			DataBinding dataBinding2 = dataBindings["NavigateUrl"];
			if (dataBinding != null || dataBinding2 != null)
			{
				HyperLink hyperLink = (HyperLink)control;
				if (dataBinding != null)
				{
					hyperLink.Text = SR.GetString("Sample_Databound_Text");
				}
				if (dataBinding2 != null)
				{
					hyperLink.NavigateUrl = "url";
				}
			}
		}
	}
}
