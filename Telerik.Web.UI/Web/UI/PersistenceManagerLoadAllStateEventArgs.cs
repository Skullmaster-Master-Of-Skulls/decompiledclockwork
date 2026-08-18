using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020002E2 RID: 738
	public class PersistenceManagerLoadAllStateEventArgs : EventArgs
	{
		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x000547B9 File Offset: 0x000529B9
		// (set) Token: 0x060019A0 RID: 6560 RVA: 0x000547C1 File Offset: 0x000529C1
		public List<RadControlState> Settings { get; set; }
	}
}
