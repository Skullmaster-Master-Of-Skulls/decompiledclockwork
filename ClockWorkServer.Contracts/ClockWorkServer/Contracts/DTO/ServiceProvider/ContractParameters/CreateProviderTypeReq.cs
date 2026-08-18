using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002B7 RID: 695
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProviderTypeReq : BaseMessageReq
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00007755 File Offset: 0x00005955
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x0000775D File Offset: 0x0000595D
		[DataMember]
		public SPProviderTypeDTO ProviderType { get; set; }
	}
}
