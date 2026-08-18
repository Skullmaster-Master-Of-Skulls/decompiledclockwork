using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A61 RID: 2657
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateBookingNoteReq : BaseMessageReq
	{
		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x0001B161 File Offset: 0x00019361
		// (set) Token: 0x060037BD RID: 14269 RVA: 0x0001B169 File Offset: 0x00019369
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x0001B172 File Offset: 0x00019372
		// (set) Token: 0x060037BF RID: 14271 RVA: 0x0001B17A File Offset: 0x0001937A
		[DataMember]
		public string BookingNote { get; set; }
	}
}
