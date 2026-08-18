using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BC4 RID: 3012
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedJobsByDateRangeReq : BaseMessageReq
	{
		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x0001F449 File Offset: 0x0001D649
		// (set) Token: 0x06003F95 RID: 16277 RVA: 0x0001F451 File Offset: 0x0001D651
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06003F96 RID: 16278 RVA: 0x0001F45A File Offset: 0x0001D65A
		// (set) Token: 0x06003F97 RID: 16279 RVA: 0x0001F462 File Offset: 0x0001D662
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x0001F46B File Offset: 0x0001D66B
		// (set) Token: 0x06003F99 RID: 16281 RVA: 0x0001F473 File Offset: 0x0001D673
		[DataMember]
		public int CampusId { get; set; }
	}
}
