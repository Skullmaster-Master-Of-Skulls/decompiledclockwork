using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams
{
	// Token: 0x020004F9 RID: 1273
	public class FinalExamsViewBase
	{
		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x0002913F File Offset: 0x0002733F
		// (set) Token: 0x0600269F RID: 9887 RVA: 0x00029147 File Offset: 0x00027347
		public int ExamId { get; set; }

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x00029150 File Offset: 0x00027350
		// (set) Token: 0x060026A1 RID: 9889 RVA: 0x00029158 File Offset: 0x00027358
		public int LuCourseId { get; set; }

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x00029161 File Offset: 0x00027361
		// (set) Token: 0x060026A3 RID: 9891 RVA: 0x00029169 File Offset: 0x00027369
		public DateTime ExamStartDateTime { get; set; }

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x00029172 File Offset: 0x00027372
		// (set) Token: 0x060026A5 RID: 9893 RVA: 0x0002917A File Offset: 0x0002737A
		public DateTime ExamEndDateTime { get; set; }

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x00029183 File Offset: 0x00027383
		// (set) Token: 0x060026A7 RID: 9895 RVA: 0x0002918B File Offset: 0x0002738B
		public virtual string CourseTitle { get; set; }

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x00029194 File Offset: 0x00027394
		// (set) Token: 0x060026A9 RID: 9897 RVA: 0x0002919C File Offset: 0x0002739C
		public bool HasTestCopy { get; set; }

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x000291A5 File Offset: 0x000273A5
		// (set) Token: 0x060026AB RID: 9899 RVA: 0x000291AD File Offset: 0x000273AD
		public string TestCopyNote { get; set; }

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x000291B6 File Offset: 0x000273B6
		// (set) Token: 0x060026AD RID: 9901 RVA: 0x000291BE File Offset: 0x000273BE
		public DateTime DateEntered { get; set; }

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x000291C7 File Offset: 0x000273C7
		// (set) Token: 0x060026AF RID: 9903 RVA: 0x000291CF File Offset: 0x000273CF
		public DateTime? DateLastModified { get; set; }

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x060026B0 RID: 9904 RVA: 0x000291D8 File Offset: 0x000273D8
		// (set) Token: 0x060026B1 RID: 9905 RVA: 0x000291E0 File Offset: 0x000273E0
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x000291E9 File Offset: 0x000273E9
		// (set) Token: 0x060026B3 RID: 9907 RVA: 0x000291F1 File Offset: 0x000273F1
		public string InstructorContactedNote { get; set; }

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x000291FA File Offset: 0x000273FA
		// (set) Token: 0x060026B5 RID: 9909 RVA: 0x00029202 File Offset: 0x00027402
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x0002920B File Offset: 0x0002740B
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x00029213 File Offset: 0x00027413
		public string TestPickedUpNote { get; set; }
	}
}
