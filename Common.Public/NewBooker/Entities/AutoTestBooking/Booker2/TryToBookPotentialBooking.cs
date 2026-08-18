using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000AD RID: 173
	public class TryToBookPotentialBooking
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000D152 File Offset: 0x0000B352
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000D15A File Offset: 0x0000B35A
		public TryToBookRoom Room { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000D163 File Offset: 0x0000B363
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000D16B File Offset: 0x0000B36B
		public DateTime StartDateTime { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000D174 File Offset: 0x0000B374
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000D17C File Offset: 0x0000B37C
		public DateTime EndDateTime { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000D185 File Offset: 0x0000B385
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000D18D File Offset: 0x0000B38D
		public IList<string> Notices { get; set; }
	}
}
