using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007DE RID: 2014
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateInstructorDataSyncExemptionReq : BaseMessageReq
	{
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x00013934 File Offset: 0x00011B34
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x0001393C File Offset: 0x00011B3C
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x00013945 File Offset: 0x00011B45
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x0001394D File Offset: 0x00011B4D
		[DataMember]
		public bool NewInstructorExemptStatus { get; set; }
	}
}
