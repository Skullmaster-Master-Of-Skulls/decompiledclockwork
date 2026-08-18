using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x0200081F RID: 2079
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsCoursesReq : BaseMessageReq
	{
		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x000141FA File Offset: 0x000123FA
		// (set) Token: 0x06002A63 RID: 10851 RVA: 0x00014202 File Offset: 0x00012402
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x0001420B File Offset: 0x0001240B
		// (set) Token: 0x06002A65 RID: 10853 RVA: 0x00014213 File Offset: 0x00012413
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06002A66 RID: 10854 RVA: 0x0001421C File Offset: 0x0001241C
		// (set) Token: 0x06002A67 RID: 10855 RVA: 0x00014224 File Offset: 0x00012424
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x0001422D File Offset: 0x0001242D
		// (set) Token: 0x06002A69 RID: 10857 RVA: 0x00014235 File Offset: 0x00012435
		[DataMember]
		public bool IncludeDroppedCourses { get; set; }
	}
}
