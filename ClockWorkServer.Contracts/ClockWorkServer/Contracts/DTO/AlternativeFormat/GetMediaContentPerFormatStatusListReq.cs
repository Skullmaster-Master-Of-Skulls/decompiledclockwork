using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B82 RID: 2946
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentPerFormatStatusListReq : BaseMessageReq
	{
		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x0001E80D File Offset: 0x0001CA0D
		// (set) Token: 0x06003E3B RID: 15931 RVA: 0x0001E815 File Offset: 0x0001CA15
		[DataMember]
		public Guid MediaContentId { get; set; }

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x0001E81E File Offset: 0x0001CA1E
		// (set) Token: 0x06003E3D RID: 15933 RVA: 0x0001E826 File Offset: 0x0001CA26
		[DataMember]
		public int StudentId { get; set; }
	}
}
