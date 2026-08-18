using System;

namespace Telerik.Web.Apoc.Layout.Inline
{
	// Token: 0x020015D1 RID: 5585
	internal abstract class InlineArea : Area
	{
		// Token: 0x0600D9D0 RID: 55760 RVA: 0x002FC3DF File Offset: 0x002FA5DF
		public InlineArea(FontState fontState, int width, float red, float green, float blue) : base(fontState)
		{
			this.contentRectangleWidth = width;
			this.red = red;
			this.green = green;
			this.blue = blue;
		}

		// Token: 0x0600D9D1 RID: 55761 RVA: 0x002FC406 File Offset: 0x002FA606
		public float getBlue()
		{
			return this.blue;
		}

		// Token: 0x0600D9D2 RID: 55762 RVA: 0x002FC40E File Offset: 0x002FA60E
		public float getGreen()
		{
			return this.green;
		}

		// Token: 0x0600D9D3 RID: 55763 RVA: 0x002FC416 File Offset: 0x002FA616
		public float getRed()
		{
			return this.red;
		}

		// Token: 0x0600D9D4 RID: 55764 RVA: 0x002FC41E File Offset: 0x002FA61E
		public override void SetHeight(int height)
		{
			this.height = height;
		}

		// Token: 0x0600D9D5 RID: 55765 RVA: 0x002FC427 File Offset: 0x002FA627
		public override int GetHeight()
		{
			return this.height;
		}

		// Token: 0x0600D9D6 RID: 55766 RVA: 0x002FC42F File Offset: 0x002FA62F
		public virtual void setVerticalAlign(int align)
		{
			this.verticalAlign = align;
		}

		// Token: 0x0600D9D7 RID: 55767 RVA: 0x002FC438 File Offset: 0x002FA638
		public virtual int getVerticalAlign()
		{
			return this.verticalAlign;
		}

		// Token: 0x0600D9D8 RID: 55768 RVA: 0x002FC440 File Offset: 0x002FA640
		public void setYOffset(int yOffset)
		{
			this.yOffset = yOffset;
		}

		// Token: 0x0600D9D9 RID: 55769 RVA: 0x002FC449 File Offset: 0x002FA649
		public int getYOffset()
		{
			return this.yOffset;
		}

		// Token: 0x0600D9DA RID: 55770 RVA: 0x002FC451 File Offset: 0x002FA651
		public void setXOffset(int xOffset)
		{
			this.xOffset = xOffset;
		}

		// Token: 0x0600D9DB RID: 55771 RVA: 0x002FC45A File Offset: 0x002FA65A
		public virtual int getXOffset()
		{
			return this.xOffset;
		}

		// Token: 0x0600D9DC RID: 55772 RVA: 0x002FC462 File Offset: 0x002FA662
		public string getPageNumberID()
		{
			return this.pageNumberId;
		}

		// Token: 0x0600D9DD RID: 55773 RVA: 0x002FC46A File Offset: 0x002FA66A
		public void setUnderlined(bool ul)
		{
			this.underlined = ul;
		}

		// Token: 0x0600D9DE RID: 55774 RVA: 0x002FC473 File Offset: 0x002FA673
		public bool getUnderlined()
		{
			return this.underlined;
		}

		// Token: 0x0600D9DF RID: 55775 RVA: 0x002FC47B File Offset: 0x002FA67B
		public void setOverlined(bool ol)
		{
			this.overlined = ol;
		}

		// Token: 0x0600D9E0 RID: 55776 RVA: 0x002FC484 File Offset: 0x002FA684
		public bool getOverlined()
		{
			return this.overlined;
		}

		// Token: 0x0600D9E1 RID: 55777 RVA: 0x002FC48C File Offset: 0x002FA68C
		public void setLineThrough(bool lt)
		{
			this.lineThrough = lt;
		}

		// Token: 0x0600D9E2 RID: 55778 RVA: 0x002FC495 File Offset: 0x002FA695
		public bool getLineThrough()
		{
			return this.lineThrough;
		}

		// Token: 0x04003C41 RID: 15425
		private int yOffset;

		// Token: 0x04003C42 RID: 15426
		private int xOffset;

		// Token: 0x04003C43 RID: 15427
		protected int height;

		// Token: 0x04003C44 RID: 15428
		private int verticalAlign;

		// Token: 0x04003C45 RID: 15429
		protected string pageNumberId;

		// Token: 0x04003C46 RID: 15430
		private float red;

		// Token: 0x04003C47 RID: 15431
		private float green;

		// Token: 0x04003C48 RID: 15432
		private float blue;

		// Token: 0x04003C49 RID: 15433
		protected bool underlined;

		// Token: 0x04003C4A RID: 15434
		protected bool overlined;

		// Token: 0x04003C4B RID: 15435
		protected bool lineThrough;
	}
}
