using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000855 RID: 2133
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateClockWorkServerJobReq : BaseMessageReq
	{
		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x00014A7A File Offset: 0x00012C7A
		// (set) Token: 0x06002B86 RID: 11142 RVA: 0x00014A82 File Offset: 0x00012C82
		[DataMember]
		public ClockWorkServerJobInfoDTO ClockWorkServerJob { get; set; }
	}
}
