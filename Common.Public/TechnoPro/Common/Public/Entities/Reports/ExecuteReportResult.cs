using System;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x0200022E RID: 558
	public class ExecuteReportResult
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00017EF2 File Offset: 0x000160F2
		// (set) Token: 0x06001114 RID: 4372 RVA: 0x00017EFA File Offset: 0x000160FA
		public virtual IList<DataTable> OptionalTables { get; set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00017F03 File Offset: 0x00016103
		// (set) Token: 0x06001116 RID: 4374 RVA: 0x00017F0B File Offset: 0x0001610B
		public virtual DataTable PrimaryTable { get; set; }

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00017F14 File Offset: 0x00016114
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x00017F1C File Offset: 0x0001611C
		public virtual string Title { get; set; }

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x00017F25 File Offset: 0x00016125
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x00017F2D File Offset: 0x0001612D
		public virtual DateTime StartDate { get; set; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00017F36 File Offset: 0x00016136
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x00017F3E File Offset: 0x0001613E
		public virtual DateTime EndDate { get; set; }
	}
}
