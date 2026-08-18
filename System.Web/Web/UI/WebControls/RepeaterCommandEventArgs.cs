using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200062C RID: 1580
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RepeaterCommandEventArgs : CommandEventArgs
	{
		// Token: 0x06004E5C RID: 20060 RVA: 0x0013D3FF File Offset: 0x0013C3FF
		public RepeaterCommandEventArgs(RepeaterItem item, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06004E5D RID: 20061 RVA: 0x0013D416 File Offset: 0x0013C416
		public RepeaterItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06004E5E RID: 20062 RVA: 0x0013D41E File Offset: 0x0013C41E
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x04002C93 RID: 11411
		private RepeaterItem item;

		// Token: 0x04002C94 RID: 11412
		private object commandSource;
	}
}
