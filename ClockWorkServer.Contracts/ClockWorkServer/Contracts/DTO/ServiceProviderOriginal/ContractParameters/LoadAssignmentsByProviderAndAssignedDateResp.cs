using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters
{
	// Token: 0x020002E6 RID: 742
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAssignmentsByProviderAndAssignedDateResp
	{
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00007F35 File Offset: 0x00006135
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x00007F3D File Offset: 0x0000613D
		[DataMember]
		public IList<ServiceProviderAssignmentDTO> Assignments { get; set; }
	}
}
