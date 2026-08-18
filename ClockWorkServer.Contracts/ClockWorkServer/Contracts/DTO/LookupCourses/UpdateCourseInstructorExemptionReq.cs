using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007A9 RID: 1961
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateCourseInstructorExemptionReq : BaseMessageReq
	{
		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x0001327E File Offset: 0x0001147E
		// (set) Token: 0x06002859 RID: 10329 RVA: 0x00013286 File Offset: 0x00011486
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600285A RID: 10330 RVA: 0x0001328F File Offset: 0x0001148F
		// (set) Token: 0x0600285B RID: 10331 RVA: 0x00013297 File Offset: 0x00011497
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x000132A0 File Offset: 0x000114A0
		// (set) Token: 0x0600285D RID: 10333 RVA: 0x000132A8 File Offset: 0x000114A8
		[DataMember]
		public bool NewIsInstructorExemptFromCourseList { get; set; }
	}
}
