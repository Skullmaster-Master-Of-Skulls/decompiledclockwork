using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000B1 RID: 177
	public class TryToBookSchedule
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000D3F5 File Offset: 0x0000B5F5
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000D3FD File Offset: 0x0000B5FD
		public int RoomPersonId { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000D406 File Offset: 0x0000B606
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0000D40E File Offset: 0x0000B60E
		public DateTime Date { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000D417 File Offset: 0x0000B617
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0000D41F File Offset: 0x0000B61F
		public IList<TryToBookAvailability> Availability { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000D428 File Offset: 0x0000B628
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0000D430 File Offset: 0x0000B630
		public IList<TryToBookAvailability> JustAvailability { get; set; }
	}
}
