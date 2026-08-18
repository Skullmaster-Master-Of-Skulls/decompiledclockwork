using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace System.Data.Entity.Migrations.History
{
	// Token: 0x020006F7 RID: 1783
	[Table("__MigrationHistory")]
	internal sealed class LegacyHistoryRow
	{
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06004766 RID: 18278 RVA: 0x00153790 File Offset: 0x00151990
		// (set) Token: 0x06004767 RID: 18279 RVA: 0x00153798 File Offset: 0x00151998
		public int Id { get; set; }

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06004768 RID: 18280 RVA: 0x001537A1 File Offset: 0x001519A1
		// (set) Token: 0x06004769 RID: 18281 RVA: 0x001537A9 File Offset: 0x001519A9
		public DateTime CreatedOn { get; set; }
	}
}
