using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005A7 RID: 1447
	[DataContract(Namespace = "http://tpro.ca")]
	public class ProductBarcodeAlreadyExistsResp
	{
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x0000DA95 File Offset: 0x0000BC95
		// (set) Token: 0x06001DED RID: 7661 RVA: 0x0000DA9D File Offset: 0x0000BC9D
		[DataMember]
		public bool BarcodeExists { get; set; }
	}
}
