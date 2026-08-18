using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000865 RID: 2149
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerJobStepByIdReq : BaseMessageReq
	{
		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x00014B8A File Offset: 0x00012D8A
		// (set) Token: 0x06002BB6 RID: 11190 RVA: 0x00014B92 File Offset: 0x00012D92
		[DataMember]
		public int JobId { get; set; }

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x00014B9B File Offset: 0x00012D9B
		// (set) Token: 0x06002BB8 RID: 11192 RVA: 0x00014BA3 File Offset: 0x00012DA3
		[DataMember]
		public int JobStepId { get; set; }
	}
}
