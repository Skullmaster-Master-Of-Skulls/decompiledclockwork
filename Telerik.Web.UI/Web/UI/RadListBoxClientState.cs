using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200193F RID: 6463
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadListBoxClientState
	{
		// Token: 0x0600FA19 RID: 64025 RVA: 0x0038590A File Offset: 0x00383B0A
		public RadListBoxClientState()
		{
			this.IsEnabled = true;
		}

		// Token: 0x17004B92 RID: 19346
		// (get) Token: 0x0600FA1A RID: 64026 RVA: 0x00385919 File Offset: 0x00383B19
		// (set) Token: 0x0600FA1B RID: 64027 RVA: 0x00385921 File Offset: 0x00383B21
		public ClientStateLogEntry[] LogEntries { get; set; }

		// Token: 0x17004B93 RID: 19347
		// (get) Token: 0x0600FA1C RID: 64028 RVA: 0x0038592A File Offset: 0x00383B2A
		// (set) Token: 0x0600FA1D RID: 64029 RVA: 0x00385932 File Offset: 0x00383B32
		public int[] SelectedIndices { get; set; }

		// Token: 0x17004B94 RID: 19348
		// (get) Token: 0x0600FA1E RID: 64030 RVA: 0x0038593B File Offset: 0x00383B3B
		// (set) Token: 0x0600FA1F RID: 64031 RVA: 0x00385943 File Offset: 0x00383B43
		public int[] CheckedIndices { get; set; }

		// Token: 0x17004B95 RID: 19349
		// (get) Token: 0x0600FA20 RID: 64032 RVA: 0x0038594C File Offset: 0x00383B4C
		// (set) Token: 0x0600FA21 RID: 64033 RVA: 0x00385954 File Offset: 0x00383B54
		public int ScrollPosition { get; set; }

		// Token: 0x17004B96 RID: 19350
		// (get) Token: 0x0600FA22 RID: 64034 RVA: 0x0038595D File Offset: 0x00383B5D
		// (set) Token: 0x0600FA23 RID: 64035 RVA: 0x00385965 File Offset: 0x00383B65
		public bool IsEnabled { get; set; }
	}
}
