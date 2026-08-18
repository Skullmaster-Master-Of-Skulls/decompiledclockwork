using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001294 RID: 4756
	[Serializable]
	public class TreeListExpandedIndexesCollection : TreeListIndexesCollection<TreeListHierarchyIndex>
	{
		// Token: 0x0600C64E RID: 50766 RVA: 0x002C3D48 File Offset: 0x002C1F48
		internal void AddHash(HashSet<TreeListHierarchyIndex> ExpandHash)
		{
			if (Math.Log((double)base.Count) < (double)ExpandHash.Count)
			{
				ExpandHash.UnionWith(this);
				base.Clear();
				base.AddRange(ExpandHash);
				return;
			}
			foreach (TreeListHierarchyIndex item in ExpandHash)
			{
				base.Add(item);
			}
		}

		// Token: 0x0600C64F RID: 50767 RVA: 0x002C3DC0 File Offset: 0x002C1FC0
		internal void AddHash(HashSet<TreeListSourceItem> ExpandItemsHash)
		{
			HashSet<TreeListHierarchyIndex> hashSet = new HashSet<TreeListHierarchyIndex>();
			foreach (TreeListSourceItem treeListSourceItem in ExpandItemsHash)
			{
				if (treeListSourceItem != null)
				{
					hashSet.Add(treeListSourceItem.HierarchyIndex);
				}
			}
			this.AddHash(hashSet);
		}
	}
}
