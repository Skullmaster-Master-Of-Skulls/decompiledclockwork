using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200030D RID: 781
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportForestBySourceResp
	{
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00008654 File Offset: 0x00006854
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x0000865C File Offset: 0x0000685C
		[DataMember]
		public Forest<ReportOrGroupDTO> ReportForest { get; set; }
	}
}
