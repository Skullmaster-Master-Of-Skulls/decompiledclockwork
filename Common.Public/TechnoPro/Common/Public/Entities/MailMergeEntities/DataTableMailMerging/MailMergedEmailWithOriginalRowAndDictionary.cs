using System;
using System.Data;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.DataTableMailMerging
{
	// Token: 0x020002E0 RID: 736
	public class MailMergedEmailWithOriginalRowAndDictionary
	{
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0001B8D5 File Offset: 0x00019AD5
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x0001B8DD File Offset: 0x00019ADD
		public MailMergeContextWithCustomDictionary ContextWithCustomDictionary { get; set; }

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0001B8E6 File Offset: 0x00019AE6
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x0001B8EE File Offset: 0x00019AEE
		public TPMailMessage MergedEmail { get; set; }

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0001B8F7 File Offset: 0x00019AF7
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x0001B8FF File Offset: 0x00019AFF
		public DataRow[] OriginalRows { get; set; }
	}
}
