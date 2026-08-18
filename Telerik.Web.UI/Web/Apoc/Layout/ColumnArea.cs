using System;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015E4 RID: 5604
	internal class ColumnArea : AreaContainer
	{
		// Token: 0x0600DA6D RID: 55917 RVA: 0x002FD966 File Offset: 0x002FBB66
		public ColumnArea(FontState fontState, int xPosition, int yPosition, int allocationWidth, int maxHeight, int columnCount) : base(fontState, xPosition, yPosition, allocationWidth, maxHeight, 1)
		{
			this.maxColumns = columnCount;
			base.setAreaName("normal-flow-ref.-area");
		}

		// Token: 0x0600DA6E RID: 55918 RVA: 0x002FD989 File Offset: 0x002FBB89
		public override void render(IRenderer renderer)
		{
			renderer.RenderAreaContainer(this);
		}

		// Token: 0x0600DA6F RID: 55919 RVA: 0x002FD992 File Offset: 0x002FBB92
		public override void end()
		{
		}

		// Token: 0x0600DA70 RID: 55920 RVA: 0x002FD994 File Offset: 0x002FBB94
		public override void start()
		{
		}

		// Token: 0x0600DA71 RID: 55921 RVA: 0x002FD996 File Offset: 0x002FBB96
		public override int spaceLeft()
		{
			return this.maxHeight - this.currentHeight;
		}

		// Token: 0x0600DA72 RID: 55922 RVA: 0x002FD9A5 File Offset: 0x002FBBA5
		public int getColumnIndex()
		{
			return this.columnIndex;
		}

		// Token: 0x0600DA73 RID: 55923 RVA: 0x002FD9AD File Offset: 0x002FBBAD
		public void setColumnIndex(int columnIndex)
		{
			this.columnIndex = columnIndex;
		}

		// Token: 0x0600DA74 RID: 55924 RVA: 0x002FD9B8 File Offset: 0x002FBBB8
		public void incrementSpanIndex()
		{
			SpanArea spanArea = (SpanArea)this.parent;
			spanArea.setCurrentColumn(spanArea.getCurrentColumn() + 1);
		}

		// Token: 0x04003CCC RID: 15564
		private int columnIndex;

		// Token: 0x04003CCD RID: 15565
		private int maxColumns;
	}
}
