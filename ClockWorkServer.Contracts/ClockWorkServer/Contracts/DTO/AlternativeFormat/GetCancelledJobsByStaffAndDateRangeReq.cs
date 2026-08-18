using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BD4 RID: 3028
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCancelledJobsByStaffAndDateRangeReq : BaseMessageReq
	{
		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x0001F647 File Offset: 0x0001D847
		// (set) Token: 0x06003FE1 RID: 16353 RVA: 0x0001F64F File Offset: 0x0001D84F
		[DataMember]
		public int AssignedStaffId { get; set; }

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x0001F658 File Offset: 0x0001D858
		// (set) Token: 0x06003FE3 RID: 16355 RVA: 0x0001F660 File Offset: 0x0001D860
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x0001F669 File Offset: 0x0001D869
		// (set) Token: 0x06003FE5 RID: 16357 RVA: 0x0001F671 File Offset: 0x0001D871
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x0001F67A File Offset: 0x0001D87A
		// (set) Token: 0x06003FE7 RID: 16359 RVA: 0x0001F682 File Offset: 0x0001D882
		[DataMember]
		public int CampusId { get; set; }
	}
}
