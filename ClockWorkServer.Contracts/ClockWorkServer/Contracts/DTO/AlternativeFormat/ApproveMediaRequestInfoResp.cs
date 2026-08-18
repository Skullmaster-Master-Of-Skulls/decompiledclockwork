using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C53 RID: 3155
	[DataContract(Namespace = "http://tpro.ca")]
	public class ApproveMediaRequestInfoResp
	{
		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x060041D9 RID: 16857 RVA: 0x000202D4 File Offset: 0x0001E4D4
		// (set) Token: 0x060041DA RID: 16858 RVA: 0x000202DC File Offset: 0x0001E4DC
		[DataMember]
		public MediaContentRequestedInfoDTO MediaContentRequestedInfo { get; set; }
	}
}
