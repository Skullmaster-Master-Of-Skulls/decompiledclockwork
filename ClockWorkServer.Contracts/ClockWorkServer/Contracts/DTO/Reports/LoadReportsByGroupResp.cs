using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000351 RID: 849
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportsByGroupResp
	{
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0000917A File Offset: 0x0000737A
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x00009182 File Offset: 0x00007382
		[DataMember]
		public Forest<ReportOrGroupDTO> Reports { get; set; }
	}
}
