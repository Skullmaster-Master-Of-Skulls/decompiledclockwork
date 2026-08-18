using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000AA3 RID: 2723
	[DataContract(Namespace = "http://tpro.ca")]
	public class PotentialTestMethodFoundNoteDTO
	{
		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x0001BFF7 File Offset: 0x0001A1F7
		// (set) Token: 0x060039A9 RID: 14761 RVA: 0x0001BFFF File Offset: 0x0001A1FF
		[DataMember]
		public string Note { get; set; }
	}
}
