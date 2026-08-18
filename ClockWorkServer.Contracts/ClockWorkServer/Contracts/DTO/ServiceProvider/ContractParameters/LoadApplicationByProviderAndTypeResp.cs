using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000280 RID: 640
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadApplicationByProviderAndTypeResp
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0000737B File Offset: 0x0000557B
		// (set) Token: 0x06000F51 RID: 3921 RVA: 0x00007383 File Offset: 0x00005583
		[DataMember]
		public SPApplicationDTO Application { get; set; }
	}
}
