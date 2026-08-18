using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm
{
	// Token: 0x020002F9 RID: 761
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInfoPmIdForCurrentSessionResp
	{
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x0000838A File Offset: 0x0000658A
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x00008392 File Offset: 0x00006592
		[DataMember]
		public int InfoPmId { get; set; }
	}
}
