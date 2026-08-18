using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000533 RID: 1331
	public class ImageGalleryItemEventArgs : ImageGalleryEventArgs
	{
		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x0009A77B File Offset: 0x0009897B
		// (set) Token: 0x06002F32 RID: 12082 RVA: 0x0009A783 File Offset: 0x00098983
		public ImageGalleryItemBase Item { get; protected set; }

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x0009A78C File Offset: 0x0009898C
		// (set) Token: 0x06002F34 RID: 12084 RVA: 0x0009A794 File Offset: 0x00098994
		public RadListViewDataItem ListViewItem { get; protected set; }

		// Token: 0x06002F35 RID: 12085 RVA: 0x0009A79D File Offset: 0x0009899D
		public ImageGalleryItemEventArgs(ImageGalleryItemBase item, RadListViewDataItem listViewItem)
		{
			this.Item = item;
			this.ListViewItem = listViewItem;
		}
	}
}
