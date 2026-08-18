using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005BF RID: 1471
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNextReservationAfterDateByProductResp
	{
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		// (set) Token: 0x06001E53 RID: 7763 RVA: 0x0000DD34 File Offset: 0x0000BF34
		[DataMember]
		public InventoryReservationDTO Reservation { get; set; }
	}
}
