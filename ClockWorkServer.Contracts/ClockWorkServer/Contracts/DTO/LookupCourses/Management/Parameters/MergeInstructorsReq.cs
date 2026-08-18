using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management.Parameters
{
	// Token: 0x0200081A RID: 2074
	[DataContract(Namespace = "http://tpro.ca")]
	public class MergeInstructorsReq : BaseMessageReq
	{
		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x0001410C File Offset: 0x0001230C
		// (set) Token: 0x06002A42 RID: 10818 RVA: 0x00014114 File Offset: 0x00012314
		[DataMember]
		public int InstructorId1 { get; set; }

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x0001411D File Offset: 0x0001231D
		// (set) Token: 0x06002A44 RID: 10820 RVA: 0x00014125 File Offset: 0x00012325
		[DataMember]
		public int InstructorId2 { get; set; }
	}
}
