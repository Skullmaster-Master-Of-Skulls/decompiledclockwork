using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002B1 RID: 689
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderTypeByIdReq : BaseMessageReq
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00007700 File Offset: 0x00005900
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x00007708 File Offset: 0x00005908
		[DataMember]
		public int SPProviderTypeId { get; set; }
	}
}
