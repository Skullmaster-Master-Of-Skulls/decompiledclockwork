using System;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200030C RID: 780
	public class InventoryAttachedFileInfo : BusinessBase<int>
	{
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x0001D289 File Offset: 0x0001B489
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x0001D291 File Offset: 0x0001B491
		public string Name { get; set; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0001D29A File Offset: 0x0001B49A
		// (set) Token: 0x06001832 RID: 6194 RVA: 0x0001D2A2 File Offset: 0x0001B4A2
		public int SizeInBytes { get; set; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0001D2AB File Offset: 0x0001B4AB
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x0001D2B3 File Offset: 0x0001B4B3
		public DateTime CreatedDatetime { get; set; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x0001D2C4 File Offset: 0x0001B4C4
		public string Notes { get; set; }
	}
}
