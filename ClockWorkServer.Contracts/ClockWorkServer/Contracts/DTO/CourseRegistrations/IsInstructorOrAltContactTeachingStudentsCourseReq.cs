using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x0200082D RID: 2093
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsInstructorOrAltContactTeachingStudentsCourseReq : BaseMessageReq
	{
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x000143BA File Offset: 0x000125BA
		// (set) Token: 0x06002AA3 RID: 10915 RVA: 0x000143C2 File Offset: 0x000125C2
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x000143CB File Offset: 0x000125CB
		// (set) Token: 0x06002AA5 RID: 10917 RVA: 0x000143D3 File Offset: 0x000125D3
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x000143DC File Offset: 0x000125DC
		// (set) Token: 0x06002AA7 RID: 10919 RVA: 0x000143E4 File Offset: 0x000125E4
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000143ED File Offset: 0x000125ED
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x000143F5 File Offset: 0x000125F5
		[DataMember]
		public int AlternateContactId { get; set; }
	}
}
