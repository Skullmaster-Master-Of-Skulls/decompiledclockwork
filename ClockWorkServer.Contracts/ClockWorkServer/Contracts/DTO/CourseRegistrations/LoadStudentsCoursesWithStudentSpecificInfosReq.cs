using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x0200082F RID: 2095
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsCoursesWithStudentSpecificInfosReq : BaseMessageReq
	{
		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x0001440F File Offset: 0x0001260F
		// (set) Token: 0x06002AAF RID: 10927 RVA: 0x00014417 File Offset: 0x00012617
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x00014420 File Offset: 0x00012620
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x00014428 File Offset: 0x00012628
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x00014431 File Offset: 0x00012631
		// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x00014439 File Offset: 0x00012639
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x00014442 File Offset: 0x00012642
		// (set) Token: 0x06002AB5 RID: 10933 RVA: 0x0001444A File Offset: 0x0001264A
		[DataMember]
		public bool IncludeDroppedCourses { get; set; }
	}
}
