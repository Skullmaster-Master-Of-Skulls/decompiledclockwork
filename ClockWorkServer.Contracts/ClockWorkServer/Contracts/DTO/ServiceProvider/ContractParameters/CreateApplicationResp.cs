using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000282 RID: 642
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateApplicationResp
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x000073AE File Offset: 0x000055AE
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x000073B6 File Offset: 0x000055B6
		[DataMember]
		public int SPApplicationId { get; set; }
	}
}
