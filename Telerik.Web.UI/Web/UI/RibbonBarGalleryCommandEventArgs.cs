using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200077F RID: 1919
	public class RibbonBarGalleryCommandEventArgs : EventArgs
	{
		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x060043B1 RID: 17329 RVA: 0x000D3A52 File Offset: 0x000D1C52
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x060043B2 RID: 17330 RVA: 0x000D3A5A File Offset: 0x000D1C5A
		public RibbonBarGallery Gallery
		{
			get
			{
				return this._gallery;
			}
		}

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x000D3A62 File Offset: 0x000D1C62
		public RibbonBarGalleryCategory Category
		{
			get
			{
				return this._category;
			}
		}

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x060043B4 RID: 17332 RVA: 0x000D3A6A File Offset: 0x000D1C6A
		public RibbonBarGalleryItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x000D3A72 File Offset: 0x000D1C72
		public RibbonBarGalleryCommandEventArgs(RibbonBarGalleryItem item, RibbonBarGalleryCategory category, RibbonBarGallery gallery, RibbonBarGroup group)
		{
			this._item = item;
			this._category = category;
			this._gallery = gallery;
			this._group = group;
		}

		// Token: 0x040011EA RID: 4586
		private RibbonBarGroup _group;

		// Token: 0x040011EB RID: 4587
		private RibbonBarGallery _gallery;

		// Token: 0x040011EC RID: 4588
		private RibbonBarGalleryCategory _category;

		// Token: 0x040011ED RID: 4589
		private RibbonBarGalleryItem _item;
	}
}
