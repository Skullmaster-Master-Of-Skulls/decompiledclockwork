using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000541 RID: 1345
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataGridPageChangedEventArgs : EventArgs
	{
		// Token: 0x06004234 RID: 16948 RVA: 0x001122D5 File Offset: 0x001112D5
		public DataGridPageChangedEventArgs(object commandSource, int newPageIndex)
		{
			this.commandSource = commandSource;
			this.newPageIndex = newPageIndex;
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x001122EB File Offset: 0x001112EB
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x001122F3 File Offset: 0x001112F3
		public int NewPageIndex
		{
			get
			{
				return this.newPageIndex;
			}
		}

		// Token: 0x040028F6 RID: 10486
		private object commandSource;

		// Token: 0x040028F7 RID: 10487
		private int newPageIndex;
	}
}
