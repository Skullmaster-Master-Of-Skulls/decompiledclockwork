using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B77 RID: 2935
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaContentResp
	{
		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x0001E774 File Offset: 0x0001C974
		// (set) Token: 0x06003E1E RID: 15902 RVA: 0x0001E77C File Offset: 0x0001C97C
		[DataMember]
		public MediaContentIdentifierDTO Identifier { get; set; }
	}
}
