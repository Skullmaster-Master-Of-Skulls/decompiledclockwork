using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000715 RID: 1813
	[DataContract(Namespace = "http://tpro.ca")]
	public class PreviewDataSyncDataReq : BaseReportMessageReq
	{
		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x000111BB File Offset: 0x0000F3BB
		// (set) Token: 0x06002570 RID: 9584 RVA: 0x000111C3 File Offset: 0x0000F3C3
		[DataMember]
		public string Student_no { get; set; }
	}
}
