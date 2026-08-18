using System;
using Telerik.Web.Apoc.Layout;
using Telerik.Web.Apoc.Layout.Inline;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Image
{
	// Token: 0x020015D2 RID: 5586
	internal class ImageArea : InlineArea
	{
		// Token: 0x0600D9E3 RID: 55779 RVA: 0x002FC4A0 File Offset: 0x002FA6A0
		public ImageArea(FontState fontState, ApocImage img, int AllocationWidth, int width, int height, int startIndent, int endIndent, int align) : base(fontState, width, 0f, 0f, 0f)
		{
			this.currentHeight = height;
			this.contentRectangleWidth = width;
			this.height = height;
			this.image = img;
			this.align = align;
		}

		// Token: 0x0600D9E4 RID: 55780 RVA: 0x002FC4EC File Offset: 0x002FA6EC
		public override int getXOffset()
		{
			return this.xOffset;
		}

		// Token: 0x0600D9E5 RID: 55781 RVA: 0x002FC4F4 File Offset: 0x002FA6F4
		public ApocImage getImage()
		{
			return this.image;
		}

		// Token: 0x0600D9E6 RID: 55782 RVA: 0x002FC4FC File Offset: 0x002FA6FC
		public override void render(IRenderer renderer)
		{
			renderer.RenderImageArea(this);
		}

		// Token: 0x0600D9E7 RID: 55783 RVA: 0x002FC505 File Offset: 0x002FA705
		public int getImageHeight()
		{
			return this.currentHeight;
		}

		// Token: 0x0600D9E8 RID: 55784 RVA: 0x002FC50D File Offset: 0x002FA70D
		public void setAlign(int align)
		{
			this.align = align;
		}

		// Token: 0x0600D9E9 RID: 55785 RVA: 0x002FC516 File Offset: 0x002FA716
		public int getAlign()
		{
			return this.align;
		}

		// Token: 0x0600D9EA RID: 55786 RVA: 0x002FC51E File Offset: 0x002FA71E
		public override void setVerticalAlign(int align)
		{
			this.valign = align;
		}

		// Token: 0x0600D9EB RID: 55787 RVA: 0x002FC527 File Offset: 0x002FA727
		public override int getVerticalAlign()
		{
			return this.valign;
		}

		// Token: 0x0600D9EC RID: 55788 RVA: 0x002FC52F File Offset: 0x002FA72F
		public void setStartIndent(int startIndent)
		{
			this.xOffset = startIndent;
		}

		// Token: 0x04003C4C RID: 15436
		protected int xOffset;

		// Token: 0x04003C4D RID: 15437
		protected int align;

		// Token: 0x04003C4E RID: 15438
		protected int valign;

		// Token: 0x04003C4F RID: 15439
		protected ApocImage image;
	}
}
