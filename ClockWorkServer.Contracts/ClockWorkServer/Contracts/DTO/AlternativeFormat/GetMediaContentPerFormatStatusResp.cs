using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C57 RID: 3159
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaContentPerFormatStatusResp
	{
		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x060041EF RID: 16879 RVA: 0x0002036D File Offset: 0x0001E56D
		// (set) Token: 0x060041F0 RID: 16880 RVA: 0x00020375 File Offset: 0x0001E575
		[DataMember]
		public MediaContentPerFormatStatusInfoDTO Status { get; set; }
	}
}
