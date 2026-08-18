using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000331 RID: 817
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExportReportToXmlForUpdatingSystemResp
	{
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00008940 File Offset: 0x00006B40
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x00008948 File Offset: 0x00006B48
		[DataMember]
		public string Xml { get; set; }
	}
}
