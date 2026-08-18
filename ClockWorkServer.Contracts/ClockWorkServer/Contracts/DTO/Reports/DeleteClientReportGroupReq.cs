using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000318 RID: 792
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteClientReportGroupReq : BaseReportMessageReq
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00008720 File Offset: 0x00006920
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x00008728 File Offset: 0x00006928
		[DataMember]
		public int ReportGroupId { get; set; }
	}
}
