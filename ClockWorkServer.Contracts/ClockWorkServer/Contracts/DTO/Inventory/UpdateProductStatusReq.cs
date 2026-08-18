using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005AA RID: 1450
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateProductStatusReq : BaseMessageReq
	{
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		// (set) Token: 0x06001DF6 RID: 7670 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		[DataMember]
		public InventoryProductStatusDTO ProductStatus { get; set; }
	}
}
