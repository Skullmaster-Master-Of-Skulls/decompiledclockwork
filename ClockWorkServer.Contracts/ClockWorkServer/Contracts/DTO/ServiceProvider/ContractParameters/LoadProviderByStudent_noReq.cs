using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200029D RID: 669
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderByStudent_noReq : BaseMessageReq
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x000075CE File Offset: 0x000057CE
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x000075D6 File Offset: 0x000057D6
		[DataMember]
		public string Student_no { get; set; }
	}
}
