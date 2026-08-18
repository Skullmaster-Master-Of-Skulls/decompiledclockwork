using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C64 RID: 3172
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentRequestedInfoByIdReq : BaseReportMessageReq
	{
		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x06004216 RID: 16918 RVA: 0x0002044A File Offset: 0x0001E64A
		// (set) Token: 0x06004217 RID: 16919 RVA: 0x00020452 File Offset: 0x0001E652
		[DataMember]
		public int MediaContentRequestedId { get; set; }
	}
}
