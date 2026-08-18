using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000596 RID: 1430
	[DataContract(Namespace = "http://tpro.ca")]
	public class AssignProductToGroupReq : BaseMessageReq
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0000D899 File Offset: 0x0000BA99
		// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x0000D8A1 File Offset: 0x0000BAA1
		[DataMember]
		public int WorkingCatalogId { get; set; }

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0000D8AA File Offset: 0x0000BAAA
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x0000D8B2 File Offset: 0x0000BAB2
		[DataMember]
		public string ProductUniqueId { get; set; }

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x0000D8BB File Offset: 0x0000BABB
		// (set) Token: 0x06001DA8 RID: 7592 RVA: 0x0000D8C3 File Offset: 0x0000BAC3
		[DataMember]
		public int GroupId { get; set; }
	}
}
