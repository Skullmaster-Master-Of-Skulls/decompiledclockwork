using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003FD RID: 1021
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadOnlineFormQueueItemsReq : BaseMessageReq
	{
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0000A616 File Offset: 0x00008816
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x0000A61E File Offset: 0x0000881E
		[DataMember]
		public int OnlineFormId { get; set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0000A627 File Offset: 0x00008827
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x0000A62F File Offset: 0x0000882F
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0000A638 File Offset: 0x00008838
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x0000A640 File Offset: 0x00008840
		[DataMember]
		public DateTime? EndDate { get; set; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0000A649 File Offset: 0x00008849
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x0000A651 File Offset: 0x00008851
		[DataMember]
		public int FilterByAssignedCounsellorPid { get; set; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0000A65A File Offset: 0x0000885A
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x0000A662 File Offset: 0x00008862
		[DataMember]
		public eOnlineFormStatusType[] OnlineFormTypesToExclude { get; set; }
	}
}
