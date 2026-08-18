using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent
{
	// Token: 0x02000B40 RID: 2880
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChannelPersonCollectionDTO
	{
		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06003CA8 RID: 15528 RVA: 0x0001D6D8 File Offset: 0x0001B8D8
		// (set) Token: 0x06003CA9 RID: 15529 RVA: 0x0001D6E0 File Offset: 0x0001B8E0
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06003CAA RID: 15530 RVA: 0x0001D6E9 File Offset: 0x0001B8E9
		// (set) Token: 0x06003CAB RID: 15531 RVA: 0x0001D6F1 File Offset: 0x0001B8F1
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x0001D6FA File Offset: 0x0001B8FA
		// (set) Token: 0x06003CAD RID: 15533 RVA: 0x0001D702 File Offset: 0x0001B902
		[DataMember]
		public string Id { get; set; }

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06003CAE RID: 15534 RVA: 0x0001D70B File Offset: 0x0001B90B
		// (set) Token: 0x06003CAF RID: 15535 RVA: 0x0001D713 File Offset: 0x0001B913
		[DataMember]
		public IList<ChannelUnderlyingPersonDTO> UnderlyingPeople { get; set; }

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06003CB0 RID: 15536 RVA: 0x0001D71C File Offset: 0x0001B91C
		// (set) Token: 0x06003CB1 RID: 15537 RVA: 0x0001D724 File Offset: 0x0001B924
		[DataMember]
		public int? ColourArgB { get; set; }

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x0001D72D File Offset: 0x0001B92D
		// (set) Token: 0x06003CB3 RID: 15539 RVA: 0x0001D735 File Offset: 0x0001B935
		[DataMember]
		public SchoolCampusDTO Campus { get; set; }
	}
}
