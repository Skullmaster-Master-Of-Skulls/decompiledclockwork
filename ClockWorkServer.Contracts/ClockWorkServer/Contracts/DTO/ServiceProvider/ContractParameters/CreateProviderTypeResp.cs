using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002B6 RID: 694
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProviderTypeResp
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x00007744 File Offset: 0x00005944
		// (set) Token: 0x06000FF9 RID: 4089 RVA: 0x0000774C File Offset: 0x0000594C
		[DataMember]
		public int SPProviderTypeId { get; set; }
	}
}
