using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000286 RID: 646
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteApplicationResp
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x000073E1 File Offset: 0x000055E1
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x000073E9 File Offset: 0x000055E9
		[DataMember]
		public bool Worked { get; set; }
	}
}
