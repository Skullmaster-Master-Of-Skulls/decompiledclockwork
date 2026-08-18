using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200030A RID: 778
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportsInAGroupReq : BaseReportMessageReq
	{
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00008610 File Offset: 0x00006810
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x00008618 File Offset: 0x00006818
		[DataMember]
		public IList<string> ReportGroupTitles { get; set; }
	}
}
