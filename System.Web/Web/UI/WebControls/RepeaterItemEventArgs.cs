using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000630 RID: 1584
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RepeaterItemEventArgs : EventArgs
	{
		// Token: 0x06004E73 RID: 20083 RVA: 0x0013D515 File Offset: 0x0013C515
		public RepeaterItemEventArgs(RepeaterItem item)
		{
			this.item = item;
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06004E74 RID: 20084 RVA: 0x0013D524 File Offset: 0x0013C524
		public RepeaterItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04002C99 RID: 11417
		private RepeaterItem item;
	}
}
