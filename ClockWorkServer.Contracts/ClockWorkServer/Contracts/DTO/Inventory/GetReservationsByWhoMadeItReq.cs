using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005BA RID: 1466
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsByWhoMadeItReq : BaseMessageReq
	{
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		// (set) Token: 0x06001E3E RID: 7742 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		[DataMember]
		public int WhoMadeReservationId { get; set; }
	}
}
