using System;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015D9 RID: 5593
	internal class AreaContainer : Area
	{
		// Token: 0x0600DA0C RID: 55820 RVA: 0x002FCA77 File Offset: 0x002FAC77
		public AreaContainer(FontState fontState, int xPosition, int yPosition, int allocationWidth, int maxHeight, int position) : base(fontState, allocationWidth, maxHeight)
		{
			this.xPosition = xPosition;
			this.yPosition = yPosition;
			this.position = position;
		}

		// Token: 0x0600DA0D RID: 55821 RVA: 0x002FCA9A File Offset: 0x002FAC9A
		public override void render(IRenderer renderer)
		{
			renderer.RenderAreaContainer(this);
		}

		// Token: 0x0600DA0E RID: 55822 RVA: 0x002FCAA3 File Offset: 0x002FACA3
		public int getPosition()
		{
			return this.position;
		}

		// Token: 0x0600DA0F RID: 55823 RVA: 0x002FCAAB File Offset: 0x002FACAB
		public int getXPosition()
		{
			return this.xPosition;
		}

		// Token: 0x0600DA10 RID: 55824 RVA: 0x002FCAB3 File Offset: 0x002FACB3
		public void setXPosition(int value)
		{
			this.xPosition = value;
		}

		// Token: 0x0600DA11 RID: 55825 RVA: 0x002FCABC File Offset: 0x002FACBC
		public int GetYPosition()
		{
			return this.yPosition;
		}

		// Token: 0x0600DA12 RID: 55826 RVA: 0x002FCAC4 File Offset: 0x002FACC4
		public int GetCurrentYPosition()
		{
			return this.yPosition;
		}

		// Token: 0x0600DA13 RID: 55827 RVA: 0x002FCACC File Offset: 0x002FACCC
		public void setYPosition(int value)
		{
			this.yPosition = value;
		}

		// Token: 0x0600DA14 RID: 55828 RVA: 0x002FCAD5 File Offset: 0x002FACD5
		public void shiftYPosition(int value)
		{
			this.yPosition += value;
		}

		// Token: 0x0600DA15 RID: 55829 RVA: 0x002FCAE5 File Offset: 0x002FACE5
		public string getAreaName()
		{
			return this.areaName;
		}

		// Token: 0x0600DA16 RID: 55830 RVA: 0x002FCAED File Offset: 0x002FACED
		public void setAreaName(string areaName)
		{
			this.areaName = areaName;
		}

		// Token: 0x04003C80 RID: 15488
		private int xPosition;

		// Token: 0x04003C81 RID: 15489
		private int yPosition;

		// Token: 0x04003C82 RID: 15490
		private int position;

		// Token: 0x04003C83 RID: 15491
		private string areaName;
	}
}
