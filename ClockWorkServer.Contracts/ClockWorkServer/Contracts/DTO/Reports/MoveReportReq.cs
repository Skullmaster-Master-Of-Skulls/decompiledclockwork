using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000300 RID: 768
	[DataContract(Namespace = "http://tpro.ca")]
	public class MoveReportReq : BaseReportMessageReq
	{
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00008467 File Offset: 0x00006667
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x0000846F File Offset: 0x0000666F
		[DataMember]
		public int ReportIdToMove { get; set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00008478 File Offset: 0x00006678
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00008480 File Offset: 0x00006680
		[DataMember]
		public int NewReportParentGroupId { get; set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00008489 File Offset: 0x00006689
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x00008491 File Offset: 0x00006691
		[DataMember]
		public int? ReportIdToMoveBeforeOrAfter { get; set; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0000849A File Offset: 0x0000669A
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x000084A2 File Offset: 0x000066A2
		[DataMember]
		public bool MoveAfter { get; set; }
	}
}
