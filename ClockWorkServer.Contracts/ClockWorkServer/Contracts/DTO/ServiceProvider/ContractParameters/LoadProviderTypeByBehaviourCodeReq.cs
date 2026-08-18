using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002B3 RID: 691
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderTypeByBehaviourCodeReq : BaseMessageReq
	{
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x0000772A File Offset: 0x0000592A
		[DataMember]
		public eProviderTypeBehaviourCode BehaviourCode { get; set; }
	}
}
