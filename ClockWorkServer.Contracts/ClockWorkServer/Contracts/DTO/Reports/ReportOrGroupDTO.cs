using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000345 RID: 837
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportOrGroupDTO
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x00008FAF File Offset: 0x000071AF
		// (set) Token: 0x06001332 RID: 4914 RVA: 0x00008FB7 File Offset: 0x000071B7
		[DataMember]
		public ReportDTO Report { get; set; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x00008FC0 File Offset: 0x000071C0
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x00008FC8 File Offset: 0x000071C8
		[DataMember]
		public ReportGroupDTO Group { get; set; }
	}
}
