using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000536 RID: 1334
	public class ImageGalleryPageSizeChangedEventArgs : ImageGalleryCommandEventArgs
	{
		// Token: 0x06002F3C RID: 12092 RVA: 0x0009A8C2 File Offset: 0x00098AC2
		public ImageGalleryPageSizeChangedEventArgs(object commandArgument) : base("ChangePageSize", commandArgument)
		{
			this.NewPageSize = int.Parse(commandArgument.ToString());
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x0009A8E1 File Offset: 0x00098AE1
		// (set) Token: 0x06002F3E RID: 12094 RVA: 0x0009A8E9 File Offset: 0x00098AE9
		public int NewPageSize { get; private set; }

		// Token: 0x06002F3F RID: 12095 RVA: 0x0009A8F2 File Offset: 0x00098AF2
		public override void ExecuteCommand(RadImageGallery gallery)
		{
			gallery.CallOnCommand(this);
			if (this.Canceled)
			{
				return;
			}
			gallery.CallOnPageSizeChanged(this);
			if (this.Canceled)
			{
				return;
			}
			gallery.CurrentItemIndex = 0;
			gallery.PageSize = this.NewPageSize;
			gallery.Rebind();
		}
	}
}
