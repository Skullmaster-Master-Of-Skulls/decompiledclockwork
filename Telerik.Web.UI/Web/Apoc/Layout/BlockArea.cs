using System;
using System.Collections;
using Telerik.Web.Apoc.Fo.Flow;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015DD RID: 5597
	internal class BlockArea : Area
	{
		// Token: 0x0600DA22 RID: 55842 RVA: 0x002FCBCC File Offset: 0x002FADCC
		public BlockArea(FontState fontState, int allocationWidth, int maxHeight, int startIndent, int endIndent, int textIndent, int align, int alignLastLine, int lineHeight) : base(fontState, allocationWidth, maxHeight)
		{
			this.startIndent = startIndent;
			this.endIndent = endIndent;
			this.textIndent = textIndent;
			this.contentRectangleWidth = allocationWidth - startIndent - endIndent;
			this.align = align;
			this.alignLastLine = alignLastLine;
			this.lineHeight = lineHeight;
			if (fontState != null)
			{
				this.halfLeading = (lineHeight - fontState.FontSize) / 2;
			}
		}

		// Token: 0x0600DA23 RID: 55843 RVA: 0x002FCC33 File Offset: 0x002FAE33
		public override void render(IRenderer renderer)
		{
			renderer.RenderBlockArea(this);
		}

		// Token: 0x0600DA24 RID: 55844 RVA: 0x002FCC3C File Offset: 0x002FAE3C
		protected void addLineArea(LineArea la)
		{
			if (!la.isEmpty())
			{
				la.verticalAlign();
				base.addDisplaySpace(this.halfLeading);
				int height = la.GetHeight();
				base.addChild(la);
				base.increaseHeight(height);
				base.addDisplaySpace(this.halfLeading);
			}
			if (this.pendingFootnotes != null)
			{
				foreach (object obj in this.pendingFootnotes)
				{
					FootnoteBody fb = (FootnoteBody)obj;
					Page page = base.getPage();
					if (!Footnote.LayoutFootnote(page, fb, this))
					{
						page.addPendingFootnote(fb);
					}
				}
				this.pendingFootnotes = null;
			}
		}

		// Token: 0x0600DA25 RID: 55845 RVA: 0x002FCCF4 File Offset: 0x002FAEF4
		public LineArea getCurrentLineArea()
		{
			if (this.currentHeight + this.lineHeight > this.maxHeight)
			{
				return null;
			}
			this.currentLineArea.changeHyphenation(this.hyphProps);
			this.hasLines = true;
			return this.currentLineArea;
		}

		// Token: 0x0600DA26 RID: 55846 RVA: 0x002FCD2C File Offset: 0x002FAF2C
		public LineArea createNextLineArea()
		{
			if (this.hasLines)
			{
				this.currentLineArea.align(this.align);
				this.addLineArea(this.currentLineArea);
			}
			this.currentLineArea = new LineArea(this.fontState, this.lineHeight, this.halfLeading, this.allocationWidth, this.startIndent, this.endIndent, this.currentLineArea);
			this.currentLineArea.changeHyphenation(this.hyphProps);
			if (this.currentHeight + this.lineHeight > this.maxHeight)
			{
				return null;
			}
			return this.currentLineArea;
		}

		// Token: 0x0600DA27 RID: 55847 RVA: 0x002FCDC1 File Offset: 0x002FAFC1
		public void setupLinkSet(LinkSet ls)
		{
			if (ls != null)
			{
				this.currentLinkSet = ls;
				ls.setYOffset(this.currentHeight);
			}
		}

		// Token: 0x0600DA28 RID: 55848 RVA: 0x002FCDD9 File Offset: 0x002FAFD9
		public override void end()
		{
			if (this.hasLines)
			{
				this.currentLineArea.addPending();
				this.currentLineArea.align(this.alignLastLine);
				this.addLineArea(this.currentLineArea);
			}
		}

		// Token: 0x0600DA29 RID: 55849 RVA: 0x002FCE0B File Offset: 0x002FB00B
		public override void start()
		{
			this.currentLineArea = new LineArea(this.fontState, this.lineHeight, this.halfLeading, this.allocationWidth, this.startIndent + this.textIndent, this.endIndent, null);
		}

		// Token: 0x0600DA2A RID: 55850 RVA: 0x002FCE44 File Offset: 0x002FB044
		public int getEndIndent()
		{
			return this.endIndent;
		}

		// Token: 0x0600DA2B RID: 55851 RVA: 0x002FCE4C File Offset: 0x002FB04C
		public int getStartIndent()
		{
			return this.startIndent;
		}

		// Token: 0x0600DA2C RID: 55852 RVA: 0x002FCE54 File Offset: 0x002FB054
		public void setIndents(int startIndent, int endIndent)
		{
			this.startIndent = startIndent;
			this.endIndent = endIndent;
			this.contentRectangleWidth = this.allocationWidth - startIndent - endIndent;
		}

		// Token: 0x0600DA2D RID: 55853 RVA: 0x002FCE74 File Offset: 0x002FB074
		public override int spaceLeft()
		{
			return this.maxHeight - this.currentHeight - (base.getPaddingTop() + base.getPaddingBottom() + base.getBorderTopWidth() + base.getBorderBottomWidth());
		}

		// Token: 0x0600DA2E RID: 55854 RVA: 0x002FCE9F File Offset: 0x002FB09F
		public int getHalfLeading()
		{
			return this.halfLeading;
		}

		// Token: 0x0600DA2F RID: 55855 RVA: 0x002FCEA7 File Offset: 0x002FB0A7
		public void setHyphenation(HyphenationProps hyphProps)
		{
			this.hyphProps = hyphProps;
		}

		// Token: 0x0600DA30 RID: 55856 RVA: 0x002FCEB0 File Offset: 0x002FB0B0
		public void addFootnote(FootnoteBody fb)
		{
			if (this.pendingFootnotes == null)
			{
				this.pendingFootnotes = new ArrayList();
			}
			this.pendingFootnotes.Add(fb);
		}

		// Token: 0x04003C9F RID: 15519
		protected int startIndent;

		// Token: 0x04003CA0 RID: 15520
		protected int endIndent;

		// Token: 0x04003CA1 RID: 15521
		protected int textIndent;

		// Token: 0x04003CA2 RID: 15522
		protected int lineHeight;

		// Token: 0x04003CA3 RID: 15523
		protected int halfLeading;

		// Token: 0x04003CA4 RID: 15524
		protected int align;

		// Token: 0x04003CA5 RID: 15525
		protected int alignLastLine;

		// Token: 0x04003CA6 RID: 15526
		protected LineArea currentLineArea;

		// Token: 0x04003CA7 RID: 15527
		protected LinkSet currentLinkSet;

		// Token: 0x04003CA8 RID: 15528
		protected bool hasLines;

		// Token: 0x04003CA9 RID: 15529
		protected HyphenationProps hyphProps;

		// Token: 0x04003CAA RID: 15530
		protected ArrayList pendingFootnotes;
	}
}
