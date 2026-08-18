using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002A1 RID: 673
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderByExternalIdReq : BaseMessageReq
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00007612 File Offset: 0x00005812
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x0000761A File Offset: 0x0000581A
		[DataMember]
		public string ExternalId { get; set; }
	}
}
