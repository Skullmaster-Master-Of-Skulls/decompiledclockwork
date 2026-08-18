using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000D4 RID: 212
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class LabelDesigner : TextControlDesigner
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x00027AC4 File Offset: 0x00025CC4
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			base.OnComponentChanged(sender, new ComponentChangedEventArgs(ce.Component, null, null, null));
		}
	}
}
