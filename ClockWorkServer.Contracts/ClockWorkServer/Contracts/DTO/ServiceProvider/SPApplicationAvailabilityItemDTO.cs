using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x0200026E RID: 622
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPApplicationAvailabilityItemDTO
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00006C4F File Offset: 0x00004E4F
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x00006C57 File Offset: 0x00004E57
		[DataMember]
		public int SPApplicationAvailabilityitemId { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00006C60 File Offset: 0x00004E60
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00006C68 File Offset: 0x00004E68
		[DataMember]
		public SPApplicationDTO Application { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00006C71 File Offset: 0x00004E71
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x00006C79 File Offset: 0x00004E79
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00006C82 File Offset: 0x00004E82
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00006C8A File Offset: 0x00004E8A
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00006C93 File Offset: 0x00004E93
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x00006C9B File Offset: 0x00004E9B
		[DataMember]
		public string Note { get; set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00006CA4 File Offset: 0x00004EA4
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x00006CAC File Offset: 0x00004EAC
		[DataMember]
		public string Location { get; set; }
	}
}
