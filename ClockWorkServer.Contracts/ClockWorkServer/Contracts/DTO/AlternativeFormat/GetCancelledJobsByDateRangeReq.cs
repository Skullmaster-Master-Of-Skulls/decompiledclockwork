using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BC6 RID: 3014
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCancelledJobsByDateRangeReq : BaseMessageReq
	{
		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06003F9E RID: 16286 RVA: 0x0001F48D File Offset: 0x0001D68D
		// (set) Token: 0x06003F9F RID: 16287 RVA: 0x0001F495 File Offset: 0x0001D695
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x0001F49E File Offset: 0x0001D69E
		// (set) Token: 0x06003FA1 RID: 16289 RVA: 0x0001F4A6 File Offset: 0x0001D6A6
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x0001F4AF File Offset: 0x0001D6AF
		// (set) Token: 0x06003FA3 RID: 16291 RVA: 0x0001F4B7 File Offset: 0x0001D6B7
		[DataMember]
		public int CampusId { get; set; }
	}
}
