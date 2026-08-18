using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005B8 RID: 1464
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsReq : BaseMessageReq
	{
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0000DC71 File Offset: 0x0000BE71
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x0000DC79 File Offset: 0x0000BE79
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0000DC82 File Offset: 0x0000BE82
		// (set) Token: 0x06001E38 RID: 7736 RVA: 0x0000DC8A File Offset: 0x0000BE8A
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
