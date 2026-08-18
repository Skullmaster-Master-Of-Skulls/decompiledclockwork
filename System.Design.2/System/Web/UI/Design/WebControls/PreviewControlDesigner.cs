using System;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000FA RID: 250
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class PreviewControlDesigner : ControlDesigner
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}
	}
}
