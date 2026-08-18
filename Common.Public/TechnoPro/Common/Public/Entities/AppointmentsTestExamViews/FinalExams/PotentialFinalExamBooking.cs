using System;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams
{
	// Token: 0x020004FC RID: 1276
	public class PotentialFinalExamBooking
	{
		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x000292E0 File Offset: 0x000274E0
		// (set) Token: 0x060026D2 RID: 9938 RVA: 0x000292E8 File Offset: 0x000274E8
		public LookupCourseBase Course { get; set; }

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x000292F1 File Offset: 0x000274F1
		// (set) Token: 0x060026D4 RID: 9940 RVA: 0x000292F9 File Offset: 0x000274F9
		public BasicPerson Student { get; set; }

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x00029302 File Offset: 0x00027502
		// (set) Token: 0x060026D6 RID: 9942 RVA: 0x0002930A File Offset: 0x0002750A
		public int ExamId { get; set; }

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x00029313 File Offset: 0x00027513
		// (set) Token: 0x060026D8 RID: 9944 RVA: 0x0002931B File Offset: 0x0002751B
		public DateTime ExamStartDateTime { get; set; }

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x00029324 File Offset: 0x00027524
		// (set) Token: 0x060026DA RID: 9946 RVA: 0x0002932C File Offset: 0x0002752C
		public DateTime ExamEndDateTime { get; set; }
	}
}
