using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000329 RID: 809
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateClientReportBuiltByTproResp
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x000088B8 File Offset: 0x00006AB8
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x000088C0 File Offset: 0x00006AC0
		[DataMember]
		public int ReportId { get; set; }
	}
}
