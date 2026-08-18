using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002AF RID: 687
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllProvidersWithAtLeastOneActiveApplicationReq : BaseMessageReq
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x000076CD File Offset: 0x000058CD
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x000076D5 File Offset: 0x000058D5
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x000076DE File Offset: 0x000058DE
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x000076E6 File Offset: 0x000058E6
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
