using System;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015F8 RID: 5624
	internal class PageMaster
	{
		// Token: 0x0600DB50 RID: 56144 RVA: 0x002FFEB3 File Offset: 0x002FE0B3
		public PageMaster(int pageWidth, int pageHeight)
		{
			this.width = pageWidth;
			this.height = pageHeight;
		}

		// Token: 0x0600DB51 RID: 56145 RVA: 0x002FFEC9 File Offset: 0x002FE0C9
		public void addAfter(RegionArea region)
		{
			this.after = region;
		}

		// Token: 0x0600DB52 RID: 56146 RVA: 0x002FFED2 File Offset: 0x002FE0D2
		public void addBefore(RegionArea region)
		{
			this.before = region;
		}

		// Token: 0x0600DB53 RID: 56147 RVA: 0x002FFEDB File Offset: 0x002FE0DB
		public void addBody(BodyRegionArea region)
		{
			this.body = region;
		}

		// Token: 0x0600DB54 RID: 56148 RVA: 0x002FFEE4 File Offset: 0x002FE0E4
		public void addEnd(RegionArea region)
		{
			this.end = region;
		}

		// Token: 0x0600DB55 RID: 56149 RVA: 0x002FFEED File Offset: 0x002FE0ED
		public void addStart(RegionArea region)
		{
			this.start = region;
		}

		// Token: 0x0600DB56 RID: 56150 RVA: 0x002FFEF6 File Offset: 0x002FE0F6
		public int GetHeight()
		{
			return this.height;
		}

		// Token: 0x0600DB57 RID: 56151 RVA: 0x002FFEFE File Offset: 0x002FE0FE
		public int getWidth()
		{
			return this.width;
		}

		// Token: 0x0600DB58 RID: 56152 RVA: 0x002FFF08 File Offset: 0x002FE108
		public Page makePage(AreaTree areaTree)
		{
			Page page = new Page(areaTree, this.height, this.width);
			if (this.body != null)
			{
				page.addBody(this.body.makeBodyAreaContainer());
			}
			if (this.before != null)
			{
				page.addBefore(this.before.makeAreaContainer());
			}
			if (this.after != null)
			{
				page.addAfter(this.after.makeAreaContainer());
			}
			if (this.start != null)
			{
				page.addStart(this.start.makeAreaContainer());
			}
			if (this.end != null)
			{
				page.addEnd(this.end.makeAreaContainer());
			}
			return page;
		}

		// Token: 0x04003D49 RID: 15689
		private int width;

		// Token: 0x04003D4A RID: 15690
		private int height;

		// Token: 0x04003D4B RID: 15691
		private BodyRegionArea body;

		// Token: 0x04003D4C RID: 15692
		private RegionArea before;

		// Token: 0x04003D4D RID: 15693
		private RegionArea after;

		// Token: 0x04003D4E RID: 15694
		private RegionArea start;

		// Token: 0x04003D4F RID: 15695
		private RegionArea end;
	}
}
