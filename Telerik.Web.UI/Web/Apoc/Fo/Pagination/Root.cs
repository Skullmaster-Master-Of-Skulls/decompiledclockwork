using System;
using System.Collections;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x02001442 RID: 5186
	internal class Root : FObj
	{
		// Token: 0x0600D397 RID: 54167 RVA: 0x002EF4EB File Offset: 0x002ED6EB
		public new static FObj.Maker GetMaker()
		{
			return new Root.Maker();
		}

		// Token: 0x0600D398 RID: 54168 RVA: 0x002EF4F2 File Offset: 0x002ED6F2
		protected internal Root(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:root";
			this.pageSequences = new ArrayList();
			if (parent != null)
			{
				throw new ApocException("root must be root element");
			}
		}

		// Token: 0x0600D399 RID: 54169 RVA: 0x002EF520 File Offset: 0x002ED720
		protected internal int getRunningPageNumberCounter()
		{
			return this.runningPageNumberCounter;
		}

		// Token: 0x0600D39A RID: 54170 RVA: 0x002EF528 File Offset: 0x002ED728
		protected internal void setRunningPageNumberCounter(int count)
		{
			this.runningPageNumberCounter = count;
		}

		// Token: 0x0600D39B RID: 54171 RVA: 0x002EF531 File Offset: 0x002ED731
		public int getPageSequenceCount()
		{
			return this.pageSequences.Count;
		}

		// Token: 0x0600D39C RID: 54172 RVA: 0x002EF540 File Offset: 0x002ED740
		public PageSequence getSucceedingPageSequence(PageSequence current)
		{
			int num = this.pageSequences.IndexOf(current);
			if (num == -1)
			{
				return null;
			}
			if (num < this.pageSequences.Count - 1)
			{
				return (PageSequence)this.pageSequences[num + 1];
			}
			return null;
		}

		// Token: 0x0600D39D RID: 54173 RVA: 0x002EF585 File Offset: 0x002ED785
		public LayoutMasterSet getLayoutMasterSet()
		{
			return this.layoutMasterSet;
		}

		// Token: 0x0600D39E RID: 54174 RVA: 0x002EF58D File Offset: 0x002ED78D
		public void setLayoutMasterSet(LayoutMasterSet layoutMasterSet)
		{
			this.layoutMasterSet = layoutMasterSet;
		}

		// Token: 0x04003968 RID: 14696
		private LayoutMasterSet layoutMasterSet;

		// Token: 0x04003969 RID: 14697
		private ArrayList pageSequences;

		// Token: 0x0400396A RID: 14698
		private int runningPageNumberCounter;

		// Token: 0x02001443 RID: 5187
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D39F RID: 54175 RVA: 0x002EF596 File Offset: 0x002ED796
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Root(parent, propertyList);
			}
		}
	}
}
