using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000315 RID: 789
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteReportReq : BaseReportMessageReq
	{
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x000086DC File Offset: 0x000068DC
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x000086E4 File Offset: 0x000068E4
		[DataMember]
		public int ReportId { get; set; }
	}
}
