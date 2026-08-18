using System;

namespace Telerik.Web.UI
{
	// Token: 0x020003FA RID: 1018
	public class ImageGalleryChangeItemIndexEventArgs : ImageGalleryCommandEventArgs
	{
		// Token: 0x06002550 RID: 9552 RVA: 0x0007C275 File Offset: 0x0007A475
		public ImageGalleryChangeItemIndexEventArgs(object commandArgument) : base("ChangeItemIndex", commandArgument)
		{
			this.newItemIndex = int.Parse(commandArgument.ToString());
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x0007C294 File Offset: 0x0007A494
		public int NewItemIndex
		{
			get
			{
				return this.newItemIndex;
			}
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x0007C29C File Offset: 0x0007A49C
		public override void ExecuteCommand(RadImageGallery gallery)
		{
			gallery.CurrentItemIndex = this.NewItemIndex;
			gallery.ActiveItemIndex = this.NewItemIndex;
			gallery.Rebind();
		}

		// Token: 0x0400097E RID: 2430
		private int newItemIndex;
	}
}
