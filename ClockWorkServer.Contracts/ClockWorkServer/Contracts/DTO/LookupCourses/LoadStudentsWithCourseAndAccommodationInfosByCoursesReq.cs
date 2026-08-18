using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007FC RID: 2044
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsWithCourseAndAccommodationInfosByCoursesReq : BaseMessageReq
	{
		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x00013C64 File Offset: 0x00011E64
		// (set) Token: 0x060029B0 RID: 10672 RVA: 0x00013C6C File Offset: 0x00011E6C
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x00013C75 File Offset: 0x00011E75
		// (set) Token: 0x060029B2 RID: 10674 RVA: 0x00013C7D File Offset: 0x00011E7D
		[DataMember]
		public int AlternateContactId { get; set; }

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x00013C86 File Offset: 0x00011E86
		// (set) Token: 0x060029B4 RID: 10676 RVA: 0x00013C8E File Offset: 0x00011E8E
		[DataMember]
		public int[] LuCourseIds { get; set; }
	}
}
