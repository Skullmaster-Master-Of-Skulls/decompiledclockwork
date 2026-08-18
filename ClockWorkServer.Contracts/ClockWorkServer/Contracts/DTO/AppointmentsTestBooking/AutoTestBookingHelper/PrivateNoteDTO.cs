using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000AA4 RID: 2724
	[DataContract(Namespace = "http://tpro.ca")]
	public class PrivateNoteDTO
	{
		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x060039AB RID: 14763 RVA: 0x0001C008 File Offset: 0x0001A208
		// (set) Token: 0x060039AC RID: 14764 RVA: 0x0001C010 File Offset: 0x0001A210
		[DataMember]
		public string Note { get; set; }
	}
}
