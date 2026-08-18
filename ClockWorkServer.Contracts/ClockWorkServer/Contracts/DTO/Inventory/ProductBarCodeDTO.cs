using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005CE RID: 1486
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(Bitmap))]
	public class ProductBarCodeDTO
	{
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x0000DDF8 File Offset: 0x0000BFF8
		// (set) Token: 0x06001E7A RID: 7802 RVA: 0x0000DE00 File Offset: 0x0000C000
		[DataMember]
		public string BarCodeId { get; set; }

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x0000DE09 File Offset: 0x0000C009
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x0000DE11 File Offset: 0x0000C011
		[DataMember]
		public Image BarCodeImage { get; set; }

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0000DE1A File Offset: 0x0000C01A
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x0000DE22 File Offset: 0x0000C022
		[DataMember]
		public string BarCodeDescription { get; set; }
	}
}
