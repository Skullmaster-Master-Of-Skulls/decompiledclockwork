using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x020002FD RID: 765
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeReportOrderInSameReportGroupResp
	{
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00008412 File Offset: 0x00006612
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x0000841A File Offset: 0x0000661A
		[DataMember]
		public int NewReportOrderNum { get; set; }
	}
}
