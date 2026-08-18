using System;

namespace System.Drawing.Printing
{
	// Token: 0x0200005D RID: 93
	public sealed class PreviewPageInfo
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x0001E037 File Offset: 0x0001C237
		public PreviewPageInfo(Image image, Size physicalSize)
		{
			this.image = image;
			this.physicalSize = physicalSize;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001E058 File Offset: 0x0001C258
		public Image Image
		{
			get
			{
				return this.image;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001E060 File Offset: 0x0001C260
		public Size PhysicalSize
		{
			get
			{
				return this.physicalSize;
			}
		}

		// Token: 0x040006B5 RID: 1717
		private Image image;

		// Token: 0x040006B6 RID: 1718
		private Size physicalSize = Size.Empty;
	}
}
