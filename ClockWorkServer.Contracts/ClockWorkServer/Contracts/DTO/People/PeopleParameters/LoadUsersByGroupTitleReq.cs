using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003E7 RID: 999
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUsersByGroupTitleReq : BaseMessageReq
	{
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x0000A308 File Offset: 0x00008508
		// (set) Token: 0x060015DB RID: 5595 RVA: 0x0000A310 File Offset: 0x00008510
		[DataMember]
		public string GroupTitle { get; set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0000A319 File Offset: 0x00008519
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x0000A321 File Offset: 0x00008521
		[DataMember]
		public string AlternateGroupTitle { get; set; }
	}
}
