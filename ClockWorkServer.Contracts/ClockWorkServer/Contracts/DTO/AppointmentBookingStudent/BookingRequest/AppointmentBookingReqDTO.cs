using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x02000B44 RID: 2884
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentBookingReqDTO
	{
		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06003CD8 RID: 15576 RVA: 0x0001D84E File Offset: 0x0001BA4E
		// (set) Token: 0x06003CD9 RID: 15577 RVA: 0x0001D856 File Offset: 0x0001BA56
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06003CDA RID: 15578 RVA: 0x0001D85F File Offset: 0x0001BA5F
		// (set) Token: 0x06003CDB RID: 15579 RVA: 0x0001D867 File Offset: 0x0001BA67
		[DataMember]
		public int StaffPersonId { get; set; }

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06003CDC RID: 15580 RVA: 0x0001D870 File Offset: 0x0001BA70
		// (set) Token: 0x06003CDD RID: 15581 RVA: 0x0001D878 File Offset: 0x0001BA78
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06003CDE RID: 15582 RVA: 0x0001D881 File Offset: 0x0001BA81
		// (set) Token: 0x06003CDF RID: 15583 RVA: 0x0001D889 File Offset: 0x0001BA89
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x0001D892 File Offset: 0x0001BA92
		// (set) Token: 0x06003CE1 RID: 15585 RVA: 0x0001D89A File Offset: 0x0001BA9A
		[DataMember]
		public int AppTypeId { get; set; }

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06003CE2 RID: 15586 RVA: 0x0001D8A3 File Offset: 0x0001BAA3
		// (set) Token: 0x06003CE3 RID: 15587 RVA: 0x0001D8AB File Offset: 0x0001BAAB
		[DataMember]
		public bool IsTentative { get; set; }

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x0001D8B4 File Offset: 0x0001BAB4
		// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x0001D8BC File Offset: 0x0001BABC
		[DataMember]
		public string MemoRtf { get; set; }

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x0001D8C5 File Offset: 0x0001BAC5
		// (set) Token: 0x06003CE7 RID: 15591 RVA: 0x0001D8CD File Offset: 0x0001BACD
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x0001D8D6 File Offset: 0x0001BAD6
		// (set) Token: 0x06003CE9 RID: 15593 RVA: 0x0001D8DE File Offset: 0x0001BADE
		[DataMember]
		public string Subject { get; set; }
	}
}
