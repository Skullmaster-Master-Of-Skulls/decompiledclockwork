using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002A7 RID: 679
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProviderReq : BaseMessageReq
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00007667 File Offset: 0x00005867
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0000766F File Offset: 0x0000586F
		[DataMember]
		public int SPProviderId { get; set; }
	}
}
