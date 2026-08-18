using System;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000516 RID: 1302
	public class Test : BaseExtendedAppointment
	{
		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x00029BFD File Offset: 0x00027DFD
		// (set) Token: 0x060027DB RID: 10203 RVA: 0x00029C05 File Offset: 0x00027E05
		public ClassTestBase ClassTestInfo { get; set; }

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x00029C0E File Offset: 0x00027E0E
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x00029C16 File Offset: 0x00027E16
		public StudentClassTestBase StudentClassTestInfo { get; set; }

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x00029C1F File Offset: 0x00027E1F
		// (set) Token: 0x060027DF RID: 10207 RVA: 0x00029C27 File Offset: 0x00027E27
		public int BreakTimeMinutes { get; set; }
	}
}
