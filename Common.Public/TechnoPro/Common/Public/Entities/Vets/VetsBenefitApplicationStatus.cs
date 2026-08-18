using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x02000107 RID: 263
	public class VetsBenefitApplicationStatus : VetsBenefitApplication
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000F074 File Offset: 0x0000D274
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0000F07C File Offset: 0x0000D27C
		public PersonBase Screener { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000F085 File Offset: 0x0000D285
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0000F08D File Offset: 0x0000D28D
		public PersonBase Certifier { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000F096 File Offset: 0x0000D296
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0000F09E File Offset: 0x0000D29E
		public ProgressStep CurrentProgressStep { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000F0A7 File Offset: 0x0000D2A7
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0000F0AF File Offset: 0x0000D2AF
		public new eVetsRequestStatus FinalStatus { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		public IList<VetsRequestStatusNote> Notes { get; set; }
	}
}
