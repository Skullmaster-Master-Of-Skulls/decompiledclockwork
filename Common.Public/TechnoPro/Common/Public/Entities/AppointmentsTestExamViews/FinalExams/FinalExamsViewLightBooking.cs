using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams
{
	// Token: 0x020004FB RID: 1275
	public class FinalExamsViewLightBooking
	{
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x00029236 File Offset: 0x00027436
		// (set) Token: 0x060026BD RID: 9917 RVA: 0x0002923E File Offset: 0x0002743E
		public int AppointmentId { get; set; }

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x00029247 File Offset: 0x00027447
		// (set) Token: 0x060026BF RID: 9919 RVA: 0x0002924F File Offset: 0x0002744F
		public bool IsCancelled { get; set; }

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x00029258 File Offset: 0x00027458
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x00029260 File Offset: 0x00027460
		public bool IsTentative { get; set; }

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x00029269 File Offset: 0x00027469
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x00029271 File Offset: 0x00027471
		public bool IsNoShow { get; set; }

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x0002927A File Offset: 0x0002747A
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x00029282 File Offset: 0x00027482
		public BasicPerson Student { get; set; }

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x0002928B File Offset: 0x0002748B
		// (set) Token: 0x060026C7 RID: 9927 RVA: 0x00029293 File Offset: 0x00027493
		public int LuCourseId { get; set; }

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x0002929C File Offset: 0x0002749C
		// (set) Token: 0x060026C9 RID: 9929 RVA: 0x000292A4 File Offset: 0x000274A4
		public virtual string CourseTitle { get; set; }

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x000292AD File Offset: 0x000274AD
		// (set) Token: 0x060026CB RID: 9931 RVA: 0x000292B5 File Offset: 0x000274B5
		public DateTime DateBooked { get; set; }

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x000292BE File Offset: 0x000274BE
		// (set) Token: 0x060026CD RID: 9933 RVA: 0x000292C6 File Offset: 0x000274C6
		public DateTime StudentReportedClassTestStartDateTime { get; set; }

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x000292CF File Offset: 0x000274CF
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x000292D7 File Offset: 0x000274D7
		public DateTime StudentReportedClassTestEndDateTime { get; set; }
	}
}
