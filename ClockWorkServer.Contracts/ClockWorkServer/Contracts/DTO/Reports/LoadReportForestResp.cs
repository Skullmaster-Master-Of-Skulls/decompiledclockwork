using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000311 RID: 785
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportForestResp
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x00008698 File Offset: 0x00006898
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x000086A0 File Offset: 0x000068A0
		[DataMember]
		public Forest<ReportOrGroupDTO> ReportForest { get; set; }
	}
}
