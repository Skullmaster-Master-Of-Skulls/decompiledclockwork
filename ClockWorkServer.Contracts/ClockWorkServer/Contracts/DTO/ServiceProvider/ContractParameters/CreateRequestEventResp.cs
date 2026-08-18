using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002CE RID: 718
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateRequestEventResp
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00007931 File Offset: 0x00005B31
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x00007939 File Offset: 0x00005B39
		[DataMember]
		public int SPRequestEventId { get; set; }
	}
}
