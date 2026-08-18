using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000515 RID: 1301
	public class StudentClassTestBase : BusinessBase<int>
	{
		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x00029BD4 File Offset: 0x00027DD4
		// (set) Token: 0x060027D6 RID: 10198 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentCourseId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x00029BEC File Offset: 0x00027DEC
		// (set) Token: 0x060027D8 RID: 10200 RVA: 0x00029BF4 File Offset: 0x00027DF4
		public LookupCourseBase Course { get; set; }
	}
}
