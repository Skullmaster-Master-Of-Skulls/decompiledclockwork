using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x0200014F RID: 335
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ProxyWebPartManagerDesigner : ControlDesigner
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
		public override string GetDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml();
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0004B41A File Offset: 0x0004961A
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(ProxyWebPartManager));
			base.Initialize(component);
		}
	}
}
