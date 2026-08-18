using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent
{
	// Token: 0x02000B41 RID: 2881
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChannelUnderlyingPersonDTO
	{
		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x0001D73E File Offset: 0x0001B93E
		// (set) Token: 0x06003CB6 RID: 15542 RVA: 0x0001D746 File Offset: 0x0001B946
		[DataMember]
		public int PersonId { get; set; }
	}
}
