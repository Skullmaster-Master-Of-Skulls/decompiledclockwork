using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000305 RID: 773
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportGroupForestResp
	{
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00008522 File Offset: 0x00006722
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x0000852A File Offset: 0x0000672A
		[DataMember]
		public Forest<ReportGroupDTO> ReportGroups { get; set; }
	}
}
