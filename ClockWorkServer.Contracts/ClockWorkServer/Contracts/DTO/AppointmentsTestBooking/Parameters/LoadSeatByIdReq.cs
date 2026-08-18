using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A85 RID: 2693
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSeatByIdReq : BaseMessageReq
	{
		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x0001B57F File Offset: 0x0001977F
		// (set) Token: 0x0600385D RID: 14429 RVA: 0x0001B587 File Offset: 0x00019787
		[DataMember]
		public int RoomId { get; set; }
	}
}
