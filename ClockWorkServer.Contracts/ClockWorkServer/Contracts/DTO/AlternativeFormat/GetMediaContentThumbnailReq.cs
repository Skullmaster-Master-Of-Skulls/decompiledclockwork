using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B90 RID: 2960
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentThumbnailReq : BaseMessageReq
	{
		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06003E62 RID: 15970 RVA: 0x0001E8EA File Offset: 0x0001CAEA
		// (set) Token: 0x06003E63 RID: 15971 RVA: 0x0001E8F2 File Offset: 0x0001CAF2
		[DataMember]
		public MediaContentIdentifierDTO Identifier { get; set; }
	}
}
