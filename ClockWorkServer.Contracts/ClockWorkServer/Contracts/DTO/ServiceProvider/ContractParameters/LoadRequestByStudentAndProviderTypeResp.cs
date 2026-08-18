using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002BE RID: 702
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestByStudentAndProviderTypeResp
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x000077BB File Offset: 0x000059BB
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x000077C3 File Offset: 0x000059C3
		[DataMember]
		public SPRequestWithSubItemsDTO RequestWithSubItems { get; set; }
	}
}
