using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004ED RID: 1261
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryAttachedFileDTO
	{
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0000C691 File Offset: 0x0000A891
		// (set) Token: 0x06001ADD RID: 6877 RVA: 0x0000C699 File Offset: 0x0000A899
		[DataMember]
		public InventoryAttachedFileInfoDTO AttachedFileInfo { get; set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x0000C6A2 File Offset: 0x0000A8A2
		// (set) Token: 0x06001ADF RID: 6879 RVA: 0x0000C6AA File Offset: 0x0000A8AA
		[DataMember]
		public byte[] BinaryData { get; set; }
	}
}
