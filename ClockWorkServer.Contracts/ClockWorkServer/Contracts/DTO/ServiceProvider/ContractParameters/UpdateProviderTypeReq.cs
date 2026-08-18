using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002B9 RID: 697
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateProviderTypeReq : BaseMessageReq
	{
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00007766 File Offset: 0x00005966
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x0000776E File Offset: 0x0000596E
		[DataMember]
		public SPProviderTypeDTO ProviderType { get; set; }
	}
}
