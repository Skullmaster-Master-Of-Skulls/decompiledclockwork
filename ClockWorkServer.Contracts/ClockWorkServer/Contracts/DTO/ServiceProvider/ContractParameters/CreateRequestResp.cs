using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002C2 RID: 706
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateRequestResp
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00007876 File Offset: 0x00005A76
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0000787E File Offset: 0x00005A7E
		[DataMember]
		public int SPRequestId { get; set; }
	}
}
