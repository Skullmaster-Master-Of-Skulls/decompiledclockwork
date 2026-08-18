using System;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015E0 RID: 5600
	internal class BodyRegionArea : RegionArea
	{
		// Token: 0x0600DA4F RID: 55887 RVA: 0x002FD64F File Offset: 0x002FB84F
		public BodyRegionArea(int xPosition, int yPosition, int width, int height) : base(xPosition, yPosition, width, height)
		{
		}

		// Token: 0x0600DA50 RID: 55888 RVA: 0x002FD65C File Offset: 0x002FB85C
		public BodyAreaContainer makeBodyAreaContainer()
		{
			BodyAreaContainer bodyAreaContainer = new BodyAreaContainer(null, this.xPosition, this.yPosition, this.width, this.height, 1, this.columnCount, this.columnGap);
			bodyAreaContainer.setBackground(base.getBackground());
			return bodyAreaContainer;
		}

		// Token: 0x0600DA51 RID: 55889 RVA: 0x002FD6A2 File Offset: 0x002FB8A2
		public void setColumnCount(int columnCount)
		{
			this.columnCount = columnCount;
		}

		// Token: 0x0600DA52 RID: 55890 RVA: 0x002FD6AB File Offset: 0x002FB8AB
		public int getColumnCount()
		{
			return this.columnCount;
		}

		// Token: 0x0600DA53 RID: 55891 RVA: 0x002FD6B3 File Offset: 0x002FB8B3
		public void setColumnGap(int columnGap)
		{
			this.columnGap = columnGap;
		}

		// Token: 0x0600DA54 RID: 55892 RVA: 0x002FD6BC File Offset: 0x002FB8BC
		public int getColumnGap()
		{
			return this.columnGap;
		}

		// Token: 0x04003CBF RID: 15551
		private int columnCount;

		// Token: 0x04003CC0 RID: 15552
		private int columnGap;
	}
}
