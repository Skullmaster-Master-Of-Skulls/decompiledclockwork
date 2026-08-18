using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000151 RID: 337
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class WebPartDesigner : PartDesigner
	{
		// Token: 0x06000BDB RID: 3035 RVA: 0x0004B4D5 File Offset: 0x000496D5
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(WebPart));
			base.Initialize(component);
		}
	}
}
