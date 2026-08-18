using System;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout.Inline
{
	// Token: 0x020015EE RID: 5614
	internal class InlineSpace : Space
	{
		// Token: 0x0600DAC8 RID: 56008 RVA: 0x002FDF46 File Offset: 0x002FC146
		public InlineSpace(int amount)
		{
			this.size = amount;
		}

		// Token: 0x0600DAC9 RID: 56009 RVA: 0x002FDF5C File Offset: 0x002FC15C
		public InlineSpace(int amount, bool resizeable)
		{
			this.resizeable = resizeable;
			this.size = amount;
		}

		// Token: 0x0600DACA RID: 56010 RVA: 0x002FDF79 File Offset: 0x002FC179
		public void setUnderlined(bool ul)
		{
			this.underlined = ul;
		}

		// Token: 0x0600DACB RID: 56011 RVA: 0x002FDF82 File Offset: 0x002FC182
		public bool getUnderlined()
		{
			return this.underlined;
		}

		// Token: 0x0600DACC RID: 56012 RVA: 0x002FDF8A File Offset: 0x002FC18A
		public void setOverlined(bool ol)
		{
			this.overlined = ol;
		}

		// Token: 0x0600DACD RID: 56013 RVA: 0x002FDF93 File Offset: 0x002FC193
		public bool getOverlined()
		{
			return this.overlined;
		}

		// Token: 0x0600DACE RID: 56014 RVA: 0x002FDF9B File Offset: 0x002FC19B
		public void setLineThrough(bool lt)
		{
			this.lineThrough = lt;
		}

		// Token: 0x0600DACF RID: 56015 RVA: 0x002FDFA4 File Offset: 0x002FC1A4
		public bool getLineThrough()
		{
			return this.lineThrough;
		}

		// Token: 0x0600DAD0 RID: 56016 RVA: 0x002FDFAC File Offset: 0x002FC1AC
		public int getSize()
		{
			return this.size;
		}

		// Token: 0x0600DAD1 RID: 56017 RVA: 0x002FDFB4 File Offset: 0x002FC1B4
		public void setSize(int amount)
		{
			this.size = amount;
		}

		// Token: 0x0600DAD2 RID: 56018 RVA: 0x002FDFBD File Offset: 0x002FC1BD
		public bool getResizeable()
		{
			return this.resizeable;
		}

		// Token: 0x0600DAD3 RID: 56019 RVA: 0x002FDFC5 File Offset: 0x002FC1C5
		public void setResizeable(bool resizeable)
		{
			this.resizeable = resizeable;
		}

		// Token: 0x0600DAD4 RID: 56020 RVA: 0x002FDFCE File Offset: 0x002FC1CE
		public void setEatable(bool eatable)
		{
			this.eatable = eatable;
		}

		// Token: 0x0600DAD5 RID: 56021 RVA: 0x002FDFD7 File Offset: 0x002FC1D7
		public bool isEatable()
		{
			return this.eatable;
		}

		// Token: 0x0600DAD6 RID: 56022 RVA: 0x002FDFDF File Offset: 0x002FC1DF
		public override void render(IRenderer renderer)
		{
			renderer.RenderInlineSpace(this);
		}

		// Token: 0x04003CF1 RID: 15601
		private int size;

		// Token: 0x04003CF2 RID: 15602
		private bool resizeable = true;

		// Token: 0x04003CF3 RID: 15603
		private bool eatable;

		// Token: 0x04003CF4 RID: 15604
		protected bool underlined;

		// Token: 0x04003CF5 RID: 15605
		protected bool overlined;

		// Token: 0x04003CF6 RID: 15606
		protected bool lineThrough;
	}
}
