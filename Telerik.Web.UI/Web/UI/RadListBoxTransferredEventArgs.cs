using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200192B RID: 6443
	public class RadListBoxTransferredEventArgs : EventArgs
	{
		// Token: 0x17004B5C RID: 19292
		// (get) Token: 0x0600F981 RID: 63873 RVA: 0x0038507D File Offset: 0x0038327D
		// (set) Token: 0x0600F982 RID: 63874 RVA: 0x00385085 File Offset: 0x00383285
		public RadListBox SourceListBox { get; set; }

		// Token: 0x17004B5D RID: 19293
		// (get) Token: 0x0600F983 RID: 63875 RVA: 0x0038508E File Offset: 0x0038328E
		// (set) Token: 0x0600F984 RID: 63876 RVA: 0x00385096 File Offset: 0x00383296
		public RadListBox DestinationListBox { get; set; }

		// Token: 0x17004B5E RID: 19294
		// (get) Token: 0x0600F985 RID: 63877 RVA: 0x0038509F File Offset: 0x0038329F
		// (set) Token: 0x0600F986 RID: 63878 RVA: 0x003850A7 File Offset: 0x003832A7
		public IList<RadListBoxItem> Items { get; set; }
	}
}
