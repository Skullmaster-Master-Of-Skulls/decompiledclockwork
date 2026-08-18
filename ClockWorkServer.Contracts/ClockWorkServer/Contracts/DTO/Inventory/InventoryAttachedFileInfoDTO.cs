using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004EE RID: 1262
	[DataContract(Namespace = "http://tpro.ca")]
	public class InventoryAttachedFileInfoDTO
	{
		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x0000C6B3 File Offset: 0x0000A8B3
		// (set) Token: 0x06001AE2 RID: 6882 RVA: 0x0000C6BB File Offset: 0x0000A8BB
		[DataMember]
		public int Id { get; set; }

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		// (set) Token: 0x06001AE4 RID: 6884 RVA: 0x0000C6CC File Offset: 0x0000A8CC
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0000C6D5 File Offset: 0x0000A8D5
		// (set) Token: 0x06001AE6 RID: 6886 RVA: 0x0000C6DD File Offset: 0x0000A8DD
		[DataMember]
		public int SizeInBytes { get; set; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0000C6E6 File Offset: 0x0000A8E6
		// (set) Token: 0x06001AE8 RID: 6888 RVA: 0x0000C6EE File Offset: 0x0000A8EE
		[DataMember]
		public DateTime CreatedDatetime { get; set; }

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x0000C6F7 File Offset: 0x0000A8F7
		// (set) Token: 0x06001AEA RID: 6890 RVA: 0x0000C6FF File Offset: 0x0000A8FF
		[DataMember]
		public string Notes { get; set; }
	}
}
