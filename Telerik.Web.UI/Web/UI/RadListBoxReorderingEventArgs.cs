using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001935 RID: 6453
	public class RadListBoxReorderingEventArgs : EventArgs
	{
		// Token: 0x17004B63 RID: 19299
		// (get) Token: 0x0600F9A8 RID: 63912 RVA: 0x0038512C File Offset: 0x0038332C
		// (set) Token: 0x0600F9A9 RID: 63913 RVA: 0x00385134 File Offset: 0x00383334
		public IList<RadListBoxItem> Items { get; set; }

		// Token: 0x17004B64 RID: 19300
		// (get) Token: 0x0600F9AA RID: 63914 RVA: 0x0038513D File Offset: 0x0038333D
		// (set) Token: 0x0600F9AB RID: 63915 RVA: 0x00385145 File Offset: 0x00383345
		public bool Cancel { get; set; }

		// Token: 0x17004B65 RID: 19301
		// (get) Token: 0x0600F9AC RID: 63916 RVA: 0x0038514E File Offset: 0x0038334E
		// (set) Token: 0x0600F9AD RID: 63917 RVA: 0x00385156 File Offset: 0x00383356
		public int Offset { get; set; }

		// Token: 0x17004B66 RID: 19302
		// (get) Token: 0x0600F9AE RID: 63918 RVA: 0x0038515F File Offset: 0x0038335F
		// (set) Token: 0x0600F9AF RID: 63919 RVA: 0x00385167 File Offset: 0x00383367
		public int Index { get; set; }
	}
}
