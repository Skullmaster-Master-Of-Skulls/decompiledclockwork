using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200032C RID: 812
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExportReportToXmlForUserReq : BaseReportMessageReq
	{
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x000088EB File Offset: 0x00006AEB
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x000088F3 File Offset: 0x00006AF3
		[DataMember]
		public IList<int> ReportIds { get; set; }
	}
}
