using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001931 RID: 6449
	public class RadListBoxInsertingEventArgs : RadListBoxEventArgs
	{
		// Token: 0x0600F99A RID: 63898 RVA: 0x003850F2 File Offset: 0x003832F2
		public RadListBoxInsertingEventArgs(IList<RadListBoxItem> items) : base(items)
		{
		}

		// Token: 0x17004B61 RID: 19297
		// (get) Token: 0x0600F99B RID: 63899 RVA: 0x003850FB File Offset: 0x003832FB
		// (set) Token: 0x0600F99C RID: 63900 RVA: 0x00385103 File Offset: 0x00383303
		public bool Cancel { get; set; }
	}
}
