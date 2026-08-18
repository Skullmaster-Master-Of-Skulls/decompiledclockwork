using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x020002FE RID: 766
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeReportGroupOrderInSameReportGroupReq : BaseReportMessageReq
	{
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x00008423 File Offset: 0x00006623
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x0000842B File Offset: 0x0000662B
		[DataMember]
		public int ReportGroupIdToMove { get; set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00008434 File Offset: 0x00006634
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x0000843C File Offset: 0x0000663C
		[DataMember]
		public int ReportGroupIdToMoveBeforeOrAfter { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00008445 File Offset: 0x00006645
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x0000844D File Offset: 0x0000664D
		[DataMember]
		public bool MoveAfter { get; set; }
	}
}
