using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000642 RID: 1602
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SiteMapNodeItemEventArgs : EventArgs
	{
		// Token: 0x06004EFA RID: 20218 RVA: 0x0013E9F2 File Offset: 0x0013D9F2
		public SiteMapNodeItemEventArgs(SiteMapNodeItem item)
		{
			this._item = item;
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x0013EA01 File Offset: 0x0013DA01
		public SiteMapNodeItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x04002CBC RID: 11452
		private SiteMapNodeItem _item;
	}
}
