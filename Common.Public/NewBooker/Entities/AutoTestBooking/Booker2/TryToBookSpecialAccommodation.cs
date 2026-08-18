using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000B3 RID: 179
	public class TryToBookSpecialAccommodation
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000D527 File Offset: 0x0000B727
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0000D52F File Offset: 0x0000B72F
		public int ControlId { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000D538 File Offset: 0x0000B738
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0000D540 File Offset: 0x0000B740
		public eSpecialAccommodationType Type { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000D549 File Offset: 0x0000B749
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0000D551 File Offset: 0x0000B751
		public IDictionary<string, string> Args { get; set; }
	}
}
