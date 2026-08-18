using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007E5 RID: 2021
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveInstructorFromCourseReq : BaseMessageReq
	{
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x000139CD File Offset: 0x00011BCD
		// (set) Token: 0x0600294B RID: 10571 RVA: 0x000139D5 File Offset: 0x00011BD5
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000139DE File Offset: 0x00011BDE
		// (set) Token: 0x0600294D RID: 10573 RVA: 0x000139E6 File Offset: 0x00011BE6
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
