using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000321 RID: 801
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteReportFunctionResp
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0000881F File Offset: 0x00006A1F
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x00008827 File Offset: 0x00006A27
		[DataMember]
		public RunFunctionDataDTO ExecuteFunctionResult { get; set; }
	}
}
