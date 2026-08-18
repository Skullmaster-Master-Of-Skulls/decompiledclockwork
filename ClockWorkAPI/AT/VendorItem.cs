using System;

namespace ClockWorkAPI.AT
{
	// Token: 0x0200007E RID: 126
	public class VendorItem
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x000241BC File Offset: 0x000231BC
		public VendorItem()
		{
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000241CE File Offset: 0x000231CE
		public VendorItem(Item item, Vendor vendor)
		{
			this.item = item;
			this.vendor = vendor;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000241F0 File Offset: 0x000231F0
		public override string ToString()
		{
			return this.item.Title + ": " + this.vendor.Title;
		}

		// Token: 0x04000344 RID: 836
		private ObjectStatus status = ObjectStatus.Unknown;

		// Token: 0x04000345 RID: 837
		private Item item;

		// Token: 0x04000346 RID: 838
		private Vendor vendor;
	}
}
