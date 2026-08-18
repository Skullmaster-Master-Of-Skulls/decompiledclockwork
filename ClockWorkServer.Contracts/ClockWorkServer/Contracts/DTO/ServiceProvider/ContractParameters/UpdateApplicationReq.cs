using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000285 RID: 645
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateApplicationReq : BaseMessageReq
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x000073D0 File Offset: 0x000055D0
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x000073D8 File Offset: 0x000055D8
		[DataMember]
		public SPApplicationDTO Application { get; set; }
	}
}
