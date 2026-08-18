using System;
using System.Collections;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Fo;
using Telerik.Web.Apoc.Fo.Flow;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015DE RID: 5598
	internal class BodyAreaContainer : Area
	{
		// Token: 0x0600DA31 RID: 55857 RVA: 0x002FCED4 File Offset: 0x002FB0D4
		public BodyAreaContainer(FontState fontState, int xPosition, int yPosition, int allocationWidth, int maxHeight, int position, int columnCount, int columnGap) : base(fontState, allocationWidth, maxHeight)
		{
			this.xPosition = xPosition;
			this.yPosition = yPosition;
			this.position = position;
			this.columnCount = columnCount;
			this.columnGap = columnGap;
			this.beforeFloatRefAreaHeight = 0;
			this.footnoteRefAreaHeight = 0;
			this.mainRefAreaHeight = maxHeight - this.beforeFloatRefAreaHeight - this.footnoteRefAreaHeight;
			this.beforeFloatReferenceArea = new AreaContainer(fontState, xPosition, yPosition, allocationWidth, this.beforeFloatRefAreaHeight, 1);
			this.beforeFloatReferenceArea.setAreaName("before-float-reference-area");
			base.addChild(this.beforeFloatReferenceArea);
			this.mainReferenceArea = new AreaContainer(fontState, xPosition, yPosition, allocationWidth, this.mainRefAreaHeight, 1);
			this.mainReferenceArea.setAreaName("main-reference-area");
			base.addChild(this.mainReferenceArea);
			int num = yPosition - this.mainRefAreaHeight;
			this.footnoteReferenceArea = new AreaContainer(fontState, xPosition, num, allocationWidth, this.footnoteRefAreaHeight, 1);
			this.footnoteReferenceArea.setAreaName("footnote-reference-area");
			base.addChild(this.footnoteReferenceArea);
		}

		// Token: 0x0600DA32 RID: 55858 RVA: 0x002FCFD8 File Offset: 0x002FB1D8
		public override void render(IRenderer renderer)
		{
			renderer.RenderBodyAreaContainer(this);
		}

		// Token: 0x0600DA33 RID: 55859 RVA: 0x002FCFE1 File Offset: 0x002FB1E1
		public int getPosition()
		{
			return this.position;
		}

		// Token: 0x0600DA34 RID: 55860 RVA: 0x002FCFE9 File Offset: 0x002FB1E9
		public int getXPosition()
		{
			return this.xPosition + base.getPaddingLeft() + base.getBorderLeftWidth();
		}

		// Token: 0x0600DA35 RID: 55861 RVA: 0x002FCFFF File Offset: 0x002FB1FF
		public void setXPosition(int value)
		{
			this.xPosition = value;
		}

		// Token: 0x0600DA36 RID: 55862 RVA: 0x002FD008 File Offset: 0x002FB208
		public int GetYPosition()
		{
			return this.yPosition + base.getPaddingTop() + base.getBorderTopWidth();
		}

		// Token: 0x0600DA37 RID: 55863 RVA: 0x002FD01E File Offset: 0x002FB21E
		public void setYPosition(int value)
		{
			this.yPosition = value;
		}

		// Token: 0x0600DA38 RID: 55864 RVA: 0x002FD027 File Offset: 0x002FB227
		public AreaContainer getMainReferenceArea()
		{
			return this.mainReferenceArea;
		}

		// Token: 0x0600DA39 RID: 55865 RVA: 0x002FD02F File Offset: 0x002FB22F
		public AreaContainer getBeforeFloatReferenceArea()
		{
			return this.beforeFloatReferenceArea;
		}

		// Token: 0x0600DA3A RID: 55866 RVA: 0x002FD037 File Offset: 0x002FB237
		public AreaContainer getFootnoteReferenceArea()
		{
			return this.footnoteReferenceArea;
		}

		// Token: 0x0600DA3B RID: 55867 RVA: 0x002FD03F File Offset: 0x002FB23F
		public override void setIDReferences(IDReferences idReferences)
		{
			this.mainReferenceArea.setIDReferences(idReferences);
		}

		// Token: 0x0600DA3C RID: 55868 RVA: 0x002FD04D File Offset: 0x002FB24D
		public override IDReferences getIDReferences()
		{
			return this.mainReferenceArea.getIDReferences();
		}

		// Token: 0x0600DA3D RID: 55869 RVA: 0x002FD05C File Offset: 0x002FB25C
		public AreaContainer getNextArea(FObj fo)
		{
			this._isNewSpanArea = false;
			int num = 51;
			Block block = fo as Block;
			if (block != null)
			{
				num = block.GetSpan();
			}
			else
			{
				BlockContainer blockContainer = fo as BlockContainer;
				if (blockContainer != null)
				{
					num = blockContainer.GetSpan();
				}
			}
			if (this.mainReferenceArea.getChildren().Count == 0)
			{
				if (num == 3)
				{
					return this.addSpanArea(1);
				}
				return this.addSpanArea(this.columnCount);
			}
			else
			{
				ArrayList children = this.mainReferenceArea.getChildren();
				SpanArea spanArea = (SpanArea)children[children.Count - 1];
				if (num == 3 && spanArea.getColumnCount() == 1)
				{
					return spanArea.getCurrentColumnArea();
				}
				if (num == 51 && spanArea.getColumnCount() == this.columnCount)
				{
					return spanArea.getCurrentColumnArea();
				}
				if (num == 3)
				{
					return this.addSpanArea(1);
				}
				if (num == 51)
				{
					return this.addSpanArea(this.columnCount);
				}
				throw new ApocException("BodyAreaContainer::getNextArea(): Span attribute messed up");
			}
		}

		// Token: 0x0600DA3E RID: 55870 RVA: 0x002FD140 File Offset: 0x002FB340
		private AreaContainer addSpanArea(int numColumns)
		{
			this.resetHeights();
			int num = this.GetYPosition() - this.mainReferenceArea.getContentHeight();
			SpanArea spanArea = new SpanArea(this.fontState, this.getXPosition(), num, this.allocationWidth, this.GetRemainingHeight(), numColumns, this.columnGap);
			this.mainReferenceArea.addChild(spanArea);
			spanArea.setPage(base.getPage());
			this._isNewSpanArea = true;
			return spanArea.getCurrentColumnArea();
		}

		// Token: 0x0600DA3F RID: 55871 RVA: 0x002FD1B4 File Offset: 0x002FB3B4
		public bool isBalancingRequired(FObj fo)
		{
			if (this.mainReferenceArea.getChildren().Count == 0)
			{
				return false;
			}
			ArrayList children = this.mainReferenceArea.getChildren();
			SpanArea spanArea = (SpanArea)children[children.Count - 1];
			if (spanArea.isBalanced())
			{
				return false;
			}
			int num = 51;
			Block block = fo as Block;
			if (block != null)
			{
				num = block.GetSpan();
			}
			else
			{
				BlockContainer blockContainer = fo as BlockContainer;
				if (blockContainer != null)
				{
					num = blockContainer.GetSpan();
				}
			}
			return (num != 3 || spanArea.getColumnCount() != 1) && (num != 51 || spanArea.getColumnCount() != this.columnCount) && (num == 3 || (num == 51 && false));
		}

		// Token: 0x0600DA40 RID: 55872 RVA: 0x002FD25C File Offset: 0x002FB45C
		public void resetSpanArea()
		{
			ArrayList children = this.mainReferenceArea.getChildren();
			SpanArea spanArea = (SpanArea)children[children.Count - 1];
			if (!spanArea.isBalanced())
			{
				int num = spanArea.getTotalContentHeight() / spanArea.getColumnCount();
				num += 31200;
				this.mainReferenceArea.removeChild(spanArea);
				this.resetHeights();
				SpanArea spanArea2 = new SpanArea(this.fontState, this.getXPosition(), spanArea.GetYPosition(), this.allocationWidth, num, spanArea.getColumnCount(), this.columnGap);
				this.mainReferenceArea.addChild(spanArea2);
				spanArea2.setPage(base.getPage());
				spanArea2.setIsBalanced();
				this._isNewSpanArea = true;
				return;
			}
			throw new Exception("Trying to balance balanced area");
		}

		// Token: 0x0600DA41 RID: 55873 RVA: 0x002FD314 File Offset: 0x002FB514
		public int GetRemainingHeight()
		{
			return this.mainReferenceArea.getMaxHeight() - this.mainReferenceArea.getContentHeight();
		}

		// Token: 0x0600DA42 RID: 55874 RVA: 0x002FD330 File Offset: 0x002FB530
		private void resetHeights()
		{
			int num = 0;
			foreach (object obj in this.mainReferenceArea.getChildren())
			{
				SpanArea spanArea = (SpanArea)obj;
				int maxContentHeight = spanArea.getMaxContentHeight();
				int maxHeight = spanArea.getMaxHeight();
				num += ((maxContentHeight < maxHeight) ? maxContentHeight : maxHeight);
			}
			this.mainReferenceArea.SetHeight(num);
		}

		// Token: 0x0600DA43 RID: 55875 RVA: 0x002FD3B8 File Offset: 0x002FB5B8
		public bool isLastColumn()
		{
			ArrayList children = this.mainReferenceArea.getChildren();
			SpanArea spanArea = (SpanArea)children[children.Count - 1];
			return spanArea.isLastColumn();
		}

		// Token: 0x0600DA44 RID: 55876 RVA: 0x002FD3EB File Offset: 0x002FB5EB
		public bool isNewSpanArea()
		{
			return this._isNewSpanArea;
		}

		// Token: 0x0600DA45 RID: 55877 RVA: 0x002FD3F4 File Offset: 0x002FB5F4
		public AreaContainer getCurrentColumnArea()
		{
			ArrayList children = this.mainReferenceArea.getChildren();
			SpanArea spanArea = (SpanArea)children[children.Count - 1];
			return spanArea.getCurrentColumnArea();
		}

		// Token: 0x0600DA46 RID: 55878 RVA: 0x002FD427 File Offset: 0x002FB627
		public int getFootnoteState()
		{
			return this.footnoteState;
		}

		// Token: 0x0600DA47 RID: 55879 RVA: 0x002FD430 File Offset: 0x002FB630
		public bool needsFootnoteAdjusting()
		{
			this.footnoteYPosition = this.footnoteReferenceArea.GetYPosition();
			switch (this.footnoteState)
			{
			case 0:
				this.resetHeights();
				if (this.footnoteReferenceArea.GetHeight() > 0 && this.mainYPosition + this.mainReferenceArea.GetHeight() > this.footnoteYPosition)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x0600DA48 RID: 55880 RVA: 0x002FD498 File Offset: 0x002FB698
		public void adjustFootnoteArea()
		{
			this.footnoteState++;
			if (this.footnoteState == 1)
			{
				this.mainReferenceArea.setMaxHeight(this.footnoteReferenceArea.GetYPosition() - this.mainYPosition);
				this.footnoteYPosition = this.footnoteReferenceArea.GetYPosition();
				this.footnoteReferenceArea.setMaxHeight(this.footnoteReferenceArea.GetHeight());
				foreach (object obj in this.footnoteReferenceArea.getChildren())
				{
					Area area = obj as Area;
					if (area != null)
					{
						this.footnoteReferenceArea.removeChild(area);
					}
				}
				base.getPage().setPendingFootnotes(null);
			}
		}

		// Token: 0x0600DA49 RID: 55881 RVA: 0x002FD56C File Offset: 0x002FB76C
		protected static void resetMaxHeight(Area ar, int change)
		{
			ar.setMaxHeight(change);
			foreach (object obj in ar.getChildren())
			{
				Area area = obj as Area;
				if (area != null)
				{
					BodyAreaContainer.resetMaxHeight(area, change);
				}
			}
		}

		// Token: 0x04003CAB RID: 15531
		private int xPosition;

		// Token: 0x04003CAC RID: 15532
		private int yPosition;

		// Token: 0x04003CAD RID: 15533
		private int position;

		// Token: 0x04003CAE RID: 15534
		private int columnCount;

		// Token: 0x04003CAF RID: 15535
		private int columnGap;

		// Token: 0x04003CB0 RID: 15536
		private AreaContainer mainReferenceArea;

		// Token: 0x04003CB1 RID: 15537
		private AreaContainer beforeFloatReferenceArea;

		// Token: 0x04003CB2 RID: 15538
		private AreaContainer footnoteReferenceArea;

		// Token: 0x04003CB3 RID: 15539
		private int mainRefAreaHeight;

		// Token: 0x04003CB4 RID: 15540
		private int beforeFloatRefAreaHeight;

		// Token: 0x04003CB5 RID: 15541
		private int footnoteRefAreaHeight;

		// Token: 0x04003CB6 RID: 15542
		private int mainYPosition;

		// Token: 0x04003CB7 RID: 15543
		private int footnoteYPosition;

		// Token: 0x04003CB8 RID: 15544
		private bool _isNewSpanArea;

		// Token: 0x04003CB9 RID: 15545
		private int footnoteState;
	}
}
