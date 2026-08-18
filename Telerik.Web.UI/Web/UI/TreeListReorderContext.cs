using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001237 RID: 4663
	internal class TreeListReorderContext
	{
		// Token: 0x0600C058 RID: 49240 RVA: 0x002AB048 File Offset: 0x002A9248
		public TreeListReorderContext(RadTreeList ownerTreeList, TreeListItemDragDropEventArgs args)
		{
			this._ownerTreeList = ownerTreeList;
			this.DragDropEventArgs = args;
			this._indexLists = new List<TreeListIndexesCollection<TreeListHierarchyIndex>>
			{
				this._ownerTreeList.ExpandedIndexes,
				this._ownerTreeList.EditIndexes,
				this._ownerTreeList.InsertIndexes,
				this._ownerTreeList.SelectedIndexes
			};
			this.ReorderedIndexes = new List<TreeListHierarchyIndex>();
			this.ReorderedKeyValuesList = new List<Hashtable>();
			this.OldValuesList = new List<Hashtable>();
			this.IndexToKeyValuesMapping = new Dictionary<TreeListHierarchyIndex, ArrayList>();
			this.ReorderStage = TreeListReorderContext.DataReorderStage.InitialStage;
		}

		// Token: 0x0600C059 RID: 49241 RVA: 0x002AB0ED File Offset: 0x002A92ED
		public void AddReorderedItemData(TreeListHierarchyIndex hierarchyIndex, Hashtable keyValues, Hashtable oldValues)
		{
			this.ReorderedIndexes.Add(hierarchyIndex);
			this.ReorderedKeyValuesList.Add(keyValues);
			this.OldValuesList.Add(oldValues);
		}

		// Token: 0x0600C05A RID: 49242 RVA: 0x002AB114 File Offset: 0x002A9314
		public bool ShouldMapKeyValues(TreeListHierarchyIndex index)
		{
			foreach (TreeListIndexesCollection<TreeListHierarchyIndex> treeListIndexesCollection in this._indexLists)
			{
				for (int i = 0; i < treeListIndexesCollection.Count; i++)
				{
					if (treeListIndexesCollection[i] == index)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600C05B RID: 49243 RVA: 0x002AB188 File Offset: 0x002A9388
		public void CacheTreeListIndexes()
		{
			this._cachedIndexLists = new List<TreeListIndexesCollection<TreeListHierarchyIndex>>
			{
				new TreeListIndexesCollection<TreeListHierarchyIndex>(),
				new TreeListIndexesCollection<TreeListHierarchyIndex>(),
				new TreeListIndexesCollection<TreeListHierarchyIndex>(),
				new TreeListIndexesCollection<TreeListHierarchyIndex>()
			};
			for (int i = 0; i < this._indexLists.Count; i++)
			{
				TreeListIndexesCollection<TreeListHierarchyIndex> treeListIndexesCollection = this._indexLists[i];
				this._cachedIndexLists[i].AddRange(treeListIndexesCollection);
				treeListIndexesCollection.Clear();
			}
		}

		// Token: 0x0600C05C RID: 49244 RVA: 0x002AB20C File Offset: 0x002A940C
		public void ReplaceReorderedIndex(TreeListHierarchyIndex originalIndex, TreeListHierarchyIndex newIndex)
		{
			int num = 0;
			foreach (TreeListIndexesCollection<TreeListHierarchyIndex> treeListIndexesCollection in this._cachedIndexLists)
			{
				for (int i = 0; i < treeListIndexesCollection.Count; i++)
				{
					if (treeListIndexesCollection[i] == originalIndex)
					{
						treeListIndexesCollection.RemoveAt(i);
						this._indexLists[num].Add(new TreeListHierarchyIndex
						{
							NestedLevel = newIndex.NestedLevel,
							LevelIndex = newIndex.LevelIndex
						});
					}
				}
				num++;
			}
		}

		// Token: 0x0600C05D RID: 49245 RVA: 0x002AB2B8 File Offset: 0x002A94B8
		public void RemoveExpandedIndex(TreeListHierarchyIndex index)
		{
			this._ownerTreeList.ExpandedIndexes.Remove(index);
		}

		// Token: 0x17003E1B RID: 15899
		// (get) Token: 0x0600C05E RID: 49246 RVA: 0x002AB2CC File Offset: 0x002A94CC
		// (set) Token: 0x0600C05F RID: 49247 RVA: 0x002AB2D4 File Offset: 0x002A94D4
		public List<TreeListHierarchyIndex> ReorderedIndexes { get; private set; }

		// Token: 0x17003E1C RID: 15900
		// (get) Token: 0x0600C060 RID: 49248 RVA: 0x002AB2DD File Offset: 0x002A94DD
		// (set) Token: 0x0600C061 RID: 49249 RVA: 0x002AB2E5 File Offset: 0x002A94E5
		public TreeListItemDragDropEventArgs DragDropEventArgs { get; private set; }

		// Token: 0x17003E1D RID: 15901
		// (get) Token: 0x0600C062 RID: 49250 RVA: 0x002AB2EE File Offset: 0x002A94EE
		// (set) Token: 0x0600C063 RID: 49251 RVA: 0x002AB2F6 File Offset: 0x002A94F6
		public List<Hashtable> ReorderedKeyValuesList { get; private set; }

		// Token: 0x17003E1E RID: 15902
		// (get) Token: 0x0600C064 RID: 49252 RVA: 0x002AB2FF File Offset: 0x002A94FF
		// (set) Token: 0x0600C065 RID: 49253 RVA: 0x002AB307 File Offset: 0x002A9507
		public List<Hashtable> OldValuesList { get; private set; }

		// Token: 0x17003E1F RID: 15903
		// (get) Token: 0x0600C066 RID: 49254 RVA: 0x002AB310 File Offset: 0x002A9510
		// (set) Token: 0x0600C067 RID: 49255 RVA: 0x002AB318 File Offset: 0x002A9518
		public Dictionary<TreeListHierarchyIndex, ArrayList> IndexToKeyValuesMapping { get; private set; }

		// Token: 0x17003E20 RID: 15904
		// (get) Token: 0x0600C068 RID: 49256 RVA: 0x002AB321 File Offset: 0x002A9521
		// (set) Token: 0x0600C069 RID: 49257 RVA: 0x002AB329 File Offset: 0x002A9529
		public TreeListReorderContext.DataReorderStage ReorderStage { get; set; }

		// Token: 0x0400329F RID: 12959
		private List<TreeListIndexesCollection<TreeListHierarchyIndex>> _indexLists;

		// Token: 0x040032A0 RID: 12960
		private List<TreeListIndexesCollection<TreeListHierarchyIndex>> _cachedIndexLists;

		// Token: 0x040032A1 RID: 12961
		private RadTreeList _ownerTreeList;

		// Token: 0x02001238 RID: 4664
		public enum DataReorderStage
		{
			// Token: 0x040032A9 RID: 12969
			InitialStage,
			// Token: 0x040032AA RID: 12970
			MappingStage,
			// Token: 0x040032AB RID: 12971
			IndexAdjustmentStage,
			// Token: 0x040032AC RID: 12972
			Done
		}
	}
}
