using System;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest
{
	// Token: 0x0200052C RID: 1324
	public class TestForEdit2 : BaseExtendedAppointment
	{
		// Token: 0x060029FD RID: 10749 RVA: 0x0002AF4E File Offset: 0x0002914E
		public TestForEdit2()
		{
			this.BookingSpecificInfo = new TestForEditBookingSpecific();
			this.ClassTestDefinitionSpecificInfo = new TestForEditClassDefinitionSpecific();
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x0002AF70 File Offset: 0x00029170
		// (set) Token: 0x060029FF RID: 10751 RVA: 0x0002AF78 File Offset: 0x00029178
		public int ExamId { get; set; }

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x0002AF81 File Offset: 0x00029181
		// (set) Token: 0x06002A01 RID: 10753 RVA: 0x0002AF89 File Offset: 0x00029189
		public int LuCourseId { get; set; }

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x0002AF92 File Offset: 0x00029192
		// (set) Token: 0x06002A03 RID: 10755 RVA: 0x0002AF9A File Offset: 0x0002919A
		public TestForEditBookingSpecific BookingSpecificInfo { get; set; }

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x0002AFA3 File Offset: 0x000291A3
		// (set) Token: 0x06002A05 RID: 10757 RVA: 0x0002AFAB File Offset: 0x000291AB
		public TestForEditClassDefinitionSpecific ClassTestDefinitionSpecificInfo { get; set; }

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x0002AFB4 File Offset: 0x000291B4
		// (set) Token: 0x06002A07 RID: 10759 RVA: 0x0002AFBC File Offset: 0x000291BC
		public int BreakTimeMinutes { get; set; }
	}
}
