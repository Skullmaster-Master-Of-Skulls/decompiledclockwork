using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B97 RID: 2967
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(Bitmap))]
	public class GetMediaContentCoverImageResp
	{
		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06003E77 RID: 15991 RVA: 0x0001E961 File Offset: 0x0001CB61
		// (set) Token: 0x06003E78 RID: 15992 RVA: 0x0001E969 File Offset: 0x0001CB69
		[DataMember]
		public Image CoverImage { get; set; }
	}
}
