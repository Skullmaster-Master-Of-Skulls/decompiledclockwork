using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000527 RID: 1319
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateProductGroupReq : BaseMessageReq
	{
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0000CB26 File Offset: 0x0000AD26
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x0000CB2E File Offset: 0x0000AD2E
		[DataMember]
		public InventoryGroupDTO Group { get; set; }
	}
}
