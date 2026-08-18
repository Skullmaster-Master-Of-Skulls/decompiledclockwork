using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200032E RID: 814
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExportReportToXmlForUserFromReportsReq : BaseReportMessageReq
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0000890D File Offset: 0x00006B0D
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x00008915 File Offset: 0x00006B15
		[DataMember]
		public ReportCollectionDTO ReportCollection { get; set; }
	}
}
