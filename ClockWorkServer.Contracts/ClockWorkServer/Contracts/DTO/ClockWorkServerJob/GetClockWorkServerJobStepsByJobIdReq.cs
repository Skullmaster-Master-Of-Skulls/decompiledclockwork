using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000863 RID: 2147
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerJobStepsByJobIdReq : BaseMessageReq
	{
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x00014B68 File Offset: 0x00012D68
		// (set) Token: 0x06002BB0 RID: 11184 RVA: 0x00014B70 File Offset: 0x00012D70
		[DataMember]
		public int JobId { get; set; }
	}
}
