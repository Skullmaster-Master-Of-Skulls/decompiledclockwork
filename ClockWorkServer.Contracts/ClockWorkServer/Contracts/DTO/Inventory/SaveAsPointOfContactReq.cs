using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005A4 RID: 1444
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveAsPointOfContactReq : BaseMsmqMessageReq
	{
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0000DA48 File Offset: 0x0000BC48
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x0000DA50 File Offset: 0x0000BC50
		[DataMember]
		public InventoryProductSnapshotDTO ProductSnapshot { get; set; }
	}
}
