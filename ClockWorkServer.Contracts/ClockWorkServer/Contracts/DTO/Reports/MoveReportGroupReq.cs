using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000302 RID: 770
	[DataContract(Namespace = "http://tpro.ca")]
	public class MoveReportGroupReq : BaseReportMessageReq
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x000084BC File Offset: 0x000066BC
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x000084C4 File Offset: 0x000066C4
		[DataMember]
		public int ReportGroupIdToMove { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x000084CD File Offset: 0x000066CD
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x000084D5 File Offset: 0x000066D5
		[DataMember]
		public int NewReportParentGroupId { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x000084DE File Offset: 0x000066DE
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x000084E6 File Offset: 0x000066E6
		[DataMember]
		public int? ReportGroupIdToMoveBeforeOrAfter { get; set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x000084EF File Offset: 0x000066EF
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x000084F7 File Offset: 0x000066F7
		[DataMember]
		public bool MoveAfter { get; set; }
	}
}
