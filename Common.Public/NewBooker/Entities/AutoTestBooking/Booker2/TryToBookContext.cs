using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000AA RID: 170
	public class TryToBookContext
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000D097 File Offset: 0x0000B297
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000D09F File Offset: 0x0000B29F
		public int LuCourseId { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
		public int PersonId { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000D0C1 File Offset: 0x0000B2C1
		public string CourseCampus { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000D0CA File Offset: 0x0000B2CA
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000D0D2 File Offset: 0x0000B2D2
		public DateTime ClassTestDate { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000D0DB File Offset: 0x0000B2DB
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000D0E3 File Offset: 0x0000B2E3
		public TimeSpan ClassStartTime { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		public int ClassTestMinutes { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000D0FD File Offset: 0x0000B2FD
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000D105 File Offset: 0x0000B305
		public IList<TryToBookAccommodationToUse> AccommodationsToUse { get; set; }
	}
}
