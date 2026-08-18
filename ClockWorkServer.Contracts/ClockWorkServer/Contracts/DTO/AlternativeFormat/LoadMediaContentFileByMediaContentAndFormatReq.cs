using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B63 RID: 2915
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentFileByMediaContentAndFormatReq : BaseMessageReq
	{
		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06003DD1 RID: 15825 RVA: 0x0001E598 File Offset: 0x0001C798
		// (set) Token: 0x06003DD2 RID: 15826 RVA: 0x0001E5A0 File Offset: 0x0001C7A0
		[DataMember]
		public Guid MediaContentId { get; set; }

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x0001E5A9 File Offset: 0x0001C7A9
		// (set) Token: 0x06003DD4 RID: 15828 RVA: 0x0001E5B1 File Offset: 0x0001C7B1
		[DataMember]
		public MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x0001E5BA File Offset: 0x0001C7BA
		// (set) Token: 0x06003DD6 RID: 15830 RVA: 0x0001E5C2 File Offset: 0x0001C7C2
		[DataMember]
		public int StudentId { get; set; }
	}
}
