using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BB4 RID: 2996
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveMediaJobsByExpiredInLessThanReq : BaseMessageReq
	{
		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x06003F5E RID: 16222 RVA: 0x0001F306 File Offset: 0x0001D506
		// (set) Token: 0x06003F5F RID: 16223 RVA: 0x0001F30E File Offset: 0x0001D50E
		[DataMember]
		public TimeSpan DueDateIn { get; set; }
	}
}
