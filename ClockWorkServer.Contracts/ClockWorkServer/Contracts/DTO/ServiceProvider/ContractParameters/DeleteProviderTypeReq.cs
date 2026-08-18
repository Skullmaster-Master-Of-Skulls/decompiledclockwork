using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002BB RID: 699
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProviderTypeReq : BaseMessageReq
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00007777 File Offset: 0x00005977
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x0000777F File Offset: 0x0000597F
		[DataMember]
		public int SPProviderTypeId { get; set; }
	}
}
