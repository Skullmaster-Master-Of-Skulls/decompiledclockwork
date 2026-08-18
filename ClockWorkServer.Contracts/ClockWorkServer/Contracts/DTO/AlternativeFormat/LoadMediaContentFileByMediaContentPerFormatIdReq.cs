using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B61 RID: 2913
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentFileByMediaContentPerFormatIdReq : BaseMessageReq
	{
		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x0001E565 File Offset: 0x0001C765
		// (set) Token: 0x06003DCA RID: 15818 RVA: 0x0001E56D File Offset: 0x0001C76D
		[DataMember]
		public int MediaContentPerFormatId { get; set; }

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x0001E576 File Offset: 0x0001C776
		// (set) Token: 0x06003DCC RID: 15820 RVA: 0x0001E57E File Offset: 0x0001C77E
		[DataMember]
		public int StudentId { get; set; }
	}
}
