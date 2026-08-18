using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000287 RID: 647
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteApplicationReq : BaseMessageReq
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x000073F2 File Offset: 0x000055F2
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x000073FA File Offset: 0x000055FA
		[DataMember]
		public int SPApplicationId { get; set; }
	}
}
