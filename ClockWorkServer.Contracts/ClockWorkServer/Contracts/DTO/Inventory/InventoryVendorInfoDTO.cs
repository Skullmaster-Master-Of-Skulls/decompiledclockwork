using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000573 RID: 1395
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryVendorInfoDTO
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0000D2C1 File Offset: 0x0000B4C1
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x0000D2C9 File Offset: 0x0000B4C9
		[DataMember]
		public string VendorName { get; set; }

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0000D2D2 File Offset: 0x0000B4D2
		// (set) Token: 0x06001CD3 RID: 7379 RVA: 0x0000D2DA File Offset: 0x0000B4DA
		[DataMember]
		public DateTime? PurchaseDate { get; set; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0000D2E3 File Offset: 0x0000B4E3
		// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x0000D2EB File Offset: 0x0000B4EB
		[DataMember]
		public double PurchaseAmount { get; set; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		[DataMember]
		public DateTime? WarrantyExpDate { get; set; }

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x0000D305 File Offset: 0x0000B505
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x0000D30D File Offset: 0x0000B50D
		[DataMember]
		public string PurchaseInfo { get; set; }
	}
}
