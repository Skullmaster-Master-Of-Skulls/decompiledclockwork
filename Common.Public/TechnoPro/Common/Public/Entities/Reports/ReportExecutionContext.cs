using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000217 RID: 535
	public class ReportExecutionContext
	{
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x000176A7 File Offset: 0x000158A7
		// (set) Token: 0x0600104E RID: 4174 RVA: 0x000176AF File Offset: 0x000158AF
		public int WhoExecutedPersonId { get; set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000176B8 File Offset: 0x000158B8
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x000176C0 File Offset: 0x000158C0
		public DateTime ExecutionTimestamp { get; set; }

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x000176C9 File Offset: 0x000158C9
		// (set) Token: 0x06001052 RID: 4178 RVA: 0x000176D1 File Offset: 0x000158D1
		public eReportExecutedFromLocation ExecutionLocation { get; set; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x000176DA File Offset: 0x000158DA
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x000176E2 File Offset: 0x000158E2
		public int ReportId { get; set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x000176EB File Offset: 0x000158EB
		// (set) Token: 0x06001056 RID: 4182 RVA: 0x000176F3 File Offset: 0x000158F3
		public PersonBase WhoExecuted { get; set; }
	}
}
