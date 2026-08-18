using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200031B RID: 795
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadReportTechnoProNoteResp
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00008764 File Offset: 0x00006964
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x0000876C File Offset: 0x0000696C
		[DataMember]
		public string Rtf { get; set; }
	}
}
