using System;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Public.Entities.CourseRegistrations
{
	// Token: 0x02000435 RID: 1077
	public class CourseRegistrationBase : BusinessBase<int>
	{
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x00024BD0 File Offset: 0x00022DD0
		// (set) Token: 0x0600208A RID: 8330 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x00024BE8 File Offset: 0x00022DE8
		// (set) Token: 0x0600208C RID: 8332 RVA: 0x00024BF0 File Offset: 0x00022DF0
		public eRegistrationStatus RegistrationStatus { get; set; }

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x00024BF9 File Offset: 0x00022DF9
		// (set) Token: 0x0600208E RID: 8334 RVA: 0x00024C01 File Offset: 0x00022E01
		public PersonBase Student { get; set; }

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x00024C0A File Offset: 0x00022E0A
		// (set) Token: 0x06002090 RID: 8336 RVA: 0x00024C12 File Offset: 0x00022E12
		public LookupCourseBase Course { get; set; }

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x00024C1B File Offset: 0x00022E1B
		// (set) Token: 0x06002092 RID: 8338 RVA: 0x00024C23 File Offset: 0x00022E23
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x00024C2C File Offset: 0x00022E2C
		// (set) Token: 0x06002094 RID: 8340 RVA: 0x00024C34 File Offset: 0x00022E34
		public DateTime? DateLetterReturned { get; set; }

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x00024C3D File Offset: 0x00022E3D
		// (set) Token: 0x06002096 RID: 8342 RVA: 0x00024C45 File Offset: 0x00022E45
		public string CourseNote { get; set; }

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x00024C4E File Offset: 0x00022E4E
		// (set) Token: 0x06002098 RID: 8344 RVA: 0x00024C56 File Offset: 0x00022E56
		public DateTime? DateStudentLastViewed { get; set; }

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x00024C5F File Offset: 0x00022E5F
		// (set) Token: 0x0600209A RID: 8346 RVA: 0x00024C67 File Offset: 0x00022E67
		public DateTime? DateInstructorLastViewed { get; set; }

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x00024C70 File Offset: 0x00022E70
		// (set) Token: 0x0600209C RID: 8348 RVA: 0x00024C78 File Offset: 0x00022E78
		public CourseRequestBase CourseAccommodationRequestBase { get; set; }
	}
}
