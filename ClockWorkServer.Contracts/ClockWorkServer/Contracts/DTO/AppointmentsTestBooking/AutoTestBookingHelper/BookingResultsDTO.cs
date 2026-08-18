using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A9C RID: 2716
	[DataContract(Namespace = "http://tpro.ca")]
	public class BookingResultsDTO
	{
		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06003935 RID: 14645 RVA: 0x0001BC47 File Offset: 0x00019E47
		// (set) Token: 0x06003936 RID: 14646 RVA: 0x0001BC4F File Offset: 0x00019E4F
		[DataMember]
		public bool? NoRoomAvailability { get; set; }

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x06003937 RID: 14647 RVA: 0x0001BC58 File Offset: 0x00019E58
		// (set) Token: 0x06003938 RID: 14648 RVA: 0x0001BC60 File Offset: 0x00019E60
		[DataMember]
		public bool? OnlyVirtualRoomsToLookAt { get; set; }

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x0001BC69 File Offset: 0x00019E69
		// (set) Token: 0x0600393A RID: 14650 RVA: 0x0001BC71 File Offset: 0x00019E71
		[DataMember]
		public bool? FailedTimetableCheck { get; set; }

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x0001BC7A File Offset: 0x00019E7A
		// (set) Token: 0x0600393C RID: 14652 RVA: 0x0001BC82 File Offset: 0x00019E82
		[DataMember]
		public bool? StudentIsDoubleBooked { get; set; }

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x0600393D RID: 14653 RVA: 0x0001BC8B File Offset: 0x00019E8B
		// (set) Token: 0x0600393E RID: 14654 RVA: 0x0001BC93 File Offset: 0x00019E93
		[DataMember]
		public bool? RoomIsDoubleBooked { get; set; }
	}
}
