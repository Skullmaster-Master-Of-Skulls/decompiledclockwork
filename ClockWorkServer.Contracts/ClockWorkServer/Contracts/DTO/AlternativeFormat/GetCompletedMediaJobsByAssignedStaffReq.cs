using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BC2 RID: 3010
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedMediaJobsByAssignedStaffReq : BaseMessageReq
	{
		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x0001F416 File Offset: 0x0001D616
		// (set) Token: 0x06003F8D RID: 16269 RVA: 0x0001F41E File Offset: 0x0001D61E
		[DataMember]
		public int AssignedStaffId { get; set; }

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x06003F8E RID: 16270 RVA: 0x0001F427 File Offset: 0x0001D627
		// (set) Token: 0x06003F8F RID: 16271 RVA: 0x0001F42F File Offset: 0x0001D62F
		[DataMember]
		public int CampusId { get; set; }
	}
}
