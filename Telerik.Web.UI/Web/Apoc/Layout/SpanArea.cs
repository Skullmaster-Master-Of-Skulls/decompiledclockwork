using System;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015FA RID: 5626
	internal class SpanArea : AreaContainer
	{
		// Token: 0x0600DB5A RID: 56154 RVA: 0x002FFFB0 File Offset: 0x002FE1B0
		public SpanArea(FontState fontState, int xPosition, int yPosition, int allocationWidth, int maxHeight, int columnCount, int columnGap) : base(fontState, xPosition, yPosition, allocationWidth, maxHeight, 1)
		{
			this.contentRectangleWidth = allocationWidth;
			this.columnCount = columnCount;
			this.columnGap = columnGap;
			int num = (allocationWidth - columnGap * (columnCount - 1)) / columnCount;
			for (int i = 0; i < columnCount; i++)
			{
				int xPosition2 = xPosition + i * (num + columnGap);
				ColumnArea columnArea = new ColumnArea(fontState, xPosition2, yPosition, num, maxHeight, columnCount);
				base.addChild(columnArea);
				columnArea.setColumnIndex(i + 1);
			}
		}

		// Token: 0x0600DB5B RID: 56155 RVA: 0x00300031 File Offset: 0x002FE231
		public override void render(IRenderer renderer)
		{
			renderer.RenderSpanArea(this);
		}

		// Token: 0x0600DB5C RID: 56156 RVA: 0x0030003A File Offset: 0x002FE23A
		public override void end()
		{
		}

		// Token: 0x0600DB5D RID: 56157 RVA: 0x0030003C File Offset: 0x002FE23C
		public override void start()
		{
		}

		// Token: 0x0600DB5E RID: 56158 RVA: 0x0030003E File Offset: 0x002FE23E
		public override int spaceLeft()
		{
			return this.maxHeight - this.currentHeight;
		}

		// Token: 0x0600DB5F RID: 56159 RVA: 0x0030004D File Offset: 0x002FE24D
		public int getColumnCount()
		{
			return this.columnCount;
		}

		// Token: 0x0600DB60 RID: 56160 RVA: 0x00300055 File Offset: 0x002FE255
		public int getCurrentColumn()
		{
			return this.currentColumn;
		}

		// Token: 0x0600DB61 RID: 56161 RVA: 0x0030005D File Offset: 0x002FE25D
		public void setCurrentColumn(int currentColumn)
		{
			if (currentColumn <= this.columnCount)
			{
				this.currentColumn = currentColumn;
				return;
			}
			this.currentColumn = this.columnCount;
		}

		// Token: 0x0600DB62 RID: 56162 RVA: 0x0030007C File Offset: 0x002FE27C
		public AreaContainer getCurrentColumnArea()
		{
			return (AreaContainer)base.getChildren()[this.currentColumn - 1];
		}

		// Token: 0x0600DB63 RID: 56163 RVA: 0x00300096 File Offset: 0x002FE296
		public bool isBalanced()
		{
			return this._isBalanced;
		}

		// Token: 0x0600DB64 RID: 56164 RVA: 0x0030009E File Offset: 0x002FE29E
		public void setIsBalanced()
		{
			this._isBalanced = true;
		}

		// Token: 0x0600DB65 RID: 56165 RVA: 0x003000A8 File Offset: 0x002FE2A8
		public int getTotalContentHeight()
		{
			int num = 0;
			foreach (object obj in base.getChildren())
			{
				AreaContainer areaContainer = (AreaContainer)obj;
				num += areaContainer.getContentHeight();
			}
			return num;
		}

		// Token: 0x0600DB66 RID: 56166 RVA: 0x00300108 File Offset: 0x002FE308
		public int getMaxContentHeight()
		{
			int num = 0;
			foreach (object obj in base.getChildren())
			{
				AreaContainer areaContainer = (AreaContainer)obj;
				if (areaContainer.getContentHeight() > num)
				{
					num = areaContainer.getContentHeight();
				}
			}
			return num;
		}

		// Token: 0x0600DB67 RID: 56167 RVA: 0x00300170 File Offset: 0x002FE370
		public override void setPage(Page page)
		{
			this.page = page;
			foreach (object obj in base.getChildren())
			{
				AreaContainer areaContainer = (AreaContainer)obj;
				areaContainer.setPage(page);
			}
		}

		// Token: 0x0600DB68 RID: 56168 RVA: 0x003001D0 File Offset: 0x002FE3D0
		public bool isLastColumn()
		{
			return this.currentColumn == this.columnCount;
		}

		// Token: 0x04003D58 RID: 15704
		private int columnCount;

		// Token: 0x04003D59 RID: 15705
		private int currentColumn = 1;

		// Token: 0x04003D5A RID: 15706
		private int columnGap;

		// Token: 0x04003D5B RID: 15707
		private bool _isBalanced;
	}
}
