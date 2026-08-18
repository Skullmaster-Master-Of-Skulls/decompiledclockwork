using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B94 RID: 2964
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(Bitmap))]
	public class SetMediaContentThumbnailReq : BaseMessageReq
	{
		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06003E6E RID: 15982 RVA: 0x0001E92E File Offset: 0x0001CB2E
		// (set) Token: 0x06003E6F RID: 15983 RVA: 0x0001E936 File Offset: 0x0001CB36
		[DataMember]
		public Guid MediaContentId { get; set; }

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06003E70 RID: 15984 RVA: 0x0001E93F File Offset: 0x0001CB3F
		// (set) Token: 0x06003E71 RID: 15985 RVA: 0x0001E947 File Offset: 0x0001CB47
		[DataMember]
		public Image Thumbnail { get; set; }
	}
}
