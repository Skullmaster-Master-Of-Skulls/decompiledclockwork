using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200032D RID: 813
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExportReportToXmlForUserResp
	{
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x000088FC File Offset: 0x00006AFC
		// (set) Token: 0x0600125F RID: 4703 RVA: 0x00008904 File Offset: 0x00006B04
		[DataMember]
		public string Xml { get; set; }
	}
}
