using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000853 RID: 2131
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerJobByIdReq : BaseMessageReq
	{
		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x00014A58 File Offset: 0x00012C58
		// (set) Token: 0x06002B80 RID: 11136 RVA: 0x00014A60 File Offset: 0x00012C60
		[DataMember]
		public int JobId { get; set; }
	}
}
