using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A9B RID: 2715
	[DataContract(Namespace = "http://tpro.ca")]
	public class BookingDTO
	{
		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x0600392E RID: 14638 RVA: 0x0001BC14 File Offset: 0x00019E14
		// (set) Token: 0x0600392F RID: 14639 RVA: 0x0001BC1C File Offset: 0x00019E1C
		[DataMember]
		public int Pid { get; set; }

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x06003930 RID: 14640 RVA: 0x0001BC25 File Offset: 0x00019E25
		// (set) Token: 0x06003931 RID: 14641 RVA: 0x0001BC2D File Offset: 0x00019E2D
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06003932 RID: 14642 RVA: 0x0001BC36 File Offset: 0x00019E36
		// (set) Token: 0x06003933 RID: 14643 RVA: 0x0001BC3E File Offset: 0x00019E3E
		[DataMember]
		public DateTime EndDateTime { get; set; }
	}
}
