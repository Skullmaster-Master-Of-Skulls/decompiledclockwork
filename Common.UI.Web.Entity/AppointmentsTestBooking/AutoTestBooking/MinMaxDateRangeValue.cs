using System;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.Common.UI.Web.Entity.AppointmentsTestBooking.AutoTestBooking
{
	// Token: 0x0200004B RID: 75
	public class MinMaxDateRangeValue
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000041A6 File Offset: 0x000023A6
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000041AE File Offset: 0x000023AE
		public Range<DateTime> DateRange { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000041B7 File Offset: 0x000023B7
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x000041BF File Offset: 0x000023BF
		public eMinMaxDateRangeInvalidReason Status { get; set; }
	}
}
