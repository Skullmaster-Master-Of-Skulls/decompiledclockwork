using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace System.Windows.Forms
{
	// Token: 0x020003D6 RID: 982
	public class ToolStripItemImageRenderEventArgs : ToolStripItemRenderEventArgs
	{
		// Token: 0x06004347 RID: 17223 RVA: 0x0011D150 File Offset: 0x0011B350
		public ToolStripItemImageRenderEventArgs(Graphics g, ToolStripItem item, Rectangle imageRectangle) : base(g, item)
		{
			this.image = ((item.RightToLeftAutoMirrorImage && item.RightToLeft == RightToLeft.Yes) ? item.MirroredImage : item.Image);
			this.imageRectangle = imageRectangle;
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x0011D19C File Offset: 0x0011B39C
		public ToolStripItemImageRenderEventArgs(Graphics g, ToolStripItem item, Image image, Rectangle imageRectangle) : base(g, item)
		{
			this.image = image;
			this.imageRectangle = imageRectangle;
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x0011D1C0 File Offset: 0x0011B3C0
		public Image Image
		{
			get
			{
				return this.image;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x0600434A RID: 17226 RVA: 0x0011D1C8 File Offset: 0x0011B3C8
		public Rectangle ImageRectangle
		{
			get
			{
				return this.imageRectangle;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x0011D1D0 File Offset: 0x0011B3D0
		// (set) Token: 0x0600434C RID: 17228 RVA: 0x0011D1D8 File Offset: 0x0011B3D8
		internal bool ShiftOnPress
		{
			get
			{
				return this.shiftOnPress;
			}
			set
			{
				this.shiftOnPress = value;
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x0600434D RID: 17229 RVA: 0x0011D1E1 File Offset: 0x0011B3E1
		// (set) Token: 0x0600434E RID: 17230 RVA: 0x0011D1E9 File Offset: 0x0011B3E9
		internal ImageAttributes ImageAttributes
		{
			get
			{
				return this.imageAttr;
			}
			set
			{
				this.imageAttr = value;
			}
		}

		// Token: 0x040025B7 RID: 9655
		private Image image;

		// Token: 0x040025B8 RID: 9656
		private Rectangle imageRectangle = Rectangle.Empty;

		// Token: 0x040025B9 RID: 9657
		private bool shiftOnPress;

		// Token: 0x040025BA RID: 9658
		private ImageAttributes imageAttr;
	}
}
