using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000EE2 RID: 3810
	public class SearchBoxButtonEventArgs : EventArgs
	{
		// Token: 0x060090AF RID: 37039 RVA: 0x00209B69 File Offset: 0x00207D69
		public SearchBoxButtonEventArgs(string commandName, string commandArgument)
		{
			this.CommandName = commandName;
			this.CommandArgument = commandArgument;
		}

		// Token: 0x17002DD0 RID: 11728
		// (get) Token: 0x060090B0 RID: 37040 RVA: 0x00209B7F File Offset: 0x00207D7F
		// (set) Token: 0x060090B1 RID: 37041 RVA: 0x00209B87 File Offset: 0x00207D87
		public string CommandName { get; set; }

		// Token: 0x17002DD1 RID: 11729
		// (get) Token: 0x060090B2 RID: 37042 RVA: 0x00209B90 File Offset: 0x00207D90
		// (set) Token: 0x060090B3 RID: 37043 RVA: 0x00209B98 File Offset: 0x00207D98
		public string CommandArgument { get; set; }
	}
}
