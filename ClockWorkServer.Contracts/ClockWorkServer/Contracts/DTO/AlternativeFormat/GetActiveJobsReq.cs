using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BB8 RID: 3000
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveJobsReq : BaseMessageReq
	{
		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x0001F339 File Offset: 0x0001D539
		// (set) Token: 0x06003F69 RID: 16233 RVA: 0x0001F341 File Offset: 0x0001D541
		[DataMember]
		public int CampusId { get; set; }
	}
}
