using System;
using Telerik.Web.UI.ImageEditor;

namespace Telerik.Web.UI
{
	// Token: 0x02000BA9 RID: 2985
	public class ImageEditorEventArgs : EventArgs
	{
		// Token: 0x170024C8 RID: 9416
		// (get) Token: 0x06007081 RID: 28801 RVA: 0x001A4307 File Offset: 0x001A2507
		// (set) Token: 0x06007082 RID: 28802 RVA: 0x001A430F File Offset: 0x001A250F
		public EditableImage Image { get; private set; }

		// Token: 0x06007083 RID: 28803 RVA: 0x001A4318 File Offset: 0x001A2518
		public ImageEditorEventArgs(EditableImage image)
		{
			this.Image = image;
		}
	}
}
