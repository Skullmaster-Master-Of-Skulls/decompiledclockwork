using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x020004FE RID: 1278
	public class ClassTestForDisplay : BusinessBase<int>
	{
		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x000293A4 File Offset: 0x000275A4
		// (set) Token: 0x060026EA RID: 9962 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ExamId
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

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x000293BC File Offset: 0x000275BC
		// (set) Token: 0x060026EC RID: 9964 RVA: 0x000293C4 File Offset: 0x000275C4
		public LookupCourseBaseWithPrimaryInstructor CourseWithPrimaryInstructor { get; set; }

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x000293CD File Offset: 0x000275CD
		// (set) Token: 0x060026EE RID: 9966 RVA: 0x000293D5 File Offset: 0x000275D5
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x000293DE File Offset: 0x000275DE
		// (set) Token: 0x060026F0 RID: 9968 RVA: 0x000293E6 File Offset: 0x000275E6
		public DateTime EndDateTime { get; set; }

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x000293EF File Offset: 0x000275EF
		// (set) Token: 0x060026F2 RID: 9970 RVA: 0x000293F7 File Offset: 0x000275F7
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x00029400 File Offset: 0x00027600
		// (set) Token: 0x060026F4 RID: 9972 RVA: 0x00029408 File Offset: 0x00027608
		public string InstructorContactedNote { get; set; }

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x00029411 File Offset: 0x00027611
		// (set) Token: 0x060026F6 RID: 9974 RVA: 0x00029419 File Offset: 0x00027619
		public eClassTestType ExamType { get; set; }

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x00029422 File Offset: 0x00027622
		// (set) Token: 0x060026F8 RID: 9976 RVA: 0x0002942A File Offset: 0x0002762A
		public string Location { get; set; }

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x00029433 File Offset: 0x00027633
		// (set) Token: 0x060026FA RID: 9978 RVA: 0x0002943B File Offset: 0x0002763B
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x00029444 File Offset: 0x00027644
		// (set) Token: 0x060026FC RID: 9980 RVA: 0x0002944C File Offset: 0x0002764C
		public string TestPickedUpNote { get; set; }

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x00029455 File Offset: 0x00027655
		// (set) Token: 0x060026FE RID: 9982 RVA: 0x0002945D File Offset: 0x0002765D
		public IList<DynamicData> InstructorFormData { get; set; }
	}
}
