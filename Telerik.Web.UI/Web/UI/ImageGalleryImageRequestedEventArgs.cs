using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000532 RID: 1330
	public class ImageGalleryImageRequestedEventArgs : ImageGalleryEventArgs
	{
		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x0009A721 File Offset: 0x00098921
		// (set) Token: 0x06002F29 RID: 12073 RVA: 0x0009A729 File Offset: 0x00098929
		public ImageGalleryItemBase Item { get; set; }

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x0009A732 File Offset: 0x00098932
		// (set) Token: 0x06002F2B RID: 12075 RVA: 0x0009A73A File Offset: 0x0009893A
		public Hashtable KeyValues { get; set; }

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x0009A743 File Offset: 0x00098943
		// (set) Token: 0x06002F2D RID: 12077 RVA: 0x0009A74B File Offset: 0x0009894B
		public byte[] ImageData { get; set; }

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x0009A754 File Offset: 0x00098954
		// (set) Token: 0x06002F2F RID: 12079 RVA: 0x0009A75C File Offset: 0x0009895C
		public string ImageUrl { get; set; }

		// Token: 0x06002F30 RID: 12080 RVA: 0x0009A765 File Offset: 0x00098965
		public ImageGalleryImageRequestedEventArgs(ImageGalleryItemBase item, Hashtable keyValues)
		{
			this.Item = item;
			this.KeyValues = keyValues;
		}
	}
}
