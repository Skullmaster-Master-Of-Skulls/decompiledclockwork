using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005C0 RID: 1472
	[DataContract(Namespace = "http://tpro.ca")]
	public class MakeReservationReq : BaseMessageReq
	{
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x0000DD3D File Offset: 0x0000BF3D
		// (set) Token: 0x06001E56 RID: 7766 RVA: 0x0000DD45 File Offset: 0x0000BF45
		[DataMember]
		public InventoryReservationGroupDTO ReservationGroup { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0000DD4E File Offset: 0x0000BF4E
		// (set) Token: 0x06001E58 RID: 7768 RVA: 0x0000DD56 File Offset: 0x0000BF56
		[DataMember]
		public IList<string> ReservedProductUniqueIds { get; set; }
	}
}
