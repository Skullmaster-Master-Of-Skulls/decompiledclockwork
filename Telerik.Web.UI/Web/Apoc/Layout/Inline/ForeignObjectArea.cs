using System;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout.Inline
{
	// Token: 0x020015ED RID: 5613
	internal class ForeignObjectArea : InlineArea
	{
		// Token: 0x0600DAA9 RID: 55977 RVA: 0x002FDDDC File Offset: 0x002FBFDC
		public ForeignObjectArea(FontState fontState, int width) : base(fontState, width, 0f, 0f, 0f)
		{
		}

		// Token: 0x0600DAAA RID: 55978 RVA: 0x002FDDF5 File Offset: 0x002FBFF5
		public override void render(IRenderer renderer)
		{
			if (this.foreignObject != null)
			{
				renderer.RenderForeignObjectArea(this);
			}
		}

		// Token: 0x0600DAAB RID: 55979 RVA: 0x002FDE06 File Offset: 0x002FC006
		public override int getContentWidth()
		{
			return this.getEffectiveWidth();
		}

		// Token: 0x0600DAAC RID: 55980 RVA: 0x002FDE0E File Offset: 0x002FC00E
		public override int GetHeight()
		{
			return this.getEffectiveHeight();
		}

		// Token: 0x0600DAAD RID: 55981 RVA: 0x002FDE16 File Offset: 0x002FC016
		public override int getContentHeight()
		{
			return this.getEffectiveHeight();
		}

		// Token: 0x0600DAAE RID: 55982 RVA: 0x002FDE1E File Offset: 0x002FC01E
		public override int getXOffset()
		{
			return this.xOffset;
		}

		// Token: 0x0600DAAF RID: 55983 RVA: 0x002FDE26 File Offset: 0x002FC026
		public void setStartIndent(int startIndent)
		{
			this.xOffset = startIndent;
		}

		// Token: 0x0600DAB0 RID: 55984 RVA: 0x002FDE2F File Offset: 0x002FC02F
		public void setObject(Area fobject)
		{
			this.foreignObject = fobject;
		}

		// Token: 0x0600DAB1 RID: 55985 RVA: 0x002FDE38 File Offset: 0x002FC038
		public Area getObject()
		{
			return this.foreignObject;
		}

		// Token: 0x0600DAB2 RID: 55986 RVA: 0x002FDE40 File Offset: 0x002FC040
		public void setSizeAuto(bool wa, bool ha)
		{
			this.wauto = wa;
			this.hauto = ha;
		}

		// Token: 0x0600DAB3 RID: 55987 RVA: 0x002FDE50 File Offset: 0x002FC050
		public void setContentSizeAuto(bool wa, bool ha)
		{
			this.cwauto = wa;
			this.chauto = ha;
		}

		// Token: 0x0600DAB4 RID: 55988 RVA: 0x002FDE60 File Offset: 0x002FC060
		public bool isContentWidthAuto()
		{
			return this.cwauto;
		}

		// Token: 0x0600DAB5 RID: 55989 RVA: 0x002FDE68 File Offset: 0x002FC068
		public bool isContentHeightAuto()
		{
			return this.chauto;
		}

		// Token: 0x0600DAB6 RID: 55990 RVA: 0x002FDE70 File Offset: 0x002FC070
		public void setAlign(int align)
		{
			this.align = align;
		}

		// Token: 0x0600DAB7 RID: 55991 RVA: 0x002FDE79 File Offset: 0x002FC079
		public int getAlign()
		{
			return this.align;
		}

		// Token: 0x0600DAB8 RID: 55992 RVA: 0x002FDE81 File Offset: 0x002FC081
		public override void setVerticalAlign(int align)
		{
			this.valign = align;
		}

		// Token: 0x0600DAB9 RID: 55993 RVA: 0x002FDE8A File Offset: 0x002FC08A
		public override int getVerticalAlign()
		{
			return this.valign;
		}

		// Token: 0x0600DABA RID: 55994 RVA: 0x002FDE92 File Offset: 0x002FC092
		public void setOverflow(int o)
		{
			this.overflow = o;
		}

		// Token: 0x0600DABB RID: 55995 RVA: 0x002FDE9B File Offset: 0x002FC09B
		public int getOverflow()
		{
			return this.overflow;
		}

		// Token: 0x0600DABC RID: 55996 RVA: 0x002FDEA3 File Offset: 0x002FC0A3
		public override void SetHeight(int height)
		{
			this.height = height;
		}

		// Token: 0x0600DABD RID: 55997 RVA: 0x002FDEAC File Offset: 0x002FC0AC
		public void SetWidth(int width)
		{
			this.width = width;
		}

		// Token: 0x0600DABE RID: 55998 RVA: 0x002FDEB5 File Offset: 0x002FC0B5
		public void setContentHeight(int cheight)
		{
			this.cheight = cheight;
		}

		// Token: 0x0600DABF RID: 55999 RVA: 0x002FDEBE File Offset: 0x002FC0BE
		public void SetContentWidth(int cwidth)
		{
			this.cwidth = cwidth;
		}

		// Token: 0x0600DAC0 RID: 56000 RVA: 0x002FDEC7 File Offset: 0x002FC0C7
		public void setScaling(int scaling)
		{
			this.scaling = scaling;
		}

		// Token: 0x0600DAC1 RID: 56001 RVA: 0x002FDED0 File Offset: 0x002FC0D0
		public int scalingMethod()
		{
			return this.scaling;
		}

		// Token: 0x0600DAC2 RID: 56002 RVA: 0x002FDED8 File Offset: 0x002FC0D8
		public void setIntrinsicWidth(int w)
		{
			this.awidth = w;
		}

		// Token: 0x0600DAC3 RID: 56003 RVA: 0x002FDEE1 File Offset: 0x002FC0E1
		public void setIntrinsicHeight(int h)
		{
			this.aheight = h;
		}

		// Token: 0x0600DAC4 RID: 56004 RVA: 0x002FDEEA File Offset: 0x002FC0EA
		public int getIntrinsicHeight()
		{
			return this.aheight;
		}

		// Token: 0x0600DAC5 RID: 56005 RVA: 0x002FDEF2 File Offset: 0x002FC0F2
		public int getIntrinsicWidth()
		{
			return this.awidth;
		}

		// Token: 0x0600DAC6 RID: 56006 RVA: 0x002FDEFA File Offset: 0x002FC0FA
		public int getEffectiveHeight()
		{
			if (!this.hauto)
			{
				return this.height;
			}
			if (this.chauto)
			{
				return this.aheight;
			}
			return this.cheight;
		}

		// Token: 0x0600DAC7 RID: 56007 RVA: 0x002FDF20 File Offset: 0x002FC120
		public int getEffectiveWidth()
		{
			if (!this.wauto)
			{
				return this.width;
			}
			if (this.cwauto)
			{
				return this.awidth;
			}
			return this.cwidth;
		}

		// Token: 0x04003CE2 RID: 15586
		protected int xOffset;

		// Token: 0x04003CE3 RID: 15587
		protected int align;

		// Token: 0x04003CE4 RID: 15588
		protected int valign;

		// Token: 0x04003CE5 RID: 15589
		protected int scaling;

		// Token: 0x04003CE6 RID: 15590
		protected Area foreignObject;

		// Token: 0x04003CE7 RID: 15591
		protected int cheight;

		// Token: 0x04003CE8 RID: 15592
		protected int cwidth;

		// Token: 0x04003CE9 RID: 15593
		protected int awidth;

		// Token: 0x04003CEA RID: 15594
		protected int aheight;

		// Token: 0x04003CEB RID: 15595
		protected int width;

		// Token: 0x04003CEC RID: 15596
		private bool wauto;

		// Token: 0x04003CED RID: 15597
		private bool hauto;

		// Token: 0x04003CEE RID: 15598
		private bool cwauto;

		// Token: 0x04003CEF RID: 15599
		private bool chauto;

		// Token: 0x04003CF0 RID: 15600
		private int overflow;
	}
}
