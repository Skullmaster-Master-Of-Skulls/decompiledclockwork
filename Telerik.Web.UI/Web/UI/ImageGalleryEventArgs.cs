using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000531 RID: 1329
	public class ImageGalleryEventArgs : EventArgs
	{
		// Token: 0x06002F25 RID: 12069 RVA: 0x0009A6F8 File Offset: 0x000988F8
		internal void ExecuteCommand(object source)
		{
			RadImageGallery radImageGallery = source as RadImageGallery;
			if (radImageGallery == null)
			{
				return;
			}
			this.ExecuteCommand(radImageGallery);
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x0009A717 File Offset: 0x00098917
		public virtual void ExecuteCommand(RadImageGallery gallery)
		{
		}
	}
}
