using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200085B RID: 2139
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveClockWorkServerJobReq : BaseMessageReq
	{
		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x00014B02 File Offset: 0x00012D02
		// (set) Token: 0x06002B9C RID: 11164 RVA: 0x00014B0A File Offset: 0x00012D0A
		[DataMember]
		public int JobId { get; set; }
	}
}
