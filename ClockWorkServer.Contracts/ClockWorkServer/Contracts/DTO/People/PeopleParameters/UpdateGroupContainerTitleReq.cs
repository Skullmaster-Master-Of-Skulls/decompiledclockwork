using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003D0 RID: 976
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateGroupContainerTitleReq : BaseMessageReq
	{
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x0000A1A3 File Offset: 0x000083A3
		// (set) Token: 0x0600159A RID: 5530 RVA: 0x0000A1AB File Offset: 0x000083AB
		[DataMember]
		public string OldContainerTitle { get; set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0000A1B4 File Offset: 0x000083B4
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x0000A1BC File Offset: 0x000083BC
		[DataMember]
		public string NewContainerTitle { get; set; }
	}
}
