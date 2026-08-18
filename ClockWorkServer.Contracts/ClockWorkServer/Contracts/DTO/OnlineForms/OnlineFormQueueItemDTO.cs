using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000411 RID: 1041
	[DataContract(Namespace = "http://tpro.ca")]
	public class OnlineFormQueueItemDTO
	{
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0000A803 File Offset: 0x00008A03
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x0000A80B File Offset: 0x00008A0B
		[DataMember]
		public int PeopleOnlineFormId { get; set; }

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0000A814 File Offset: 0x00008A14
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x0000A81C File Offset: 0x00008A1C
		[DataMember]
		public BasicPersonDTO Student { get; set; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x0000A825 File Offset: 0x00008A25
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x0000A82D File Offset: 0x00008A2D
		[DataMember]
		public BasicPersonDTO AssignedCounsellor { get; set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0000A836 File Offset: 0x00008A36
		// (set) Token: 0x060016A1 RID: 5793 RVA: 0x0000A83E File Offset: 0x00008A3E
		[DataMember]
		public OnlineFormForDisplayDTO OnlineForm { get; set; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0000A847 File Offset: 0x00008A47
		// (set) Token: 0x060016A3 RID: 5795 RVA: 0x0000A84F File Offset: 0x00008A4F
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x0000A858 File Offset: 0x00008A58
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x0000A860 File Offset: 0x00008A60
		[DataMember]
		public OnlineFormStatusDTO Status { get; set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0000A869 File Offset: 0x00008A69
		// (set) Token: 0x060016A7 RID: 5799 RVA: 0x0000A871 File Offset: 0x00008A71
		[DataMember]
		public string StudentEmail { get; set; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0000A87A File Offset: 0x00008A7A
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x0000A882 File Offset: 0x00008A82
		[DataMember]
		public string StaffNote { get; set; }
	}
}
