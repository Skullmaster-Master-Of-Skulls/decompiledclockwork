using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B87 RID: 2951
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentDetailByIdResp
	{
		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06003E4B RID: 15947 RVA: 0x0001E873 File Offset: 0x0001CA73
		// (set) Token: 0x06003E4C RID: 15948 RVA: 0x0001E87B File Offset: 0x0001CA7B
		[DataMember]
		public MediaContentDetailDTO MediaContentDetail { get; set; }
	}
}
