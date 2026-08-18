using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000352 RID: 850
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportParametersLegacyDTO
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x0000918B File Offset: 0x0000738B
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x00009193 File Offset: 0x00007393
		[DataMember]
		public eReportBuiltInDynamicForm BuiltInDynamicForm { get; set; }
	}
}
