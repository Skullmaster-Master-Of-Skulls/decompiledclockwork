using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005BC RID: 1468
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsByWhoMadeItInDateRangeReq : BaseMessageReq
	{
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x0000DCC6 File Offset: 0x0000BEC6
		// (set) Token: 0x06001E44 RID: 7748 RVA: 0x0000DCCE File Offset: 0x0000BECE
		[DataMember]
		public int WhoMadeReservationId { get; set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x0000DCD7 File Offset: 0x0000BED7
		// (set) Token: 0x06001E46 RID: 7750 RVA: 0x0000DCDF File Offset: 0x0000BEDF
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		// (set) Token: 0x06001E48 RID: 7752 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
