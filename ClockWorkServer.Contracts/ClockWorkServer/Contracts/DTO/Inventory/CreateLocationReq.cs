using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000563 RID: 1379
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateLocationReq : BaseMessageReq
	{
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0000D0B2 File Offset: 0x0000B2B2
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x0000D0BA File Offset: 0x0000B2BA
		[DataMember]
		public InventoryLocationDTO Location { get; set; }
	}
}
