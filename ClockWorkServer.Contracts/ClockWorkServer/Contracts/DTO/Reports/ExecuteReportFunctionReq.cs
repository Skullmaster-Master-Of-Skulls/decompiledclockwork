using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000320 RID: 800
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteReportFunctionReq : BaseReportMessageReq
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x000087EC File Offset: 0x000069EC
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x000087F4 File Offset: 0x000069F4
		[DataMember]
		public eFunctionType FunctionToExecute { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x000087FD File Offset: 0x000069FD
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x00008805 File Offset: 0x00006A05
		[DataMember]
		public IList<ReportParameterDTO> FunctionParameters { get; set; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0000880E File Offset: 0x00006A0E
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x00008816 File Offset: 0x00006A16
		[DataMember]
		public RunFunctionDataDTO CurrentData { get; set; }
	}
}
