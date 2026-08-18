using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults
{
	// Token: 0x02000357 RID: 855
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunFunctionResultDTO
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00009246 File Offset: 0x00007446
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x0000924E File Offset: 0x0000744E
		[DataMember]
		public ReportFunctionDTO Function { get; set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00009257 File Offset: 0x00007457
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x0000925F File Offset: 0x0000745F
		[DataMember]
		public RunStatusDTO Status { get; set; }
	}
}
