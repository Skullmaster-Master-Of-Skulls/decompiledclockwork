using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x020002FC RID: 764
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeReportOrderInSameReportGroupReq : BaseReportMessageReq
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x000083DF File Offset: 0x000065DF
		// (set) Token: 0x06001194 RID: 4500 RVA: 0x000083E7 File Offset: 0x000065E7
		[DataMember]
		public int ReportIdToMove { get; set; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x000083F0 File Offset: 0x000065F0
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x000083F8 File Offset: 0x000065F8
		[DataMember]
		public int ReportIdToMoveBeforeOrAfter { get; set; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00008401 File Offset: 0x00006601
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x00008409 File Offset: 0x00006609
		[DataMember]
		public bool moveAfter { get; set; }
	}
}
