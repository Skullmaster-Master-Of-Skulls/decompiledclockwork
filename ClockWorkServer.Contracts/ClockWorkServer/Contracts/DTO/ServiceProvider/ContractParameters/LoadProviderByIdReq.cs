using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200029B RID: 667
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderByIdReq : BaseMessageReq
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x000075AC File Offset: 0x000057AC
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x000075B4 File Offset: 0x000057B4
		[DataMember]
		public int SPProviderId { get; set; }
	}
}
