using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000879 RID: 2169
	[DataContract(Namespace = "http://tpro.ca")]
	public class DisableClockWorkServerJobReq : BaseMessageReq
	{
		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x00014D00 File Offset: 0x00012F00
		// (set) Token: 0x06002BF6 RID: 11254 RVA: 0x00014D08 File Offset: 0x00012F08
		[DataMember]
		public int JobId { get; set; }
	}
}
