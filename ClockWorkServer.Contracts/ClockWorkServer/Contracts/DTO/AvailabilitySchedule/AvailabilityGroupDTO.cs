using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008B9 RID: 2233
	[DataContract(Namespace = "http://tpro.ca")]
	public class AvailabilityGroupDTO
	{
		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x000155FD File Offset: 0x000137FD
		// (set) Token: 0x06002D2E RID: 11566 RVA: 0x00015605 File Offset: 0x00013805
		[DataMember]
		public int AvailabilityGroupId { get; set; }

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x0001560E File Offset: 0x0001380E
		// (set) Token: 0x06002D30 RID: 11568 RVA: 0x00015616 File Offset: 0x00013816
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x0001561F File Offset: 0x0001381F
		// (set) Token: 0x06002D32 RID: 11570 RVA: 0x00015627 File Offset: 0x00013827
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x00015630 File Offset: 0x00013830
		// (set) Token: 0x06002D34 RID: 11572 RVA: 0x00015638 File Offset: 0x00013838
		[DataMember]
		public int ColourArgB { get; set; }

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x00015641 File Offset: 0x00013841
		// (set) Token: 0x06002D36 RID: 11574 RVA: 0x00015649 File Offset: 0x00013849
		[DataMember]
		public int Pattern { get; set; }
	}
}
