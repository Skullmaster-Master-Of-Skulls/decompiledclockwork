using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020003F9 RID: 1017
	public class ImageGalleryCommandEventArgs : CommandEventArgs
	{
		// Token: 0x0600254B RID: 9547 RVA: 0x0007C237 File Offset: 0x0007A437
		public ImageGalleryCommandEventArgs(string commandName, object commandArgument) : base(commandName, commandArgument)
		{
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x0007C241 File Offset: 0x0007A441
		// (set) Token: 0x0600254D RID: 9549 RVA: 0x0007C249 File Offset: 0x0007A449
		public virtual bool Canceled { get; set; }

		// Token: 0x0600254E RID: 9550 RVA: 0x0007C254 File Offset: 0x0007A454
		internal void ExecuteCommand(object source)
		{
			RadImageGallery radImageGallery = source as RadImageGallery;
			if (radImageGallery == null)
			{
				return;
			}
			this.ExecuteCommand(radImageGallery);
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x0007C273 File Offset: 0x0007A473
		public virtual void ExecuteCommand(RadImageGallery gallery)
		{
		}
	}
}
