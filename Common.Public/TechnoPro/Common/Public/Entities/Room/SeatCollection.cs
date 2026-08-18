using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Room
{
	// Token: 0x0200020D RID: 525
	public class SeatCollection
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x00017378 File Offset: 0x00015578
		// (set) Token: 0x06001005 RID: 4101 RVA: 0x00017380 File Offset: 0x00015580
		public IList<SeatGroup> AllSeatGroups { get; set; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00017389 File Offset: 0x00015589
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x00017391 File Offset: 0x00015591
		public IList<SeatAsset> AllAssets { get; set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0001739A File Offset: 0x0001559A
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x000173A2 File Offset: 0x000155A2
		public IList<Seat> Seats { get; set; }
	}
}
