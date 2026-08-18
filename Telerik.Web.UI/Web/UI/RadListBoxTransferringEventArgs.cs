using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001937 RID: 6455
	public class RadListBoxTransferringEventArgs : EventArgs
	{
		// Token: 0x17004B67 RID: 19303
		// (get) Token: 0x0600F9B5 RID: 63925 RVA: 0x00385178 File Offset: 0x00383378
		// (set) Token: 0x0600F9B6 RID: 63926 RVA: 0x00385180 File Offset: 0x00383380
		public RadListBox SourceListBox { get; set; }

		// Token: 0x17004B68 RID: 19304
		// (get) Token: 0x0600F9B7 RID: 63927 RVA: 0x00385189 File Offset: 0x00383389
		// (set) Token: 0x0600F9B8 RID: 63928 RVA: 0x00385191 File Offset: 0x00383391
		public RadListBox DestinationListBox { get; set; }

		// Token: 0x17004B69 RID: 19305
		// (get) Token: 0x0600F9B9 RID: 63929 RVA: 0x0038519A File Offset: 0x0038339A
		// (set) Token: 0x0600F9BA RID: 63930 RVA: 0x003851A2 File Offset: 0x003833A2
		public bool Cancel { get; set; }

		// Token: 0x17004B6A RID: 19306
		// (get) Token: 0x0600F9BB RID: 63931 RVA: 0x003851AB File Offset: 0x003833AB
		// (set) Token: 0x0600F9BC RID: 63932 RVA: 0x003851B3 File Offset: 0x003833B3
		public IList<RadListBoxItem> Items { get; set; }
	}
}
