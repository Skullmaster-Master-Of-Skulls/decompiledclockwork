using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000549 RID: 1353
	internal class ImageGalleryPagerItem : RadControlPagerItem
	{
		// Token: 0x06002FE7 RID: 12263 RVA: 0x0009D5BC File Offset: 0x0009B7BC
		public ImageGalleryPagerItem(RadImageGallery gallery)
		{
			this.Gallery = gallery;
			this.PagerStyle = this.Gallery.PagerStyle;
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x0009D5DC File Offset: 0x0009B7DC
		protected override RadControlPagerItemProperties RequestRequriedProperties()
		{
			this.Gallery.ThumbnailsArea.EnsureChildControlsCreated();
			return new RadControlPagerItemProperties
			{
				Control = this.Gallery,
				PagerStyle = new RadControlPagerStyle
				{
					Prefix = "rig",
					Mode = TreeListPagerMode.NumericPages,
					PagerTextFormat = this.PagerStyle.PagerTextFormat,
					AlwaysVisible = this.PagerStyle.AlwaysVisible,
					PageButtonCount = this.PagerStyle.PageButtonCount,
					ShowPagerText = this.PagerStyle.ShowPagerText
				},
				PagingSettings = new RadControlPagingSettings
				{
					CurrentPageIndex = this.Gallery.CurrentPageIndex,
					DataSourceCount = this.Gallery.ThumbnailListView.DataSourceCount,
					PageSize = this.Gallery.PageSize
				}
			};
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x0009D6B4 File Offset: 0x0009B8B4
		protected override void PagingPropertyChanged(string name, int value)
		{
			if (name != null)
			{
				if (!(name == "Page"))
				{
					return;
				}
				new ImageGalleryPageIndexChangedEventArgs(value).ExecuteCommand(this.Gallery);
			}
		}

		// Token: 0x04000CE2 RID: 3298
		private readonly RadImageGallery Gallery;

		// Token: 0x04000CE3 RID: 3299
		private readonly ImageGalleryPagerStyle PagerStyle;
	}
}
