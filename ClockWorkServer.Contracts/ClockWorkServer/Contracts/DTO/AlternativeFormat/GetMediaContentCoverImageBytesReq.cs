using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B98 RID: 2968
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentCoverImageBytesReq : BaseMessageReq
	{
		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06003E7A RID: 15994 RVA: 0x0001E972 File Offset: 0x0001CB72
		// (set) Token: 0x06003E7B RID: 15995 RVA: 0x0001E97A File Offset: 0x0001CB7A
		[DataMember]
		public MediaContentIdentifierDTO Identifier { get; set; }
	}
}
