using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007F0 RID: 2032
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveInstructorsForCourseReq : BaseMessageReq
	{
		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x00013A99 File Offset: 0x00011C99
		// (set) Token: 0x0600296E RID: 10606 RVA: 0x00013AA1 File Offset: 0x00011CA1
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x00013AAA File Offset: 0x00011CAA
		// (set) Token: 0x06002970 RID: 10608 RVA: 0x00013AB2 File Offset: 0x00011CB2
		[DataMember]
		public bool UpdateInstructorInfo { get; set; }

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x00013ABB File Offset: 0x00011CBB
		// (set) Token: 0x06002972 RID: 10610 RVA: 0x00013AC3 File Offset: 0x00011CC3
		[DataMember]
		public List<LookupInstructorDTO> Instructors { get; set; }
	}
}
