using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003AE RID: 942
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAssignedAdvisorSignatureDataResp
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00009DDA File Offset: 0x00007FDA
		// (set) Token: 0x06001506 RID: 5382 RVA: 0x00009DE2 File Offset: 0x00007FE2
		[DataMember]
		public DynamicDataDTO Data { get; set; }
	}
}
