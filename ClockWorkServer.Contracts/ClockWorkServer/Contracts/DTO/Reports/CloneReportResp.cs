using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000333 RID: 819
	[DataContract(Namespace = "http://tpro.ca")]
	public class CloneReportResp
	{
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00008962 File Offset: 0x00006B62
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x0000896A File Offset: 0x00006B6A
		[DataMember]
		public int ReportId { get; set; }
	}
}
