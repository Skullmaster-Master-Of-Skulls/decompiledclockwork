using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002BD RID: 701
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestByIdReq : BaseMessageReq
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00007799 File Offset: 0x00005999
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x000077A1 File Offset: 0x000059A1
		[DataMember]
		public int SPRequestId { get; set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000077AA File Offset: 0x000059AA
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x000077B2 File Offset: 0x000059B2
		[DataMember]
		public bool IncludeSubItems { get; set; }
	}
}
