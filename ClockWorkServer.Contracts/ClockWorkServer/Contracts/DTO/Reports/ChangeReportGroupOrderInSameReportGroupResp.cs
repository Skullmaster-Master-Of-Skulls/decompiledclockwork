using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x020002FF RID: 767
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeReportGroupOrderInSameReportGroupResp
	{
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00008456 File Offset: 0x00006656
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x0000845E File Offset: 0x0000665E
		[DataMember]
		public int NewGroupOrderNum { get; set; }
	}
}
