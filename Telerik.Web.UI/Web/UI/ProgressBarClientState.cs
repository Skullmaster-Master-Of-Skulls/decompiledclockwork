using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000775 RID: 1909
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ProgressBarClientState
	{
		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x000D2C40 File Offset: 0x000D0E40
		// (set) Token: 0x0600435C RID: 17244 RVA: 0x000D2C48 File Offset: 0x000D0E48
		public double Value { get; set; }

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x000D2C51 File Offset: 0x000D0E51
		// (set) Token: 0x0600435E RID: 17246 RVA: 0x000D2C59 File Offset: 0x000D0E59
		public bool Indeterminate { get; set; }
	}
}
