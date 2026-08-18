using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters
{
	// Token: 0x020003DD RID: 989
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupByTitleReq : BaseMessageReq
	{
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0000A26F File Offset: 0x0000846F
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x0000A277 File Offset: 0x00008477
		[DataMember]
		public string GroupTitle { get; set; }

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0000A280 File Offset: 0x00008480
		// (set) Token: 0x060015C1 RID: 5569 RVA: 0x0000A288 File Offset: 0x00008488
		[DataMember]
		public string AlternateGroupTitle { get; set; }
	}
}
