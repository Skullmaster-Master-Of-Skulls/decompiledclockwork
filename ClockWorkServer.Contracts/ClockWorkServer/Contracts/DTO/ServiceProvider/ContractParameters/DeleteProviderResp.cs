using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002A6 RID: 678
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProviderResp
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00007656 File Offset: 0x00005856
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x0000765E File Offset: 0x0000585E
		[DataMember]
		public bool Worked { get; set; }
	}
}
