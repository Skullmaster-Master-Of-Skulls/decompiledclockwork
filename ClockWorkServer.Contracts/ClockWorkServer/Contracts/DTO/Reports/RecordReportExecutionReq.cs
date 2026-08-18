using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000317 RID: 791
	[DataContract(Namespace = "http://tpro.ca")]
	public class RecordReportExecutionReq : BaseReportMessageReq
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x000086FE File Offset: 0x000068FE
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x00008706 File Offset: 0x00006906
		[DataMember]
		public eReportExecutedFromLocation ExectedFrom { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0000870F File Offset: 0x0000690F
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x00008717 File Offset: 0x00006917
		[DataMember]
		public int ReportId { get; set; }
	}
}
