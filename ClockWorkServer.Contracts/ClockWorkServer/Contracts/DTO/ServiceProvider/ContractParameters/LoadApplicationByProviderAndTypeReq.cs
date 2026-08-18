using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000281 RID: 641
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadApplicationByProviderAndTypeReq : BaseMessageReq
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0000738C File Offset: 0x0000558C
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x00007394 File Offset: 0x00005594
		[DataMember]
		public int SPProviderId { get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0000739D File Offset: 0x0000559D
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x000073A5 File Offset: 0x000055A5
		[DataMember]
		public int SPProviderTypeId { get; set; }
	}
}
