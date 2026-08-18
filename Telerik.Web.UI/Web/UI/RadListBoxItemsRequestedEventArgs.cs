using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000574 RID: 1396
	public class RadListBoxItemsRequestedEventArgs : EventArgs
	{
		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x000A61D4 File Offset: 0x000A43D4
		// (set) Token: 0x06003298 RID: 12952 RVA: 0x000A61DC File Offset: 0x000A43DC
		public int StartIndex { get; set; }

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x000A61E5 File Offset: 0x000A43E5
		// (set) Token: 0x0600329A RID: 12954 RVA: 0x000A61ED File Offset: 0x000A43ED
		public int Count { get; set; }

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000A61F6 File Offset: 0x000A43F6
		// (set) Token: 0x0600329C RID: 12956 RVA: 0x000A61FE File Offset: 0x000A43FE
		public IDictionary<string, object> Context { get; set; }
	}
}
