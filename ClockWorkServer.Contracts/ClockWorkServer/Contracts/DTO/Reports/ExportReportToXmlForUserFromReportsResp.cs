using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200032F RID: 815
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExportReportToXmlForUserFromReportsResp
	{
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0000891E File Offset: 0x00006B1E
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x00008926 File Offset: 0x00006B26
		[DataMember]
		public string Xml { get; set; }
	}
}
