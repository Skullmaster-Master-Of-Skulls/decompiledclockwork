using System;
using System.Data.Common;

namespace System.Data.Entity.Migrations.History
{
	// Token: 0x020006F6 RID: 1782
	internal sealed class LegacyHistoryContext : DbContext
	{
		// Token: 0x06004763 RID: 18275 RVA: 0x00153769 File Offset: 0x00151969
		public LegacyHistoryContext(DbConnection existingConnection) : base(existingConnection, false)
		{
			this.InternalContext.InitializerDisabled = true;
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06004764 RID: 18276 RVA: 0x0015377F File Offset: 0x0015197F
		// (set) Token: 0x06004765 RID: 18277 RVA: 0x00153787 File Offset: 0x00151987
		public IDbSet<LegacyHistoryRow> History { get; set; }
	}
}
