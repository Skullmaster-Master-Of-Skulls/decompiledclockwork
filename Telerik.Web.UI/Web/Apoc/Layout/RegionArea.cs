using System;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015DF RID: 5599
	internal class RegionArea
	{
		// Token: 0x0600DA4A RID: 55882 RVA: 0x002FD5D4 File Offset: 0x002FB7D4
		public RegionArea(int xPosition, int yPosition, int width, int height)
		{
			this.xPosition = xPosition;
			this.yPosition = yPosition;
			this.width = width;
			this.height = height;
		}

		// Token: 0x0600DA4B RID: 55883 RVA: 0x002FD5FC File Offset: 0x002FB7FC
		public AreaContainer makeAreaContainer()
		{
			AreaContainer areaContainer = new AreaContainer(null, this.xPosition, this.yPosition, this.width, this.height, 1);
			areaContainer.setBackground(this.getBackground());
			return areaContainer;
		}

		// Token: 0x0600DA4C RID: 55884 RVA: 0x002FD636 File Offset: 0x002FB836
		public BackgroundProps getBackground()
		{
			return this.background;
		}

		// Token: 0x0600DA4D RID: 55885 RVA: 0x002FD63E File Offset: 0x002FB83E
		public void setBackground(BackgroundProps bg)
		{
			this.background = bg;
		}

		// Token: 0x0600DA4E RID: 55886 RVA: 0x002FD647 File Offset: 0x002FB847
		public int GetHeight()
		{
			return this.height;
		}

		// Token: 0x04003CBA RID: 15546
		protected int xPosition;

		// Token: 0x04003CBB RID: 15547
		protected int yPosition;

		// Token: 0x04003CBC RID: 15548
		protected int width;

		// Token: 0x04003CBD RID: 15549
		protected int height;

		// Token: 0x04003CBE RID: 15550
		protected BackgroundProps background;
	}
}
