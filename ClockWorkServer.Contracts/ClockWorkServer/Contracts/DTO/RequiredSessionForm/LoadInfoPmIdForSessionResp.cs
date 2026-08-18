using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm
{
	// Token: 0x020002FB RID: 763
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInfoPmIdForSessionResp
	{
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000083CE File Offset: 0x000065CE
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x000083D6 File Offset: 0x000065D6
		[DataMember]
		public int InfoPmId { get; set; }
	}
}
