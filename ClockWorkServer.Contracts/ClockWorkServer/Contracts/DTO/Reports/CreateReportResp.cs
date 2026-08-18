using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200034B RID: 843
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateReportResp
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x00009026 File Offset: 0x00007226
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x0000902E File Offset: 0x0000722E
		[DataMember]
		public int ReportId { get; set; }
	}
}
