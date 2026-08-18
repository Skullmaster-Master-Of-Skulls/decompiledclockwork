using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200030B RID: 779
	public class InventoryAttachedFile : BusinessBase<int>
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0001D238 File Offset: 0x0001B438
		// (set) Token: 0x06001829 RID: 6185 RVA: 0x0001D240 File Offset: 0x0001B440
		public InventoryAttachedFileInfo AttachedFileInfo { get; set; }

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0001D249 File Offset: 0x0001B449
		// (set) Token: 0x0600182B RID: 6187 RVA: 0x0001D251 File Offset: 0x0001B451
		public byte[] BinaryData { get; set; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x0001D25C File Offset: 0x0001B45C
		// (set) Token: 0x0600182D RID: 6189 RVA: 0x0001D279 File Offset: 0x0001B479
		public override int Id
		{
			get
			{
				return this.AttachedFileInfo.Id;
			}
			set
			{
				this.AttachedFileInfo.Id = value;
			}
		}
	}
}
