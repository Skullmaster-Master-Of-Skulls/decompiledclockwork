using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200036D RID: 877
	[DataContract(Namespace = "http://tpro.ca")]
	public class GroupOrGroupContainerForEditDTO
	{
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0000973B File Offset: 0x0000793B
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x00009743 File Offset: 0x00007943
		[DataMember]
		public GroupForEditDTO Group { get; set; }

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0000974C File Offset: 0x0000794C
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x00009754 File Offset: 0x00007954
		[DataMember]
		public GroupContainerForEditDTO GroupContainer { get; set; }
	}
}
