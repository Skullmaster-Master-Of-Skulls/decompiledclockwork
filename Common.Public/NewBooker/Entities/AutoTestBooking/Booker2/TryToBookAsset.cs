using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000A5 RID: 165
	public class TryToBookAsset
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000CF04 File Offset: 0x0000B104
		public string Id { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000CF0D File Offset: 0x0000B10D
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000CF15 File Offset: 0x0000B115
		public int Score { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000CF1E File Offset: 0x0000B11E
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000CF26 File Offset: 0x0000B126
		public IList<TryToBookAssetAccommodation> AssetAccommodations { get; set; }
	}
}
