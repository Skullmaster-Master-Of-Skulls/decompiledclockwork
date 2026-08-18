using System;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x020004FD RID: 1277
	public class BasicTest : BaseBasicAppointment
	{
		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x00029335 File Offset: 0x00027535
		// (set) Token: 0x060026DD RID: 9949 RVA: 0x0002933D File Offset: 0x0002753D
		public PersonBase Student { get; set; }

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x00029346 File Offset: 0x00027546
		// (set) Token: 0x060026DF RID: 9951 RVA: 0x0002934E File Offset: 0x0002754E
		public int ExamId { get; set; }

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x00029357 File Offset: 0x00027557
		// (set) Token: 0x060026E1 RID: 9953 RVA: 0x0002935F File Offset: 0x0002755F
		public DateTime? ClassStartDateTime { get; set; }

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x060026E2 RID: 9954 RVA: 0x00029368 File Offset: 0x00027568
		// (set) Token: 0x060026E3 RID: 9955 RVA: 0x00029370 File Offset: 0x00027570
		public DateTime? ClassEndDateTime { get; set; }

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x00029379 File Offset: 0x00027579
		// (set) Token: 0x060026E5 RID: 9957 RVA: 0x00029381 File Offset: 0x00027581
		public eClassTestType ExamType { get; set; }

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x0002938A File Offset: 0x0002758A
		// (set) Token: 0x060026E7 RID: 9959 RVA: 0x00029392 File Offset: 0x00027592
		public LookupCourseBase CourseBase { get; set; }
	}
}
