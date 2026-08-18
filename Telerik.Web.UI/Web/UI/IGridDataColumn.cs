using System;

namespace Telerik.Web.UI
{
	// Token: 0x020004BE RID: 1214
	public interface IGridDataColumn
	{
		// Token: 0x06002BE2 RID: 11234
		string GetActiveDataField();

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06002BE3 RID: 11235
		// (set) Token: 0x06002BE4 RID: 11236
		bool AllowFiltering { get; set; }

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06002BE5 RID: 11237
		// (set) Token: 0x06002BE6 RID: 11238
		bool AllowSorting { get; set; }
	}
}
