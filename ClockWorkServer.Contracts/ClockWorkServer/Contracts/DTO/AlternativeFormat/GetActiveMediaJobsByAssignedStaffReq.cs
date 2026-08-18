using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BB2 RID: 2994
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveMediaJobsByAssignedStaffReq : BaseMessageReq
	{
		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x0001F2D3 File Offset: 0x0001D4D3
		// (set) Token: 0x06003F57 RID: 16215 RVA: 0x0001F2DB File Offset: 0x0001D4DB
		[DataMember]
		public int AssignedStaffId { get; set; }

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x0001F2E4 File Offset: 0x0001D4E4
		// (set) Token: 0x06003F59 RID: 16217 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		[DataMember]
		public int CampusId { get; set; }
	}
}
