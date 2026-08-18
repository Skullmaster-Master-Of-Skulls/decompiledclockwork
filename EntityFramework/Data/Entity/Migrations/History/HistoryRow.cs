using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.History
{
	// Token: 0x020006F5 RID: 1781
	public class HistoryRow
	{
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x0600475A RID: 18266 RVA: 0x0015371D File Offset: 0x0015191D
		// (set) Token: 0x0600475B RID: 18267 RVA: 0x00153725 File Offset: 0x00151925
		public string MigrationId { get; set; }

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600475C RID: 18268 RVA: 0x0015372E File Offset: 0x0015192E
		// (set) Token: 0x0600475D RID: 18269 RVA: 0x00153736 File Offset: 0x00151936
		public string ContextKey { get; set; }

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600475E RID: 18270 RVA: 0x0015373F File Offset: 0x0015193F
		// (set) Token: 0x0600475F RID: 18271 RVA: 0x00153747 File Offset: 0x00151947
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public byte[] Model { get; set; }

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06004760 RID: 18272 RVA: 0x00153750 File Offset: 0x00151950
		// (set) Token: 0x06004761 RID: 18273 RVA: 0x00153758 File Offset: 0x00151958
		public string ProductVersion { get; set; }
	}
}
