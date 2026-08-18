using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent
{
	// Token: 0x02000B3E RID: 2878
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChannelAvailabilityDTO
	{
		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06003C86 RID: 15494 RVA: 0x0001D5C8 File Offset: 0x0001B7C8
		// (set) Token: 0x06003C87 RID: 15495 RVA: 0x0001D5D0 File Offset: 0x0001B7D0
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06003C88 RID: 15496 RVA: 0x0001D5D9 File Offset: 0x0001B7D9
		// (set) Token: 0x06003C89 RID: 15497 RVA: 0x0001D5E1 File Offset: 0x0001B7E1
		[DataMember]
		public int AvailabilityGroupId { get; set; }

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06003C8A RID: 15498 RVA: 0x0001D5EA File Offset: 0x0001B7EA
		// (set) Token: 0x06003C8B RID: 15499 RVA: 0x0001D5F2 File Offset: 0x0001B7F2
		[DataMember]
		public int AppTypeIdToBookWith { get; set; }

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06003C8C RID: 15500 RVA: 0x0001D5FB File Offset: 0x0001B7FB
		// (set) Token: 0x06003C8D RID: 15501 RVA: 0x0001D603 File Offset: 0x0001B803
		[DataMember]
		public int PreBookScreenNum { get; set; }

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06003C8E RID: 15502 RVA: 0x0001D60C File Offset: 0x0001B80C
		// (set) Token: 0x06003C8F RID: 15503 RVA: 0x0001D614 File Offset: 0x0001B814
		[DataMember]
		public int SlotSizeInMinutes { get; set; }

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06003C90 RID: 15504 RVA: 0x0001D61D File Offset: 0x0001B81D
		// (set) Token: 0x06003C91 RID: 15505 RVA: 0x0001D625 File Offset: 0x0001B825
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x0001D62E File Offset: 0x0001B82E
		// (set) Token: 0x06003C93 RID: 15507 RVA: 0x0001D636 File Offset: 0x0001B836
		[DataMember]
		public IList<ChannelPersonCollectionDTO> PersonCollection { get; set; }

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06003C94 RID: 15508 RVA: 0x0001D63F File Offset: 0x0001B83F
		// (set) Token: 0x06003C95 RID: 15509 RVA: 0x0001D647 File Offset: 0x0001B847
		[DataMember]
		public bool UseAssignedAdvisorInsteadOfPersonCollection { get; set; }

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06003C96 RID: 15510 RVA: 0x0001D650 File Offset: 0x0001B850
		// (set) Token: 0x06003C97 RID: 15511 RVA: 0x0001D658 File Offset: 0x0001B858
		[DataMember]
		public int[] UseAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids { get; set; }
	}
}
