using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Veteran
{
	// Token: 0x02000123 RID: 291
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadChangeInBenefitRequestsResp
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00003415 File Offset: 0x00001615
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0000341D File Offset: 0x0000161D
		[DataMember]
		public IList<ChangeInBenefitRequestDTO> ChangeInBenefitRequests { get; set; }
	}
}
