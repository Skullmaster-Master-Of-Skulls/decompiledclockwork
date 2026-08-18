using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200030C RID: 780
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportForestBySourceReq : BaseReportMessageReq
	{
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00008632 File Offset: 0x00006832
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x0000863A File Offset: 0x0000683A
		[DataMember]
		public string Xml { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00008643 File Offset: 0x00006843
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x0000864B File Offset: 0x0000684B
		[DataMember]
		public ReportContextDTO ReportContext { get; set; }
	}
}
