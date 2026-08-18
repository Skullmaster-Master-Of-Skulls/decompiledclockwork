using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent
{
	// Token: 0x02000B3F RID: 2879
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChannelDTO
	{
		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06003C99 RID: 15513 RVA: 0x0001D661 File Offset: 0x0001B861
		// (set) Token: 0x06003C9A RID: 15514 RVA: 0x0001D669 File Offset: 0x0001B869
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06003C9B RID: 15515 RVA: 0x0001D672 File Offset: 0x0001B872
		// (set) Token: 0x06003C9C RID: 15516 RVA: 0x0001D67A File Offset: 0x0001B87A
		[DataMember]
		public string Id { get; set; }

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06003C9D RID: 15517 RVA: 0x0001D683 File Offset: 0x0001B883
		// (set) Token: 0x06003C9E RID: 15518 RVA: 0x0001D68B File Offset: 0x0001B88B
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x0001D694 File Offset: 0x0001B894
		// (set) Token: 0x06003CA0 RID: 15520 RVA: 0x0001D69C File Offset: 0x0001B89C
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x0001D6A5 File Offset: 0x0001B8A5
		// (set) Token: 0x06003CA2 RID: 15522 RVA: 0x0001D6AD File Offset: 0x0001B8AD
		[DataMember]
		public IList<ChannelAvailabilityDTO> Availabilities { get; set; }

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x0001D6B6 File Offset: 0x0001B8B6
		// (set) Token: 0x06003CA4 RID: 15524 RVA: 0x0001D6BE File Offset: 0x0001B8BE
		[DataMember]
		public AppointmentBookingFilterParametersDTO OverrideBookingFilterParameters { get; set; }

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x0001D6C7 File Offset: 0x0001B8C7
		// (set) Token: 0x06003CA6 RID: 15526 RVA: 0x0001D6CF File Offset: 0x0001B8CF
		[DataMember]
		public int OrderNum { get; set; }
	}
}
