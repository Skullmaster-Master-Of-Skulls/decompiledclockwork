using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B6D RID: 2925
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByIdentifierResp
	{
		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x0001E6CA File Offset: 0x0001C8CA
		// (set) Token: 0x06003E00 RID: 15872 RVA: 0x0001E6D2 File Offset: 0x0001C8D2
		[DataMember]
		public MediaContentDTO MediaContent { get; set; }
	}
}
