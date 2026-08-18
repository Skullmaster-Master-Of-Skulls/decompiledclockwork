using System;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x0200056B RID: 1387
	public class AppointmentBookingFilterParameters
	{
		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x00031AAD File Offset: 0x0002FCAD
		// (set) Token: 0x06002CA5 RID: 11429 RVA: 0x00031AB5 File Offset: 0x0002FCB5
		public int MaxNumberOfAppointmentsPerWeek { get; set; }

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x00031ABE File Offset: 0x0002FCBE
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x00031AC6 File Offset: 0x0002FCC6
		public int[] MaxNumberOfAppointmentsPerWeekAppTypeIds { get; set; }

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x00031ACF File Offset: 0x0002FCCF
		// (set) Token: 0x06002CA9 RID: 11433 RVA: 0x00031AD7 File Offset: 0x0002FCD7
		public int MaxNumberOfAppointmentsPerDay { get; set; }

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x00031AE0 File Offset: 0x0002FCE0
		// (set) Token: 0x06002CAB RID: 11435 RVA: 0x00031AE8 File Offset: 0x0002FCE8
		public int[] MaxNumberOfAppointmentsPerDayAppTypeIds { get; set; }

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x00031AF1 File Offset: 0x0002FCF1
		// (set) Token: 0x06002CAD RID: 11437 RVA: 0x00031AF9 File Offset: 0x0002FCF9
		public int MaxNumberOfNoShows { get; set; }

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x00031B02 File Offset: 0x0002FD02
		// (set) Token: 0x06002CAF RID: 11439 RVA: 0x00031B0A File Offset: 0x0002FD0A
		public int[] MaxNumberOfNoShowsAppTypeIds { get; set; }

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x00031B13 File Offset: 0x0002FD13
		// (set) Token: 0x06002CB1 RID: 11441 RVA: 0x00031B1B File Offset: 0x0002FD1B
		public int MaxNumberOfAppointmentsInFuture { get; set; }

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x00031B24 File Offset: 0x0002FD24
		// (set) Token: 0x06002CB3 RID: 11443 RVA: 0x00031B2C File Offset: 0x0002FD2C
		public int[] MaxNumberOfAppointmentsInFutureAppTypeIds { get; set; }

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x00031B35 File Offset: 0x0002FD35
		// (set) Token: 0x06002CB5 RID: 11445 RVA: 0x00031B3D File Offset: 0x0002FD3D
		public bool AllowDoubleBookingStaff { get; set; }

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x00031B46 File Offset: 0x0002FD46
		// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x00031B4E File Offset: 0x0002FD4E
		public bool AllowDoubleBookingStudent { get; set; }

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x00031B57 File Offset: 0x0002FD57
		// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x00031B5F File Offset: 0x0002FD5F
		public CutoffTime CutoffTime { get; set; }

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x00031B68 File Offset: 0x0002FD68
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x00031B70 File Offset: 0x0002FD70
		public int BannedExpiryDateCid { get; set; }
	}
}
