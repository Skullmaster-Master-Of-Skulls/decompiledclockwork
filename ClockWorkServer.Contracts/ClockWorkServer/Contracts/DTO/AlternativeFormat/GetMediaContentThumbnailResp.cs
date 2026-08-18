using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B91 RID: 2961
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(Bitmap))]
	public class GetMediaContentThumbnailResp
	{
		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06003E65 RID: 15973 RVA: 0x0001E8FB File Offset: 0x0001CAFB
		// (set) Token: 0x06003E66 RID: 15974 RVA: 0x0001E903 File Offset: 0x0001CB03
		[DataMember]
		public Image Thumbnail { get; set; }
	}
}
