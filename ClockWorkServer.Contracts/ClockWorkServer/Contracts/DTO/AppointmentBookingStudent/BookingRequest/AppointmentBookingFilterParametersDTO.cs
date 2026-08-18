using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x02000B43 RID: 2883
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentBookingFilterParametersDTO
	{
		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x0001D782 File Offset: 0x0001B982
		// (set) Token: 0x06003CC0 RID: 15552 RVA: 0x0001D78A File Offset: 0x0001B98A
		[DataMember]
		public int MaxNumberOfAppointmentsPerWeek { get; set; }

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x0001D793 File Offset: 0x0001B993
		// (set) Token: 0x06003CC2 RID: 15554 RVA: 0x0001D79B File Offset: 0x0001B99B
		[DataMember]
		public int[] MaxNumberOfAppointmentsPerWeekAppTypeIds { get; set; }

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x0001D7A4 File Offset: 0x0001B9A4
		// (set) Token: 0x06003CC4 RID: 15556 RVA: 0x0001D7AC File Offset: 0x0001B9AC
		[DataMember]
		public int MaxNumberOfAppointmentsPerDay { get; set; }

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x0001D7B5 File Offset: 0x0001B9B5
		// (set) Token: 0x06003CC6 RID: 15558 RVA: 0x0001D7BD File Offset: 0x0001B9BD
		[DataMember]
		public int[] MaxNumberOfAppointmentsPerDayAppTypeIds { get; set; }

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x0001D7C6 File Offset: 0x0001B9C6
		// (set) Token: 0x06003CC8 RID: 15560 RVA: 0x0001D7CE File Offset: 0x0001B9CE
		[DataMember]
		public CutoffTimeDTO CutoffTime { get; set; }

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x0001D7D7 File Offset: 0x0001B9D7
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x0001D7DF File Offset: 0x0001B9DF
		[DataMember]
		public int MaxNumberOfNoShows { get; set; }

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x0001D7E8 File Offset: 0x0001B9E8
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x0001D7F0 File Offset: 0x0001B9F0
		[DataMember]
		public int[] MaxNumberOfNoShowsAppTypeIds { get; set; }

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x0001D7F9 File Offset: 0x0001B9F9
		// (set) Token: 0x06003CCE RID: 15566 RVA: 0x0001D801 File Offset: 0x0001BA01
		[DataMember]
		public int MaxNumberOfAppointmentsInFuture { get; set; }

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x0001D80A File Offset: 0x0001BA0A
		// (set) Token: 0x06003CD0 RID: 15568 RVA: 0x0001D812 File Offset: 0x0001BA12
		[DataMember]
		public int[] MaxNumberOfAppointmentsInFutureAppTypeIds { get; set; }

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x0001D81B File Offset: 0x0001BA1B
		// (set) Token: 0x06003CD2 RID: 15570 RVA: 0x0001D823 File Offset: 0x0001BA23
		[DataMember]
		public bool AllowDoubleBookingStaff { get; set; }

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x0001D82C File Offset: 0x0001BA2C
		// (set) Token: 0x06003CD4 RID: 15572 RVA: 0x0001D834 File Offset: 0x0001BA34
		[DataMember]
		public bool AllowDoubleBookingStudent { get; set; }

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06003CD5 RID: 15573 RVA: 0x0001D83D File Offset: 0x0001BA3D
		// (set) Token: 0x06003CD6 RID: 15574 RVA: 0x0001D845 File Offset: 0x0001BA45
		[DataMember]
		public int BannedExpiryDateCid { get; set; }
	}
}
