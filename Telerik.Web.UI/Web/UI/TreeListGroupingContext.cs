using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001290 RID: 4752
	public class TreeListGroupingContext
	{
		// Token: 0x0600C635 RID: 50741 RVA: 0x002C3A44 File Offset: 0x002C1C44
		public TreeListGroupingContext(List<TreeListHierarchyIndex> expandedItems)
		{
			this._expandedItems = expandedItems;
			this._generator = TreeListDisplayIndexGenerator.Create();
		}

		// Token: 0x17004002 RID: 16386
		// (get) Token: 0x0600C636 RID: 50742 RVA: 0x002C3A5E File Offset: 0x002C1C5E
		public List<TreeListHierarchyIndex> ExpandedItems
		{
			get
			{
				return this._expandedItems;
			}
		}

		// Token: 0x17004003 RID: 16387
		// (get) Token: 0x0600C637 RID: 50743 RVA: 0x002C3A66 File Offset: 0x002C1C66
		public TreeListDisplayIndexGenerator IndexGenerator
		{
			get
			{
				return this._generator;
			}
		}

		// Token: 0x04003469 RID: 13417
		private List<TreeListHierarchyIndex> _expandedItems;

		// Token: 0x0400346A RID: 13418
		private TreeListDisplayIndexGenerator _generator;
	}
}
