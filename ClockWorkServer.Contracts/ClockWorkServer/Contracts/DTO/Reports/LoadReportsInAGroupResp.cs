using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200030B RID: 779
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportsInAGroupResp
	{
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00008621 File Offset: 0x00006821
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x00008629 File Offset: 0x00006829
		[DataMember]
		public ReportCollectionDTO ReportCollection { get; set; }
	}
}
