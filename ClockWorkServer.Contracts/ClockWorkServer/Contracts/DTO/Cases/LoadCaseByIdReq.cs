using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x0200089E RID: 2206
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCaseByIdReq : BaseMessageReq
	{
		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x0001530F File Offset: 0x0001350F
		// (set) Token: 0x06002CC1 RID: 11457 RVA: 0x00015317 File Offset: 0x00013517
		[DataMember]
		public int InfoPcId { get; set; }

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x00015320 File Offset: 0x00013520
		// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x00015328 File Offset: 0x00013528
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
