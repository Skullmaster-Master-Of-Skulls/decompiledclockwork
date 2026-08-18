using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000330 RID: 816
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExportReportToXmlForUpdatingSystemReq : BaseReportMessageReq
	{
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x0000892F File Offset: 0x00006B2F
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x00008937 File Offset: 0x00006B37
		[DataMember]
		public IList<int> ReportIds { get; set; }
	}
}
