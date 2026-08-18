using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001929 RID: 6441
	public class RadListBoxDroppingEventArgs : RadListBoxDropEventArgs
	{
		// Token: 0x0600F97A RID: 63866 RVA: 0x00385062 File Offset: 0x00383262
		public RadListBoxDroppingEventArgs(string htmlElementId, IList<RadListBoxItem> sourceDragItems) : base(htmlElementId, sourceDragItems)
		{
		}

		// Token: 0x17004B5B RID: 19291
		// (get) Token: 0x0600F97B RID: 63867 RVA: 0x0038506C File Offset: 0x0038326C
		// (set) Token: 0x0600F97C RID: 63868 RVA: 0x00385074 File Offset: 0x00383274
		public bool Cancel { get; set; }
	}
}
