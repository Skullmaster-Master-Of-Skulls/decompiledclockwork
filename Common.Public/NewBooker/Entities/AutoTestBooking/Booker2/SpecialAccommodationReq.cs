using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000A2 RID: 162
	public class SpecialAccommodationReq
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000CE49 File Offset: 0x0000B049
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000CE51 File Offset: 0x0000B051
		public Func<SpecialAccommodationReq, SpecialAccommodationRes> Func { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000CE5A File Offset: 0x0000B05A
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000CE62 File Offset: 0x0000B062
		public TryToBookWorking Working { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000CE6B File Offset: 0x0000B06B
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000CE73 File Offset: 0x0000B073
		public IList<TryToBookSpecialAccommodation> SpecialAccommodationsToApply { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000CE7C File Offset: 0x0000B07C
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000CE84 File Offset: 0x0000B084
		public TryToBookPotentialBooking PotentialBookingToAdd { get; set; }
	}
}
