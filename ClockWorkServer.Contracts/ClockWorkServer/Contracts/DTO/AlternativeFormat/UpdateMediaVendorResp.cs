using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C22 RID: 3106
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMediaVendorResp
	{
		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x06004130 RID: 16688 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		// (set) Token: 0x06004131 RID: 16689 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
		[DataMember]
		public bool WasUpdated { get; set; }
	}
}
