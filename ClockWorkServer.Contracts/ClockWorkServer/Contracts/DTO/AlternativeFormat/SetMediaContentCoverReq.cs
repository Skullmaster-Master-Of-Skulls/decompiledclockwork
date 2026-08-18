using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B9A RID: 2970
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(Bitmap))]
	public class SetMediaContentCoverReq : BaseMessageReq
	{
		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06003E80 RID: 16000 RVA: 0x0001E994 File Offset: 0x0001CB94
		// (set) Token: 0x06003E81 RID: 16001 RVA: 0x0001E99C File Offset: 0x0001CB9C
		[DataMember]
		public Guid MediaContentId { get; set; }

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06003E82 RID: 16002 RVA: 0x0001E9A5 File Offset: 0x0001CBA5
		// (set) Token: 0x06003E83 RID: 16003 RVA: 0x0001E9AD File Offset: 0x0001CBAD
		[DataMember]
		public Image CoverImage { get; set; }
	}
}
