using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000319 RID: 793
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveReportTechnoProNoteReq : BaseReportMessageReq
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00008731 File Offset: 0x00006931
		// (set) Token: 0x06001215 RID: 4629 RVA: 0x00008739 File Offset: 0x00006939
		[DataMember]
		public int ReportId { get; set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00008742 File Offset: 0x00006942
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x0000874A File Offset: 0x0000694A
		[DataMember]
		public string Rtf { get; set; }
	}
}
