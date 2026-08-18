using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200192D RID: 6445
	public class RadListBoxEventArgs : EventArgs
	{
		// Token: 0x0600F98C RID: 63884 RVA: 0x003850B8 File Offset: 0x003832B8
		public RadListBoxEventArgs(IList<RadListBoxItem> items)
		{
			this.Items = items;
		}

		// Token: 0x17004B5F RID: 19295
		// (get) Token: 0x0600F98D RID: 63885 RVA: 0x003850C7 File Offset: 0x003832C7
		// (set) Token: 0x0600F98E RID: 63886 RVA: 0x003850CF File Offset: 0x003832CF
		public IList<RadListBoxItem> Items { get; private set; }
	}
}
