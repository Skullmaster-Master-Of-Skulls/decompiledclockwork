using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000845 RID: 2117
	[DataContract(Namespace = "http://tpro.ca")]
	public class StopControllerJobReq : ClockWorkServerJobBaseReq
	{
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x0001474A File Offset: 0x0001294A
		// (set) Token: 0x06002B1A RID: 11034 RVA: 0x00014752 File Offset: 0x00012952
		[DataMember]
		public int JobId { get; set; }
	}
}
