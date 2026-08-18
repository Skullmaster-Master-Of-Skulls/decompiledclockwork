using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000537 RID: 1335
	public abstract class ImageGallerySettings : StateManager
	{
		// Token: 0x06002F40 RID: 12096 RVA: 0x0009A92D File Offset: 0x00098B2D
		public ImageGallerySettings(RadImageGallery gallery)
		{
			this.Gallery = gallery;
		}

		// Token: 0x04000CB2 RID: 3250
		protected readonly RadImageGallery Gallery;
	}
}
