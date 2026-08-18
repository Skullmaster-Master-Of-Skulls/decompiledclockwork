using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000DB RID: 219
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class LiteralDesigner : ControlDesigner
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x00027AC4 File Offset: 0x00025CC4
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			base.OnComponentChanged(sender, new ComponentChangedEventArgs(ce.Component, null, null, null));
		}
	}
}
