using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults
{
	// Token: 0x02000358 RID: 856
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunFunctionResultWithDataDTO
	{
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x00009268 File Offset: 0x00007468
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x00009270 File Offset: 0x00007470
		[DataMember]
		public RunFunctionResultDTO Result { get; set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x00009279 File Offset: 0x00007479
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x00009281 File Offset: 0x00007481
		[DataMember]
		public RunFunctionDataDTO Data { get; set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0000928A File Offset: 0x0000748A
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x00009292 File Offset: 0x00007492
		[DataMember]
		public IList<ReportParameterDTO> ReportParametersOut { get; set; }
	}
}
