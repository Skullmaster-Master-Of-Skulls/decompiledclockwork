using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Public.Entities.CourseRegistrations
{
	// Token: 0x0200043D RID: 1085
	public class CourseRegistration : BusinessBase<int>
	{
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x00024E54 File Offset: 0x00023054
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int CoursesId
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

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x00024E6C File Offset: 0x0002306C
		// (set) Token: 0x060020D9 RID: 8409 RVA: 0x00024E74 File Offset: 0x00023074
		public eRegistrationStatus RegistrationStatus { get; set; }

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x00024E7D File Offset: 0x0002307D
		// (set) Token: 0x060020DB RID: 8411 RVA: 0x00024E85 File Offset: 0x00023085
		public PersonBase Student { get; set; }

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x00024E8E File Offset: 0x0002308E
		// (set) Token: 0x060020DD RID: 8413 RVA: 0x00024E96 File Offset: 0x00023096
		public LookupCourse Course { get; set; }

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x00024E9F File Offset: 0x0002309F
		// (set) Token: 0x060020DF RID: 8415 RVA: 0x00024EA7 File Offset: 0x000230A7
		public DateTime DateAdded { get; set; }

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x00024EB0 File Offset: 0x000230B0
		// (set) Token: 0x060020E1 RID: 8417 RVA: 0x00024EB8 File Offset: 0x000230B8
		public PersonBase WhoAdded { get; set; }

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x00024EC1 File Offset: 0x000230C1
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x00024EC9 File Offset: 0x000230C9
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x00024ED2 File Offset: 0x000230D2
		// (set) Token: 0x060020E5 RID: 8421 RVA: 0x00024EDA File Offset: 0x000230DA
		public DateTime? DateLetterReturned { get; set; }

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x00024EE3 File Offset: 0x000230E3
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x00024EEB File Offset: 0x000230EB
		public string CourseNote { get; set; }

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x00024EF4 File Offset: 0x000230F4
		// (set) Token: 0x060020E9 RID: 8425 RVA: 0x00024EFC File Offset: 0x000230FC
		public DateTime? DateStudentLastViewed { get; set; }

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x00024F05 File Offset: 0x00023105
		// (set) Token: 0x060020EB RID: 8427 RVA: 0x00024F0D File Offset: 0x0002310D
		public DateTime? DateInstructorLastViewed { get; set; }

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x00024F16 File Offset: 0x00023116
		// (set) Token: 0x060020ED RID: 8429 RVA: 0x00024F1E File Offset: 0x0002311E
		public bool IsExemptFromDataSync { get; set; }

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x00024F27 File Offset: 0x00023127
		// (set) Token: 0x060020EF RID: 8431 RVA: 0x00024F2F File Offset: 0x0002312F
		public IList<int> ExemptedInstructorAssignments { get; set; }

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060020F0 RID: 8432 RVA: 0x00024F38 File Offset: 0x00023138
		// (set) Token: 0x060020F1 RID: 8433 RVA: 0x00024F40 File Offset: 0x00023140
		public CourseRequestBase CourseAccommodationRequestBase { get; set; }
	}
}
