using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200031A RID: 794
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportTechnoProNoteReq : BaseReportMessageReq
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00008753 File Offset: 0x00006953
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x0000875B File Offset: 0x0000695B
		[DataMember]
		public int ReportId { get; set; }
	}
}
