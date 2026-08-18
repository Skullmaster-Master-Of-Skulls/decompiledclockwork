using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000AB RID: 171
	public class TryToBookEnvironment
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000D10E File Offset: 0x0000B30E
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000D116 File Offset: 0x0000B316
		public IList<TryToBookSpecialAccommodation> AllSpecialAccommodations { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000D11F File Offset: 0x0000B31F
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000D127 File Offset: 0x0000B327
		public IList<TryToBookAsset> AllAssets { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000D130 File Offset: 0x0000B330
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000D138 File Offset: 0x0000B338
		public IList<TryToBookRoom> AllRooms { get; set; }
	}
}
