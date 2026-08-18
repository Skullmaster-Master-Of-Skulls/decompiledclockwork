using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000558 RID: 1368
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewCommandEventArgs : CommandEventArgs
	{
		// Token: 0x060043F5 RID: 17397 RVA: 0x00119741 File Offset: 0x00118741
		public DetailsViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._commandSource = commandSource;
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x00119751 File Offset: 0x00118751
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		// Token: 0x0400298C RID: 10636
		private object _commandSource;
	}
}
