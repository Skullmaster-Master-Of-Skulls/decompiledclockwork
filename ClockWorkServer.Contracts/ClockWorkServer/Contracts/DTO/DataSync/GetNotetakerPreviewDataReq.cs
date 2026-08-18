using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x0200071F RID: 1823
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNotetakerPreviewDataReq : BaseReportMessageReq
	{
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x00011265 File Offset: 0x0000F465
		// (set) Token: 0x0600258E RID: 9614 RVA: 0x0001126D File Offset: 0x0000F46D
		[DataMember]
		public string UserName { get; set; }
	}
}
