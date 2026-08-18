using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005AD RID: 1453
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductStatusByIdResp
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x0000DAEA File Offset: 0x0000BCEA
		// (set) Token: 0x06001DFD RID: 7677 RVA: 0x0000DAF2 File Offset: 0x0000BCF2
		[DataMember]
		public InventoryProductStatusDTO ProductStatus { get; set; }
	}
}
