using System;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x0200056C RID: 1388
	public class AppointmentBookingReq
	{
		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x00031B79 File Offset: 0x0002FD79
		// (set) Token: 0x06002CBE RID: 11454 RVA: 0x00031B81 File Offset: 0x0002FD81
		public int StudentPersonId { get; set; }

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x00031B8A File Offset: 0x0002FD8A
		// (set) Token: 0x06002CC0 RID: 11456 RVA: 0x00031B92 File Offset: 0x0002FD92
		public int StaffPersonId { get; set; }

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x00031B9B File Offset: 0x0002FD9B
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x00031BA3 File Offset: 0x0002FDA3
		public DateTime StartDateTime { get; set; }

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x00031BAC File Offset: 0x0002FDAC
		// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x00031BB4 File Offset: 0x0002FDB4
		public DateTime EndDateTime { get; set; }

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x00031BBD File Offset: 0x0002FDBD
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x00031BC5 File Offset: 0x0002FDC5
		public int AppTypeId { get; set; }

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x00031BCE File Offset: 0x0002FDCE
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x00031BD6 File Offset: 0x0002FDD6
		public bool IsTentative { get; set; }

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x00031BDF File Offset: 0x0002FDDF
		// (set) Token: 0x06002CCA RID: 11466 RVA: 0x00031BE7 File Offset: 0x0002FDE7
		public string MemoRtf { get; set; }

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x00031BF0 File Offset: 0x0002FDF0
		// (set) Token: 0x06002CCC RID: 11468 RVA: 0x00031BF8 File Offset: 0x0002FDF8
		public string Location { get; set; }

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x00031C01 File Offset: 0x0002FE01
		// (set) Token: 0x06002CCE RID: 11470 RVA: 0x00031C09 File Offset: 0x0002FE09
		public string Subject { get; set; }
	}
}
