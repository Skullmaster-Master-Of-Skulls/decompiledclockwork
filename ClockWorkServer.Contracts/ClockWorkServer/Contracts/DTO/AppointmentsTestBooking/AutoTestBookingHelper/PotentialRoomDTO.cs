using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000AA1 RID: 2721
	[DataContract(Namespace = "http://tpro.ca")]
	public class PotentialRoomDTO
	{
		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x06003990 RID: 14736 RVA: 0x0001BF3C File Offset: 0x0001A13C
		// (set) Token: 0x06003991 RID: 14737 RVA: 0x0001BF44 File Offset: 0x0001A144
		[DataMember]
		public DateTime AvailabilityStartTimeForTheDay { get; set; }

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x0001BF4D File Offset: 0x0001A14D
		// (set) Token: 0x06003993 RID: 14739 RVA: 0x0001BF55 File Offset: 0x0001A155
		[DataMember]
		public DateTime AvailabilityEndTimeForTheDay { get; set; }

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x0001BF5E File Offset: 0x0001A15E
		// (set) Token: 0x06003995 RID: 14741 RVA: 0x0001BF66 File Offset: 0x0001A166
		[DataMember]
		public RoomDTO Room { get; set; }

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x0001BF6F File Offset: 0x0001A16F
		// (set) Token: 0x06003997 RID: 14743 RVA: 0x0001BF77 File Offset: 0x0001A177
		[DataMember]
		public int Score { get; set; }
	}
}
