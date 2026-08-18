using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities
{
	// Token: 0x020002BD RID: 701
	public class MailMergeOutputOperationContext : OperationContext
	{
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0001A4C5 File Offset: 0x000186C5
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x0001A4CD File Offset: 0x000186CD
		public MailMergeTemplate Template { get; set; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0001A4D6 File Offset: 0x000186D6
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x0001A4DE File Offset: 0x000186DE
		public IList<IList<MailMergeCode>> CodeLists { get; set; }
	}
}
