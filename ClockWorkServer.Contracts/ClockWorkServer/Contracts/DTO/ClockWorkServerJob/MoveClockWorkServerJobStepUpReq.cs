using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000873 RID: 2163
	[DataContract(Namespace = "http://tpro.ca")]
	public class MoveClockWorkServerJobStepUpReq : BaseMessageReq
	{
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x00014C89 File Offset: 0x00012E89
		// (set) Token: 0x06002BE2 RID: 11234 RVA: 0x00014C91 File Offset: 0x00012E91
		[DataMember]
		public ClockWorkServerJobStepDTO JobStep { get; set; }
	}
}
