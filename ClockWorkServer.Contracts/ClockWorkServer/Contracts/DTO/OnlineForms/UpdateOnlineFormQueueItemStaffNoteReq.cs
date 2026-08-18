using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000401 RID: 1025
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateOnlineFormQueueItemStaffNoteReq : BaseMessageReq
	{
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x0000A6C0 File Offset: 0x000088C0
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x0000A6C8 File Offset: 0x000088C8
		[DataMember]
		public int PeopleOnlineFormId { get; set; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0000A6D1 File Offset: 0x000088D1
		// (set) Token: 0x06001667 RID: 5735 RVA: 0x0000A6D9 File Offset: 0x000088D9
		[DataMember]
		public string NewStaffNote { get; set; }
	}
}
