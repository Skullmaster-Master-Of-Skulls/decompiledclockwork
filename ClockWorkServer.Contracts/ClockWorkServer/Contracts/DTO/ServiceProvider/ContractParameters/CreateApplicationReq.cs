using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000283 RID: 643
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateApplicationReq : BaseMessageReq
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x000073BF File Offset: 0x000055BF
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x000073C7 File Offset: 0x000055C7
		[DataMember]
		public SPApplicationDTO Application { get; set; }
	}
}
