using System;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000A3 RID: 163
	public class SpecialAccommodationRes
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000CE96 File Offset: 0x0000B096
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000CE9E File Offset: 0x0000B09E
		public bool AbortFindPotentialBookingsProcess { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000CEA7 File Offset: 0x0000B0A7
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000CEAF File Offset: 0x0000B0AF
		public TryToBookTimeToInvestigate TimeToInvestigate { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
		public TryToBookPotentialBooking PotentialBookingToAdd { get; set; }
	}
}
