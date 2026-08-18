using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003FF RID: 1023
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateOnlineFormQueueItemStaffNoteAndStatusReq : BaseMessageReq
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0000A67C File Offset: 0x0000887C
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x0000A684 File Offset: 0x00008884
		[DataMember]
		public int PeopleOnlineFormId { get; set; }

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0000A68D File Offset: 0x0000888D
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x0000A695 File Offset: 0x00008895
		[DataMember]
		public int? NewPeopleOnlineFormStatusId { get; set; }

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0000A69E File Offset: 0x0000889E
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x0000A6A6 File Offset: 0x000088A6
		[DataMember]
		public string NewStaffNote { get; set; }
	}
}
