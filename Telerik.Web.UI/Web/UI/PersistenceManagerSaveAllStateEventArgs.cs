using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020002E3 RID: 739
	public class PersistenceManagerSaveAllStateEventArgs : EventArgs
	{
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x000547D2 File Offset: 0x000529D2
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x000547DA File Offset: 0x000529DA
		public List<RadControlState> Settings { get; set; }
	}
}
