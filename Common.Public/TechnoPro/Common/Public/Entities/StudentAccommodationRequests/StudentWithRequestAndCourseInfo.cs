using System;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x020001A6 RID: 422
	public class StudentWithRequestAndCourseInfo : BusinessBase<int>
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00013C68 File Offset: 0x00011E68
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int StudentCourseAccommodationRequestId
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00013C80 File Offset: 0x00011E80
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x00013C88 File Offset: 0x00011E88
		public PersonBase Student { get; set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00013C91 File Offset: 0x00011E91
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x00013C99 File Offset: 0x00011E99
		public eStudentCourseAccommodationRequestStatus Status { get; set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00013CA2 File Offset: 0x00011EA2
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00013CAA File Offset: 0x00011EAA
		public LookupCourseBase CourseBase { get; set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00013CB3 File Offset: 0x00011EB3
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x00013CBB File Offset: 0x00011EBB
		public DateTime? DateLetterReturned { get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00013CC4 File Offset: 0x00011EC4
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x00013CCC File Offset: 0x00011ECC
		public DateTime RequestDate { get; set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00013CD5 File Offset: 0x00011ED5
		// (set) Token: 0x06000AFF RID: 2815 RVA: 0x00013CDD File Offset: 0x00011EDD
		public DateTime? DateApproved { get; set; }
	}
}
