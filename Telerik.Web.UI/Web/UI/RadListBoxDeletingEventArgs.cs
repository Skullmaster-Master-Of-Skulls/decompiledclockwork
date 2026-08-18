using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200192E RID: 6446
	public class RadListBoxDeletingEventArgs : RadListBoxEventArgs
	{
		// Token: 0x0600F98F RID: 63887 RVA: 0x003850D8 File Offset: 0x003832D8
		public RadListBoxDeletingEventArgs(IList<RadListBoxItem> items) : base(items)
		{
		}

		// Token: 0x17004B60 RID: 19296
		// (get) Token: 0x0600F990 RID: 63888 RVA: 0x003850E1 File Offset: 0x003832E1
		// (set) Token: 0x0600F991 RID: 63889 RVA: 0x003850E9 File Offset: 0x003832E9
		public bool Cancel { get; set; }
	}
}
