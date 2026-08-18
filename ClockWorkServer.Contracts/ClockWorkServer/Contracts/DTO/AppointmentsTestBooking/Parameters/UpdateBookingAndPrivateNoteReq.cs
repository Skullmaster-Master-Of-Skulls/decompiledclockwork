using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A63 RID: 2659
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateBookingAndPrivateNoteReq : BaseMessageReq
	{
		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x0001B1A5 File Offset: 0x000193A5
		// (set) Token: 0x060037C7 RID: 14279 RVA: 0x0001B1AD File Offset: 0x000193AD
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x0001B1B6 File Offset: 0x000193B6
		// (set) Token: 0x060037C9 RID: 14281 RVA: 0x0001B1BE File Offset: 0x000193BE
		[DataMember]
		public string BookingNote { get; set; }

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x0001B1C7 File Offset: 0x000193C7
		// (set) Token: 0x060037CB RID: 14283 RVA: 0x0001B1CF File Offset: 0x000193CF
		[DataMember]
		public string PrivateNote { get; set; }
	}
}
