using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B92 RID: 2962
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentThumbnailBytesReq : BaseMessageReq
	{
		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06003E68 RID: 15976 RVA: 0x0001E90C File Offset: 0x0001CB0C
		// (set) Token: 0x06003E69 RID: 15977 RVA: 0x0001E914 File Offset: 0x0001CB14
		[DataMember]
		public MediaContentIdentifierDTO Identifier { get; set; }
	}
}
