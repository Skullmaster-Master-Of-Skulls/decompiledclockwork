using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B96 RID: 2966
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentCoverImageReq : BaseMessageReq
	{
		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06003E74 RID: 15988 RVA: 0x0001E950 File Offset: 0x0001CB50
		// (set) Token: 0x06003E75 RID: 15989 RVA: 0x0001E958 File Offset: 0x0001CB58
		[DataMember]
		public MediaContentIdentifierDTO Identifier { get; set; }
	}
}
