using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BD2 RID: 3026
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedJobsByStaffAndDateRangeReq : BaseMessageReq
	{
		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x0001F5F2 File Offset: 0x0001D7F2
		// (set) Token: 0x06003FD5 RID: 16341 RVA: 0x0001F5FA File Offset: 0x0001D7FA
		[DataMember]
		public int AssignedStaffId { get; set; }

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x0001F603 File Offset: 0x0001D803
		// (set) Token: 0x06003FD7 RID: 16343 RVA: 0x0001F60B File Offset: 0x0001D80B
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x0001F614 File Offset: 0x0001D814
		// (set) Token: 0x06003FD9 RID: 16345 RVA: 0x0001F61C File Offset: 0x0001D81C
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x0001F625 File Offset: 0x0001D825
		// (set) Token: 0x06003FDB RID: 16347 RVA: 0x0001F62D File Offset: 0x0001D82D
		[DataMember]
		public int CampusId { get; set; }
	}
}
