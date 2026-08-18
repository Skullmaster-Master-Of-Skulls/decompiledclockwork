using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B93 RID: 2963
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(Bitmap))]
	public class GetMediaContentThumbnailBytesResp
	{
		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06003E6B RID: 15979 RVA: 0x0001E91D File Offset: 0x0001CB1D
		// (set) Token: 0x06003E6C RID: 15980 RVA: 0x0001E925 File Offset: 0x0001CB25
		[DataMember]
		public byte[] Thumbnail { get; set; }
	}
}
