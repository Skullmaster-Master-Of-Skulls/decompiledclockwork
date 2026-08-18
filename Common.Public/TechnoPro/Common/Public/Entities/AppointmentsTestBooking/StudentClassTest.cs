using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000512 RID: 1298
	public class StudentClassTest : StudentClassTestBase
	{
		// Token: 0x060027A7 RID: 10151 RVA: 0x00029A05 File Offset: 0x00027C05
		public StudentClassTest()
		{
			this.TestNote = "";
			base.Course = new LookupCourseBase();
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x00029A27 File Offset: 0x00027C27
		// (set) Token: 0x060027A9 RID: 10153 RVA: 0x00029A2F File Offset: 0x00027C2F
		public DateTime? StudentReportedClassStartDateTime { get; set; }

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x00029A38 File Offset: 0x00027C38
		// (set) Token: 0x060027AB RID: 10155 RVA: 0x00029A40 File Offset: 0x00027C40
		public DateTime? StudentReportedClassEndDateTime { get; set; }

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x00029A49 File Offset: 0x00027C49
		// (set) Token: 0x060027AD RID: 10157 RVA: 0x00029A51 File Offset: 0x00027C51
		public string TestNote { get; set; }

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x00029A5A File Offset: 0x00027C5A
		// (set) Token: 0x060027AF RID: 10159 RVA: 0x00029A62 File Offset: 0x00027C62
		public string BookingNote { get; set; }

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x00029A6B File Offset: 0x00027C6B
		// (set) Token: 0x060027B1 RID: 10161 RVA: 0x00029A73 File Offset: 0x00027C73
		public string PrivateNote { get; set; }

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x00029A7C File Offset: 0x00027C7C
		// (set) Token: 0x060027B3 RID: 10163 RVA: 0x00029A84 File Offset: 0x00027C84
		public string ExtendedProperties { get; set; }
	}
}
