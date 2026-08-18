using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x0200123A RID: 4666
	public class TreeListEnumerableHelper
	{
		// Token: 0x17003E21 RID: 15905
		// (get) Token: 0x0600C06E RID: 49262 RVA: 0x002AB42C File Offset: 0x002A962C
		// (set) Token: 0x0600C06F RID: 49263 RVA: 0x002AB434 File Offset: 0x002A9634
		public IList<string> KeyNames { get; internal set; }

		// Token: 0x17003E22 RID: 15906
		// (get) Token: 0x0600C070 RID: 49264 RVA: 0x002AB43D File Offset: 0x002A963D
		// (set) Token: 0x0600C071 RID: 49265 RVA: 0x002AB445 File Offset: 0x002A9645
		public IList<string> ParentKeyNames { get; internal set; }

		// Token: 0x17003E23 RID: 15907
		// (get) Token: 0x0600C072 RID: 49266 RVA: 0x002AB44E File Offset: 0x002A964E
		// (set) Token: 0x0600C073 RID: 49267 RVA: 0x002AB455 File Offset: 0x002A9655
		internal static bool showFooter { get; set; }

		// Token: 0x17003E24 RID: 15908
		// (get) Token: 0x0600C074 RID: 49268 RVA: 0x002AB45D File Offset: 0x002A965D
		// (set) Token: 0x0600C075 RID: 49269 RVA: 0x002AB465 File Offset: 0x002A9665
		public IEnumerable OriginalSource { get; internal set; }

		// Token: 0x17003E25 RID: 15909
		// (get) Token: 0x0600C076 RID: 49270 RVA: 0x002AB46E File Offset: 0x002A966E
		// (set) Token: 0x0600C077 RID: 49271 RVA: 0x002AB476 File Offset: 0x002A9676
		public ArrayList DataLeft { get; internal set; }

		// Token: 0x17003E26 RID: 15910
		// (get) Token: 0x0600C078 RID: 49272 RVA: 0x002AB47F File Offset: 0x002A967F
		// (set) Token: 0x0600C079 RID: 49273 RVA: 0x002AB487 File Offset: 0x002A9687
		internal TreeListEnumerableHelper.TreeListDataItemEvaluator ItemEvaluator { get; set; }

		// Token: 0x17003E27 RID: 15911
		// (get) Token: 0x0600C07A RID: 49274 RVA: 0x002AB490 File Offset: 0x002A9690
		// (set) Token: 0x0600C07B RID: 49275 RVA: 0x002AB498 File Offset: 0x002A9698
		public TreeListEnumerableBase TreeListEnumerable { get; protected set; }

		// Token: 0x17003E28 RID: 15912
		// (get) Token: 0x0600C07C RID: 49276 RVA: 0x002AB4A1 File Offset: 0x002A96A1
		// (set) Token: 0x0600C07D RID: 49277 RVA: 0x002AB4A9 File Offset: 0x002A96A9
		private TreeListGroupingContext GroupingContext { get; set; }

		// Token: 0x17003E29 RID: 15913
		// (get) Token: 0x0600C07E RID: 49278 RVA: 0x002AB4B2 File Offset: 0x002A96B2
		// (set) Token: 0x0600C07F RID: 49279 RVA: 0x002AB4BA File Offset: 0x002A96BA
		internal int TotalItemCount { get; private set; }

		// Token: 0x17003E2A RID: 15914
		// (get) Token: 0x0600C080 RID: 49280 RVA: 0x002AB4C3 File Offset: 0x002A96C3
		// (set) Token: 0x0600C081 RID: 49281 RVA: 0x002AB4CB File Offset: 0x002A96CB
		internal IList<TreeListSourceItem> RootItems { get; private set; }

		// Token: 0x0600C082 RID: 49282 RVA: 0x002AB4D4 File Offset: 0x002A96D4
		public TreeListEnumerableHelper(TreeListEnumerableBase treeListEnumerable, IList<string> keyNames, IList<string> parentKeyNames)
		{
			if (keyNames == null)
			{
				throw new ArgumentNullException("keyNames");
			}
			if (parentKeyNames == null)
			{
				throw new ArgumentNullException("parentKeyNames");
			}
			if (keyNames.Count != parentKeyNames.Count)
			{
				throw new ArgumentException("KeyNames collection length must match ParentKeyNames collection length.");
			}
			this.KeyNames = keyNames;
			this.ParentKeyNames = parentKeyNames;
			this.ItemEvaluator = new TreeListEnumerableHelper.TreeListDataItemEvaluator();
			this.DataLeft = new ArrayList();
			this.TreeListEnumerable = treeListEnumerable;
		}

		// Token: 0x0600C083 RID: 49283 RVA: 0x002AB5E0 File Offset: 0x002A97E0
		public IList<TreeListSourceItem> GroupEnumerable(IEnumerable source, TreeListGroupingContext context)
		{
			List<TreeListSourceItem> list = new List<TreeListSourceItem>();
			this.OriginalSource = this.WrapOriginalEnumerable(source);
			this.GroupingContext = context;
			this.GroupingContext.IndexGenerator.Reset();
			List<TAction<IList<TreeListSourceItem>>> list2 = new List<TAction<IList<TreeListSourceItem>>>();
			if (TreeListEnumerableHelper.showFooter && TreeListAggregatesHelper.AggregatesSourceItemsCollection != null && TreeListAggregatesHelper.AggregatesSourceItemsCollection.Count == 0)
			{
				TreeListAggregatesHelper.AggregatedSourceItems = new Dictionary<TreeListHierarchyIndex, TreeListSourceItem>();
			}
			this.RootItems = this.RetrieveRootItems();
			int num = 0;
			foreach (TreeListSourceItem treeListSourceItem in this.RootItems)
			{
				IList<TreeListSourceItem> childItems = this.GetChildItemsRecursive(treeListSourceItem);
				treeListSourceItem.ItemState.HasChildItems = (childItems.Count > 0);
				treeListSourceItem.ChildItems = childItems;
				treeListSourceItem.SiblingsCount = this.RootItems.Count;
				treeListSourceItem.ItemIndex = num;
				list.Add(treeListSourceItem);
				if (this.ShouldAddItemInResult(treeListSourceItem.HierarchyIndex))
				{
					TreeListSourceItem rootItem = treeListSourceItem;
					TAction<IList<TreeListSourceItem>> item = delegate(IList<TreeListSourceItem> parentsCollection)
					{
						int num2 = parentsCollection.IndexOf(rootItem);
						foreach (TreeListSourceItem item2 in childItems)
						{
							parentsCollection.Insert(++num2, item2);
						}
					};
					list2.Add(item);
				}
				num++;
				this.TotalItemCount++;
			}
			if (this.reorderContext != null && this.reorderContext.ReorderStage == TreeListReorderContext.DataReorderStage.IndexAdjustmentStage)
			{
				this.reorderContext.ReorderStage = TreeListReorderContext.DataReorderStage.Done;
			}
			if (this.RootItems.Count == 0)
			{
				return this.RootItems;
			}
			IList<TreeListSourceItem> sortedCollection = this.Sort(this.RootItems);
			list2.ForEach(delegate(TAction<IList<TreeListSourceItem>> action)
			{
				action(sortedCollection);
			});
			this.AdjustIndexes(sortedCollection);
			return sortedCollection;
		}

		// Token: 0x0600C084 RID: 49284 RVA: 0x002AB84C File Offset: 0x002A9A4C
		public IList<TreeListSourceItem> GroupEnumerableWhenLoadOnDemand(IEnumerable source, TreeListGroupingContext context)
		{
			RadTreeList ownerTreeList = this.TreeListEnumerable.OwnerTreeList;
			this.OriginalSource = this.WrapOriginalEnumerable(source);
			this.GroupingContext = context;
			this.GroupingContext.IndexGenerator.Reset();
			this.RootItems = new List<TreeListSourceItem>();
			bool flag = true;
			List<TAction<IList<TreeListSourceItem>>> list = new List<TAction<IList<TreeListSourceItem>>>();
			foreach (object obj in this.OriginalSource)
			{
				if (flag)
				{
					this.TreeListEnumerable.Columns = new TreeListDataColumns(obj);
					flag = false;
					this.CacheCalculatedColumns();
				}
				this.RootItems.Add(this.BuildSourceItem(0, obj));
			}
			int num = 0;
			foreach (TreeListSourceItem treeListSourceItem in this.RootItems)
			{
				treeListSourceItem.SiblingsCount = this.RootItems.Count;
				treeListSourceItem.ItemIndex = num;
				IList<TreeListSourceItem> childItems = this.GetChildItemsReqursiceWhenLoadOnDemand(treeListSourceItem);
				foreach (TreeListSourceItem treeListSourceItem2 in childItems)
				{
					treeListSourceItem2.SiblingsCount = childItems.Count;
				}
				treeListSourceItem.ChildItems = childItems;
				if (ownerTreeList.HideExpandCollapseButtonIfNoChildren && ownerTreeList.LoadOnDemandContext.ExpandedItems.Contains(treeListSourceItem))
				{
					treeListSourceItem.ItemState.HasChildItems = (childItems.Count > 0);
					TreeListSourceItem rootItem = treeListSourceItem;
					TAction<IList<TreeListSourceItem>> item = delegate(IList<TreeListSourceItem> parentsCollection)
					{
						int num2 = parentsCollection.IndexOf(rootItem);
						foreach (TreeListSourceItem item2 in childItems)
						{
							parentsCollection.Insert(++num2, item2);
						}
					};
					list.Add(item);
				}
				else
				{
					treeListSourceItem.ItemState.HasChildItems = true;
				}
				num++;
				this.TotalItemCount++;
				this.TotalItemCount += childItems.Count;
			}
			if (this.RootItems.Count == 0)
			{
				return this.RootItems;
			}
			if (this.reorderContext != null && this.reorderContext.ReorderStage == TreeListReorderContext.DataReorderStage.IndexAdjustmentStage)
			{
				this.reorderContext.ReorderStage = TreeListReorderContext.DataReorderStage.Done;
			}
			IList<TreeListSourceItem> sortedCollection = this.Sort(this.RootItems);
			list.ForEach(delegate(TAction<IList<TreeListSourceItem>> action)
			{
				action(sortedCollection);
			});
			this.AdjustIndexes(sortedCollection);
			return sortedCollection;
		}

		// Token: 0x0600C085 RID: 49285 RVA: 0x002ABB2C File Offset: 0x002A9D2C
		private void CacheCalculatedColumns()
		{
			IEnumerable<TreeListCalculatedColumn> enumerable = this.TreeListEnumerable.OwnerTreeList.Columns.OfType<TreeListCalculatedColumn>();
			this.CachedCalculatedColumn = new Dictionary<string, Delegate>();
			this.TreeListEnumerable.OwnerTreeList.Columns.OfType<TreeListDataColumn>();
			foreach (TreeListCalculatedColumn treeListCalculatedColumn in enumerable)
			{
				List<string> list = new List<string>();
				int num = 0;
				foreach (string text in treeListCalculatedColumn.DataFields)
				{
					if (this.TreeListEnumerable.Columns.ContainsKey(text))
					{
						Type nonNullableType = TreeListTypeHelper.GetNonNullableType(this.TreeListEnumerable.Columns[text].PropertyType);
						if (nonNullableType != typeof(string) && nonNullableType != typeof(object))
						{
							list.Add(string.Format("{0}?", nonNullableType.ToString().Split(new char[]
							{
								'.'
							})[1]));
						}
						else if (nonNullableType != typeof(string))
						{
							list.Add("object");
						}
						else
						{
							list.Add("Convert.ToString");
						}
						num++;
					}
					if (num == 0)
					{
						throw new FormatException(string.Format("DataField \"{0}\" for TreeListCalculatedColumn \"{1}\" does not exist in current DataSource.", text, treeListCalculatedColumn.UniqueName));
					}
				}
				this.CachedCalculatedColumn.Add(string.Format("{0}Result", treeListCalculatedColumn.UniqueName), DynamicExpression.ParseLambda(this.ResolvedItemType, typeof(object), this.FormatExpression(list, treeListCalculatedColumn.Expression, treeListCalculatedColumn.UniqueName, treeListCalculatedColumn.DataFields), new object[0]).Compile());
			}
		}

		// Token: 0x0600C086 RID: 49286 RVA: 0x002ABD94 File Offset: 0x002A9F94
		public IList<TreeListSourceItem> GetChildItemsReqursiceWhenLoadOnDemand(TreeListSourceItem item)
		{
			RadTreeList ownerTreeList = this.TreeListEnumerable.OwnerTreeList;
			List<TAction<IList<TreeListSourceItem>>> list = new List<TAction<IList<TreeListSourceItem>>>();
			List<TreeListSourceItem> list2 = new List<TreeListSourceItem>();
			Hashtable hashtable = new Hashtable();
			List<DataKey> list3 = new List<DataKey>();
			new TreeListDataKeyArray(list3);
			foreach (string text in ownerTreeList.DataKeyNames)
			{
				DataKey dataKey = new DataKey();
				dataKey[text] = ownerTreeList.ExtractDataKeyValue(item.OriginalDataItem, text);
				hashtable[text] = dataKey[text];
				list3.Add(dataKey);
			}
			bool flag = false;
			bool flag2 = false;
			if (this.ExpandedItemsDataKeyValuesHash == null)
			{
				this.ExpandedItemsDataKeyValuesHash = new HashSet<Hashtable>(ownerTreeList.LoadOnDemandContext.ExpandedItemsDataKeyValues, new TreeListEnumerableHelper.ExpandedItemsDataKeyValuesHashEqualityComparer());
			}
			if (ownerTreeList.LoadOnDemandContext.ExpandedOnDemandIndexes.Contains(item.HierarchyIndex))
			{
				flag = true;
				ownerTreeList.LoadOnDemandContext.ExpandedItems.Add(item);
				ownerTreeList.LoadOnDemandContext.InsertExpandedDataKeyValue(item);
				this.ExpandedItemsDataKeyValuesHash.Add(hashtable);
				if (item.HierarchyIndex.NestedLevel < ownerTreeList.LoadOnDemandContext.ExpansionDepth - 1)
				{
					flag2 = true;
				}
			}
			else if (ownerTreeList.LoadOnDemandContext.ExpandedItemsDataKeyValues.Count > 0)
			{
				if (this.ExpandedItemsDataKeyValuesHash.Contains(hashtable))
				{
					flag = true;
					ownerTreeList.LoadOnDemandContext.ExpandedItems.Add(item);
				}
			}
			else
			{
				flag = ownerTreeList.LoadOnDemandContext.ExpandedItems.Contains(item);
			}
			if (flag)
			{
				new List<TreeListSourceItem>();
				int num = 0;
				TreeListChildItemsDataBindEventArgs treeListChildItemsDataBindEventArgs = new TreeListChildItemsDataBindEventArgs(item.HierarchyIndex, hashtable);
				ownerTreeList.CallOnChildItemsDataBind(treeListChildItemsDataBindEventArgs);
				IEnumerable enumerable;
				if (treeListChildItemsDataBindEventArgs.ChildItemsDataSource.GetType().Name == "SqlDataReader")
				{
					enumerable = ((IEnumerable)treeListChildItemsDataBindEventArgs.ChildItemsDataSource).Cast<IDataRecord>().ToList<IDataRecord>();
				}
				else
				{
					IEnumerable enumerableFromSource = TreeListDataSourceHelper.GetEnumerableFromSource(treeListChildItemsDataBindEventArgs.ChildItemsDataSource, string.Empty);
					enumerable = this.WrapOriginalEnumerable(enumerableFromSource);
				}
				int nestedLevel = item.HierarchyIndex.NestedLevel + 1;
				foreach (object originalItem in enumerable)
				{
					TreeListSourceItem sourceItem = this.BuildSourceItem(nestedLevel, originalItem);
					if (flag2)
					{
						ownerTreeList.LoadOnDemandContext.ExpandedOnDemandIndexes.Add(sourceItem.HierarchyIndex);
					}
					IList<TreeListSourceItem> subChildItems = this.GetChildItemsReqursiceWhenLoadOnDemand(sourceItem);
					foreach (TreeListSourceItem treeListSourceItem in subChildItems)
					{
						treeListSourceItem.SiblingsCount = subChildItems.Count;
					}
					if (ownerTreeList.HideExpandCollapseButtonIfNoChildren && ownerTreeList.LoadOnDemandContext.ExpandedItems.Contains(sourceItem))
					{
						sourceItem.ItemState.HasChildItems = (subChildItems.Count > 0);
					}
					else
					{
						sourceItem.ItemState.HasChildItems = true;
					}
					sourceItem.ItemState.ParentHierarchyIndex = item.HierarchyIndex;
					sourceItem.ChildItems = subChildItems;
					sourceItem.ParentItem = item;
					sourceItem.ItemIndex = num;
					list2.Add(sourceItem);
					if (ownerTreeList.LoadOnDemandContext.ExpandedItems.Contains(sourceItem))
					{
						TAction<IList<TreeListSourceItem>> item2 = delegate(IList<TreeListSourceItem> parentsCollection)
						{
							int num2 = parentsCollection.IndexOf(sourceItem);
							foreach (TreeListSourceItem item3 in subChildItems)
							{
								parentsCollection.Insert(++num2, item3);
							}
						};
						list.Add(item2);
					}
					num++;
					this.TotalItemCount++;
				}
			}
			IList<TreeListSourceItem> sortedCollection = (list2.Count == 0) ? list2 : this.Sort(list2);
			list.ForEach(delegate(TAction<IList<TreeListSourceItem>> action)
			{
				action(sortedCollection);
			});
			return sortedCollection;
		}

		// Token: 0x0600C087 RID: 49287 RVA: 0x002AC1C4 File Offset: 0x002AA3C4
		internal IEnumerable<TreeListSourceItem> PrepareItemsForDelete(IEnumerable source, TreeListDeleteContext context)
		{
			this.isRecursilveDelete = this.TreeListEnumerable.OwnerTreeList.AllowRecursiveDelete;
			this.deleteContext = context;
			this.OriginalSource = this.WrapOriginalEnumerable(source);
			this.GroupingContext = new TreeListGroupingContext(new List<TreeListHierarchyIndex>());
			TreeListDisplayIndexGenerator treeListDisplayIndexGenerator = TreeListDisplayIndexGenerator.Create();
			this._itemsToDelete = new TreeListDeleteItemsEnumerable();
			IList<TreeListSourceItem> list = this.RetrieveRootItems();
			bool flag = false;
			foreach (TreeListSourceItem treeListSourceItem in list)
			{
				bool isParentDeleted = false;
				int nestedLevel = treeListSourceItem.HierarchyIndex.NestedLevel;
				if (this.ShouldDelete(treeListSourceItem, context.Keys))
				{
					flag = true;
					isParentDeleted = true;
					this._itemsToDelete.Add(treeListSourceItem);
					this.InvalidateItemIndex(treeListSourceItem.HierarchyIndex);
				}
				else if (flag)
				{
					TreeListHierarchyIndex treeListHierarchyIndex = new TreeListHierarchyIndex();
					treeListHierarchyIndex.NestedLevel = nestedLevel;
					treeListHierarchyIndex.LevelIndex = treeListDisplayIndexGenerator.GenerateIndex(nestedLevel);
					this.ResetItemState(treeListSourceItem.HierarchyIndex, treeListHierarchyIndex);
					treeListSourceItem.HierarchyIndex = treeListHierarchyIndex;
				}
				else
				{
					treeListDisplayIndexGenerator.GenerateIndex(nestedLevel);
					this.CopyItemState(treeListSourceItem.HierarchyIndex);
				}
				this.DeleteItemsRecursive(treeListSourceItem, isParentDeleted, ref flag, treeListDisplayIndexGenerator, context.Keys);
			}
			return this._itemsToDelete;
		}

		// Token: 0x0600C088 RID: 49288 RVA: 0x002AC30C File Offset: 0x002AA50C
		protected void DeleteItemsRecursive(TreeListSourceItem parentItem, bool isParentDeleted, ref bool isDeleteItemFound, TreeListDisplayIndexGenerator indexGenerator, IDictionary keys)
		{
			ArrayList childItems = this.GetChildItems(parentItem.OriginalDataItem);
			foreach (object originalItem in childItems)
			{
				bool isParentDeleted2 = isParentDeleted;
				int nestedLevel = parentItem.HierarchyIndex.NestedLevel + 1;
				TreeListSourceItem treeListSourceItem = this.BuildSourceItem(nestedLevel, originalItem);
				if (isParentDeleted)
				{
					this.InvalidateItemIndex(treeListSourceItem.HierarchyIndex);
					this._itemsToDelete.Add(treeListSourceItem);
				}
				else if (isDeleteItemFound)
				{
					TreeListHierarchyIndex treeListHierarchyIndex = new TreeListHierarchyIndex();
					treeListHierarchyIndex.NestedLevel = nestedLevel;
					treeListHierarchyIndex.LevelIndex = indexGenerator.GenerateIndex(nestedLevel);
					this.ResetItemState(treeListSourceItem.HierarchyIndex, treeListHierarchyIndex);
					treeListSourceItem.HierarchyIndex = treeListHierarchyIndex;
				}
				else if (this.ShouldDelete(treeListSourceItem, keys))
				{
					this.InvalidateItemIndex(treeListSourceItem.HierarchyIndex);
					this._itemsToDelete.Add(treeListSourceItem);
					isParentDeleted2 = true;
					isDeleteItemFound = true;
					if (childItems.Count == 1)
					{
						TreeListExpandedIndexesCollection expandedIndexes = this.TreeListEnumerable.OwnerTreeList.ExpandedIndexes;
						if (expandedIndexes.Contains(parentItem.HierarchyIndex))
						{
							expandedIndexes.Remove(parentItem.HierarchyIndex);
						}
						if (this.deleteContext.Indexes[0].Contains(parentItem.HierarchyIndex))
						{
							this.deleteContext.Indexes[0].Remove(parentItem.HierarchyIndex);
						}
					}
				}
				else
				{
					indexGenerator.GenerateIndex(nestedLevel);
					this.CopyItemState(treeListSourceItem.HierarchyIndex);
				}
				this.DeleteItemsRecursive(treeListSourceItem, isParentDeleted2, ref isDeleteItemFound, indexGenerator, keys);
			}
		}

		// Token: 0x0600C089 RID: 49289 RVA: 0x002AC4C0 File Offset: 0x002AA6C0
		private void ResetItemState(TreeListHierarchyIndex oldIndex, TreeListHierarchyIndex newIndex)
		{
			RadTreeList ownerTreeList = this.TreeListEnumerable.OwnerTreeList;
			List<TreeListIndexesCollection<TreeListHierarchyIndex>> list = new List<TreeListIndexesCollection<TreeListHierarchyIndex>>
			{
				ownerTreeList.ExpandedIndexes,
				ownerTreeList.EditIndexes,
				ownerTreeList.InsertIndexes,
				ownerTreeList.SelectedIndexes
			};
			int num = 0;
			foreach (TreeListIndexesCollection<TreeListHierarchyIndex> treeListIndexesCollection in list)
			{
				int num2 = treeListIndexesCollection.IndexOf(oldIndex);
				if (num2 >= 0)
				{
					if (this.isRecursilveDelete)
					{
						treeListIndexesCollection[num2] = newIndex;
					}
					else
					{
						this.deleteContext.Indexes[num].Add(newIndex);
					}
				}
				num++;
			}
		}

		// Token: 0x0600C08A RID: 49290 RVA: 0x002AC590 File Offset: 0x002AA790
		private void CopyItemState(TreeListHierarchyIndex index)
		{
			RadTreeList ownerTreeList = this.TreeListEnumerable.OwnerTreeList;
			List<TreeListIndexesCollection<TreeListHierarchyIndex>> list = new List<TreeListIndexesCollection<TreeListHierarchyIndex>>
			{
				ownerTreeList.ExpandedIndexes,
				ownerTreeList.EditIndexes,
				ownerTreeList.InsertIndexes,
				ownerTreeList.SelectedIndexes
			};
			int num = 0;
			foreach (TreeListIndexesCollection<TreeListHierarchyIndex> treeListIndexesCollection in list)
			{
				if (treeListIndexesCollection.Contains(index))
				{
					this.deleteContext.Indexes[num].Add(index);
				}
				num++;
			}
		}

		// Token: 0x0600C08B RID: 49291 RVA: 0x002AC648 File Offset: 0x002AA848
		internal void SetReorderContext(TreeListReorderContext context)
		{
			this.reorderContext = context;
		}

		// Token: 0x0600C08C RID: 49292 RVA: 0x002AC654 File Offset: 0x002AA854
		internal IEnumerable<TreeListSourceItem> PrepareItemsAfterReorder(IEnumerable source)
		{
			this._itemsToReorder = new List<TreeListSourceItem>();
			this.OriginalSource = this.WrapOriginalEnumerable(source);
			this.GroupingContext = new TreeListGroupingContext(new List<TreeListHierarchyIndex>());
			IList<TreeListSourceItem> list = this.RetrieveRootItems();
			foreach (TreeListSourceItem sourceItem in list)
			{
				this.MapHierarchyIndexesToKeyValuesRecursive(sourceItem);
			}
			this.reorderContext.CacheTreeListIndexes();
			return this._itemsToReorder;
		}

		// Token: 0x0600C08D RID: 49293 RVA: 0x002AC6E0 File Offset: 0x002AA8E0
		private bool MapHierarchyIndexesToKeyValuesRecursive(TreeListSourceItem sourceItem)
		{
			bool flag = this.ShouldReorder(sourceItem);
			if (flag)
			{
				this._itemsToReorder.Add(sourceItem);
			}
			ArrayList childItems = this.GetChildItems(sourceItem.OriginalDataItem);
			int num = 0;
			foreach (object originalItem in childItems)
			{
				int nestedLevel = sourceItem.HierarchyIndex.NestedLevel + 1;
				TreeListSourceItem sourceItem2 = this.BuildSourceItem(nestedLevel, originalItem);
				bool flag2 = this.MapHierarchyIndexesToKeyValuesRecursive(sourceItem2);
				if (flag2)
				{
					num++;
				}
			}
			TreeListHierarchyIndex left = (this.reorderContext.DragDropEventArgs.DestinationDataItem != null) ? this.reorderContext.DragDropEventArgs.DestinationDataItem.HierarchyIndex : null;
			if ((left == null || left != sourceItem.HierarchyIndex) && childItems.Count > 0 && childItems.Count == num)
			{
				this.reorderContext.RemoveExpandedIndex(sourceItem.HierarchyIndex);
			}
			if (this.reorderContext.ShouldMapKeyValues(sourceItem.HierarchyIndex))
			{
				ArrayList value = this.ItemEvaluator.ExtractKeyValues(sourceItem.OriginalDataItem, this.KeyNames);
				this.reorderContext.IndexToKeyValuesMapping[sourceItem.HierarchyIndex] = value;
			}
			return flag;
		}

		// Token: 0x0600C08E RID: 49294 RVA: 0x002AC830 File Offset: 0x002AAA30
		private bool ShouldReorder(TreeListSourceItem sourceItem)
		{
			foreach (Hashtable ownerKeys in this.reorderContext.ReorderedKeyValuesList)
			{
				if (this.IsEqual(sourceItem.OriginalDataItem, ownerKeys, this.KeyNames))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600C08F RID: 49295 RVA: 0x002AC8A0 File Offset: 0x002AAAA0
		private void InvalidateItemIndex(TreeListHierarchyIndex index)
		{
			if (!this.isRecursilveDelete)
			{
				return;
			}
			RadTreeList ownerTreeList = this.TreeListEnumerable.OwnerTreeList;
			ownerTreeList.ExpandedIndexes.Remove(index);
			ownerTreeList.EditIndexes.Remove(index);
			ownerTreeList.InsertIndexes.Remove(index);
			ownerTreeList.SelectedIndexes.Remove(index);
		}

		// Token: 0x0600C090 RID: 49296 RVA: 0x002AC8F6 File Offset: 0x002AAAF6
		private bool ShouldDelete(TreeListSourceItem item, IDictionary keys)
		{
			return this.IsEqual(item.OriginalDataItem, keys, this.KeyNames) || (item.HierarchyIndex.NestedLevel != 0 && this.IsEqual(item.OriginalDataItem, keys, this.ParentKeyNames));
		}

		// Token: 0x0600C091 RID: 49297 RVA: 0x002AC934 File Offset: 0x002AAB34
		private bool IsEqual(object item, IDictionary ownerKeys, IList<string> keyNames)
		{
			ArrayList arrayList = this.ItemEvaluator.ExtractKeyValues(item, keyNames);
			for (int i = 0; i < this.KeyNames.Count; i++)
			{
				if (!object.Equals(arrayList[i], ownerKeys[keyNames[i]]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600C092 RID: 49298 RVA: 0x002AC984 File Offset: 0x002AAB84
		private void AdjustIndexes(IList<TreeListSourceItem> groupedEnumerable)
		{
			this.GroupingContext.IndexGenerator.Reset();
			Stack<int> stack = new Stack<int>();
			TreeListSourceItem treeListSourceItem = null;
			int num = -1;
			foreach (TreeListSourceItem treeListSourceItem2 in groupedEnumerable)
			{
				if (treeListSourceItem != null)
				{
					if (treeListSourceItem.HierarchyIndex.NestedLevel < treeListSourceItem2.HierarchyIndex.NestedLevel)
					{
						stack.Push(num);
						num = -1;
					}
				}
				while (treeListSourceItem2.HierarchyIndex.NestedLevel < stack.Count)
				{
					num = stack.Pop();
				}
				treeListSourceItem = treeListSourceItem2;
				TreeListHierarchyIndex treeListHierarchyIndex = new TreeListHierarchyIndex();
				treeListHierarchyIndex.NestedLevel = treeListSourceItem2.HierarchyIndex.NestedLevel;
				treeListHierarchyIndex.LevelIndex = this.GroupingContext.IndexGenerator.GenerateIndex(treeListSourceItem2.HierarchyIndex.NestedLevel);
				Dictionary<TreeListHierarchyIndex, TreeListEnumerableHelper.CalculatedIndexes> dictionary = this.reCalculatedIndexes;
				TreeListHierarchyIndex hierarchyIndex = treeListSourceItem2.HierarchyIndex;
				TreeListEnumerableHelper.CalculatedIndexes value = default(TreeListEnumerableHelper.CalculatedIndexes);
				value.HierarchyIndex = treeListHierarchyIndex;
				num = (value.ItemIndex = num + 1);
				dictionary.Add(hierarchyIndex, value);
			}
		}

		// Token: 0x0600C093 RID: 49299 RVA: 0x002ACD14 File Offset: 0x002AAF14
		public IEnumerable WrapOriginalEnumerable(IEnumerable source)
		{
			object item = null;
			foreach (object dataItem in source)
			{
				ICustomTypeDescriptor originalItem = dataItem as ICustomTypeDescriptor;
				if (originalItem != null)
				{
					object propertyOwner = originalItem.GetPropertyOwner(null);
					if (propertyOwner == null)
					{
						PropertyDescriptorCollection properties = originalItem.GetProperties();
						propertyOwner = originalItem.GetPropertyOwner(properties[0]);
					}
					item = propertyOwner;
				}
				else
				{
					item = dataItem;
				}
				if (this.ResolvedItemType == null)
				{
					this.ResolvedItemType = item.GetType();
					TreeListEnumerableHelper.firstDataItem = item;
					TreeListEnumerableHelper.resolvedItemType = item.GetType();
					this.TreeListEnumerable.OwnerTreeList.FirstItemInstance = item;
				}
				yield return item;
			}
			yield break;
		}

		// Token: 0x0600C094 RID: 49300 RVA: 0x002ACD38 File Offset: 0x002AAF38
		public void SetSortExpressions(TreeListSortExpressionCollection sortExpressions)
		{
			this.SortExpressions = sortExpressions;
		}

		// Token: 0x17003E2B RID: 15915
		// (get) Token: 0x0600C095 RID: 49301 RVA: 0x002ACD41 File Offset: 0x002AAF41
		// (set) Token: 0x0600C096 RID: 49302 RVA: 0x002ACD5C File Offset: 0x002AAF5C
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual TreeListSortExpressionCollection SortExpressions
		{
			get
			{
				if (this._sortExpressions == null)
				{
					this._sortExpressions = new TreeListSortExpressionCollection();
				}
				return this._sortExpressions;
			}
			protected set
			{
				this._sortExpressions = value;
			}
		}

		// Token: 0x0600C097 RID: 49303 RVA: 0x002ACF2C File Offset: 0x002AB12C
		public IList<TreeListSourceItem> Sort(IList<TreeListSourceItem> originalEnumerable)
		{
			Type itemType = this.ResolvedItemType;
			TFunc<IList<TreeListSourceItem>, IList<TreeListSourceItem>> tfunc = delegate(IList<TreeListSourceItem> input)
			{
				IEnumerable<TreeListSourceItem> enumerable = input;
				bool flag = true;
				using (IEnumerator enumerator = this.SortExpressions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TreeListSortExpression expression = (TreeListSortExpression)enumerator.Current;
						if (expression.SortOrder != TreeListSortOrder.None)
						{
							Type propertyType;
							if (this.CachedCalculatedColumn.ContainsKey(expression.FieldName))
							{
								TreeListSourceItem treeListSourceItem = (from i in input
								where i.CalculatedColumns[expression.FieldName] != null
								select i).FirstOrDefault<TreeListSourceItem>();
								if (treeListSourceItem == null)
								{
									continue;
								}
								propertyType = this.GetPropertyType(itemType, expression.FieldName, treeListSourceItem);
							}
							else
							{
								propertyType = this.GetPropertyType(itemType, expression.FieldName, null);
							}
							if (flag)
							{
								enumerable = this.SortByField(input, propertyType, expression.FieldName, this.IsDesending(expression.SortOrder));
								flag = false;
							}
							else
							{
								enumerable = this.ThenBy((IOrderedEnumerable<TreeListSourceItem>)enumerable, propertyType, expression.FieldName, this.IsDesending(expression.SortOrder));
							}
						}
					}
				}
				return new List<TreeListSourceItem>(enumerable);
			};
			return tfunc(originalEnumerable);
		}

		// Token: 0x0600C098 RID: 49304 RVA: 0x002ACF68 File Offset: 0x002AB168
		private IEnumerable<TreeListSourceItem> SortByField(IEnumerable<TreeListSourceItem> input, Type propertyType, string propertyName, bool sortOrder)
		{
			if (!TreeListTypeHelper.IsBindableType(propertyType))
			{
				return input;
			}
			bool allowStableSort = this.TreeListEnumerable.OwnerTreeList.AllowStableSort;
			if (propertyType == typeof(string))
			{
				return new OrderByEnumerable<TreeListSourceItem, string>(input, this.GetEvalFunc<string>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(int) || propertyType == typeof(int?))
			{
				return new OrderByEnumerable<TreeListSourceItem, int?>(input, this.GetEvalFunc<int?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(short) || propertyType == typeof(short?))
			{
				return new OrderByEnumerable<TreeListSourceItem, short?>(input, this.GetEvalFunc<short?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(long) || propertyType == typeof(long?))
			{
				return new OrderByEnumerable<TreeListSourceItem, long?>(input, this.GetEvalFunc<long?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
			{
				return new OrderByEnumerable<TreeListSourceItem, DateTime?>(input, this.GetEvalFunc<DateTime?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
			{
				return new OrderByEnumerable<TreeListSourceItem, decimal?>(input, this.GetEvalFunc<decimal?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(TimeSpan) || propertyType == typeof(TimeSpan?))
			{
				return new OrderByEnumerable<TreeListSourceItem, TimeSpan?>(input, this.GetEvalFunc<TimeSpan?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
			{
				return new OrderByEnumerable<TreeListSourceItem, Guid?>(input, this.GetEvalFunc<Guid?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(bool) || propertyType == typeof(bool?))
			{
				return new OrderByEnumerable<TreeListSourceItem, bool?>(input, this.GetEvalFunc<bool?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(double) || propertyType == typeof(double?))
			{
				return new OrderByEnumerable<TreeListSourceItem, double?>(input, this.GetEvalFunc<double?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(float) || propertyType == typeof(float?))
			{
				return new OrderByEnumerable<TreeListSourceItem, float?>(input, this.GetEvalFunc<float?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(float) || propertyType == typeof(float?))
			{
				return new OrderByEnumerable<TreeListSourceItem, float?>(input, this.GetEvalFunc<float?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(byte) || propertyType == typeof(byte?))
			{
				return new OrderByEnumerable<TreeListSourceItem, byte?>(input, this.GetEvalFunc<byte?>(propertyName), null, sortOrder, allowStableSort);
			}
			return input;
		}

		// Token: 0x0600C099 RID: 49305 RVA: 0x002AD240 File Offset: 0x002AB440
		private IEnumerable<TreeListSourceItem> ThenBy(IOrderedEnumerable<TreeListSourceItem> input, Type propertyType, string propertyName, bool sortOrder)
		{
			if (!TreeListTypeHelper.IsBindableType(propertyType))
			{
				return input;
			}
			bool allowStableSort = this.TreeListEnumerable.OwnerTreeList.AllowStableSort;
			if (propertyType == typeof(string))
			{
				return input.CreateOrderedEnumerable<string>(this.GetEvalFunc<string>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(int) || propertyType == typeof(int?))
			{
				return input.CreateOrderedEnumerable<int?>(this.GetEvalFunc<int?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(short) || propertyType == typeof(short?))
			{
				return input.CreateOrderedEnumerable<short?>(this.GetEvalFunc<short?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(long) || propertyType == typeof(long?))
			{
				return input.CreateOrderedEnumerable<long?>(this.GetEvalFunc<long?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
			{
				return input.CreateOrderedEnumerable<DateTime?>(this.GetEvalFunc<DateTime?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
			{
				return input.CreateOrderedEnumerable<decimal?>(this.GetEvalFunc<decimal?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(TimeSpan) || propertyType == typeof(TimeSpan?))
			{
				return input.CreateOrderedEnumerable<TimeSpan?>(this.GetEvalFunc<TimeSpan?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
			{
				return input.CreateOrderedEnumerable<Guid?>(this.GetEvalFunc<Guid?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(bool) || propertyType == typeof(bool?))
			{
				return input.CreateOrderedEnumerable<bool?>(this.GetEvalFunc<bool?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(double) || propertyType == typeof(double?))
			{
				return input.CreateOrderedEnumerable<double?>(this.GetEvalFunc<double?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(float) || propertyType == typeof(float?))
			{
				return input.CreateOrderedEnumerable<float?>(this.GetEvalFunc<float?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(float) || propertyType == typeof(float?))
			{
				return input.CreateOrderedEnumerable<float?>(this.GetEvalFunc<float?>(propertyName), null, sortOrder, allowStableSort);
			}
			return input;
		}

		// Token: 0x0600C09A RID: 49306 RVA: 0x002AD4DF File Offset: 0x002AB6DF
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private bool IsDesending(TreeListSortOrder sortOrder)
		{
			return sortOrder == TreeListSortOrder.Descending;
		}

		// Token: 0x0600C09B RID: 49307 RVA: 0x002AD4E8 File Offset: 0x002AB6E8
		private Type GetPropertyType(Type itemType, string propertyName, TreeListSourceItem itemToSort)
		{
			if (itemToSort != null && itemToSort.CalculatedColumns.Count > 0 && itemToSort.CalculatedColumns.ContainsKey(propertyName))
			{
				return itemToSort.CalculatedColumns[propertyName].GetType();
			}
			if (this.ItemEvaluator == null)
			{
				this.ItemEvaluator = new TreeListEnumerableHelper.TreeListDataItemEvaluator();
			}
			Type result = typeof(object);
			PropertyDescriptor propertyDescriptor = this.ItemEvaluator.FindProperty(propertyName);
			if (propertyDescriptor != null)
			{
				return propertyDescriptor.PropertyType;
			}
			PropertyInfo property = itemType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property != null)
			{
				result = property.PropertyType;
			}
			else
			{
				propertyDescriptor = this.ItemEvaluator.FindProperty(TreeListEnumerableHelper.firstDataItem, propertyName);
				if (propertyDescriptor != null)
				{
					result = propertyDescriptor.PropertyType;
				}
			}
			return result;
		}

		// Token: 0x0600C09C RID: 49308 RVA: 0x002AD5FC File Offset: 0x002AB7FC
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private TFunc<object, TResult> GetEvalFunc<TResult>(string propertyName)
		{
			return delegate(object element)
			{
				TreeListSourceItem treeListSourceItem = element as TreeListSourceItem;
				object obj = treeListSourceItem.IsCalculatedColumn(propertyName) ? treeListSourceItem.CalculatedColumns[propertyName] : DataBinder.Eval(treeListSourceItem.OriginalDataItem, propertyName);
				if (obj == Convert.DBNull)
				{
					return default(TResult);
				}
				return (TResult)((object)obj);
			};
		}

		// Token: 0x0600C09D RID: 49309 RVA: 0x002AD624 File Offset: 0x002AB824
		public int GetCount<TSource>(IEnumerable<TSource> source)
		{
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				return collection.Count;
			}
			return this.GetCount(source);
		}

		// Token: 0x0600C09E RID: 49310 RVA: 0x002AD64C File Offset: 0x002AB84C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public int GetCount(IEnumerable source)
		{
			if (source is ICollection)
			{
				return ((ICollection)source).Count;
			}
			if (source is Array)
			{
				return ((Array)source).Length;
			}
			int num = 0;
			foreach (object obj in source)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600C09F RID: 49311 RVA: 0x002AD6C4 File Offset: 0x002AB8C4
		public IList<TreeListSourceItem> RetrieveRootItems()
		{
			List<TreeListSourceItem> list = new List<TreeListSourceItem>();
			bool flag = true;
			foreach (object obj in this.OriginalSource)
			{
				if (flag)
				{
					this.TreeListEnumerable.Columns = new TreeListDataColumns(obj);
					flag = false;
					this.CacheCalculatedColumns();
				}
				if (this.ItemEvaluator.IsRootItem(obj, this.KeyNames, this.ParentKeyNames))
				{
					list.Add(this.BuildSourceItem(0, obj));
				}
				else
				{
					this.DataLeft.Add(obj);
				}
			}
			return list;
		}

		// Token: 0x0600C0A0 RID: 49312 RVA: 0x002AD770 File Offset: 0x002AB970
		protected TreeListSourceItem BuildSourceItem(int nestedLevel, object originalItem)
		{
			TreeListHierarchyIndex treeListHierarchyIndex = new TreeListHierarchyIndex
			{
				NestedLevel = nestedLevel,
				LevelIndex = this.GroupingContext.IndexGenerator.GenerateIndex(nestedLevel)
			};
			TreeListSourceItem treeListSourceItem = new TreeListSourceItem
			{
				HierarchyIndex = treeListHierarchyIndex,
				OriginalDataItem = originalItem
			};
			foreach (KeyValuePair<string, Delegate> keyValuePair in this.CachedCalculatedColumn)
			{
				treeListSourceItem.CalculatedColumns.Add(keyValuePair.Key, keyValuePair.Value.DynamicInvoke(new object[]
				{
					originalItem
				}));
			}
			if (this.reorderContext != null && this.reorderContext.ReorderStage == TreeListReorderContext.DataReorderStage.IndexAdjustmentStage)
			{
				ArrayList keyList = this.ItemEvaluator.ExtractKeyValues(originalItem, this.KeyNames);
				foreach (KeyValuePair<TreeListHierarchyIndex, ArrayList> keyValuePair2 in this.reorderContext.IndexToKeyValuesMapping)
				{
					if (this.KeysMatch(keyList, keyValuePair2.Value))
					{
						TreeListHierarchyIndex key = keyValuePair2.Key;
						this.reorderContext.ReplaceReorderedIndex(key, treeListHierarchyIndex);
						break;
					}
				}
			}
			return treeListSourceItem;
		}

		// Token: 0x0600C0A1 RID: 49313 RVA: 0x002AD8C8 File Offset: 0x002ABAC8
		private bool KeysMatch(ArrayList keyList1, ArrayList keyList2)
		{
			if (keyList1.Count != keyList2.Count)
			{
				return false;
			}
			for (int i = 0; i < keyList1.Count; i++)
			{
				if (keyList1[i] == null || !keyList1[i].Equals(keyList2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600C0A2 RID: 49314 RVA: 0x002AD917 File Offset: 0x002ABB17
		protected bool ShouldAddItemInResult(TreeListHierarchyIndex index)
		{
			if (this.ExpandedItems == null)
			{
				this.ExpandedItems = new HashSet<TreeListHierarchyIndex>(this.GroupingContext.ExpandedItems);
			}
			return this.ExpandedItems.Contains(index);
		}

		// Token: 0x0600C0A3 RID: 49315 RVA: 0x002AD944 File Offset: 0x002ABB44
		public ArrayList GetChildItems(object parentItem)
		{
			if (this.ItemsMap == null)
			{
				this.ItemsMap = new Dictionary<ArrayList, ArrayList>(new TreeListEnumerableHelper.DataItemKeysEqualityComparer());
				foreach (object obj in this.DataLeft)
				{
					ArrayList key = this.ItemEvaluator.ExtractKeyValues(obj, this.ParentKeyNames);
					ArrayList arrayList;
					if (!this.ItemsMap.TryGetValue(key, out arrayList))
					{
						arrayList = new ArrayList();
						this.ItemsMap.Add(key, arrayList);
					}
					arrayList.Add(obj);
				}
			}
			ArrayList key2 = this.ItemEvaluator.ExtractKeyValues(parentItem, this.KeyNames);
			ArrayList result;
			if (this.ItemsMap.TryGetValue(this.ItemEvaluator.ExtractKeyValues(parentItem, this.KeyNames), out result))
			{
				this.ItemsMap[key2] = new ArrayList();
				return result;
			}
			return new ArrayList();
		}

		// Token: 0x0600C0A4 RID: 49316 RVA: 0x002ADAC0 File Offset: 0x002ABCC0
		protected IList<TreeListSourceItem> GetChildItemsRecursive(TreeListSourceItem item)
		{
			List<TAction<IList<TreeListSourceItem>>> list = new List<TAction<IList<TreeListSourceItem>>>();
			ArrayList childItems = this.GetChildItems(item.OriginalDataItem);
			List<TreeListSourceItem> list2 = new List<TreeListSourceItem>(childItems.Count);
			if (TreeListEnumerableHelper.showFooter && TreeListAggregatesHelper.AggregatesSourceItemsCollection != null && TreeListAggregatesHelper.AggregatesSourceItemsCollection.Count == 0 && !TreeListAggregatesHelper.AggregatedSourceItems.ContainsKey(item.HierarchyIndex))
			{
				TreeListAggregatesHelper.AggregatedSourceItems.Add(item.HierarchyIndex, item);
			}
			int num = 0;
			foreach (object originalItem in childItems)
			{
				int nestedLevel = item.HierarchyIndex.NestedLevel + 1;
				TreeListSourceItem sourceItem = this.BuildSourceItem(nestedLevel, originalItem);
				TreeListSourceItem treeListSourceItem = new TreeListSourceItem();
				treeListSourceItem.HierarchyIndex = item.HierarchyIndex;
				sourceItem.ParentItem = treeListSourceItem;
				IList<TreeListSourceItem> subChildItems = this.GetChildItemsRecursive(sourceItem);
				sourceItem.ItemState.HasChildItems = (subChildItems.Count > 0);
				sourceItem.ItemState.ParentHierarchyIndex = item.HierarchyIndex;
				sourceItem.ChildItems = subChildItems;
				sourceItem.SiblingsCount = childItems.Count;
				sourceItem.ParentItem = item;
				sourceItem.ItemIndex = num;
				list2.Add(sourceItem);
				if (this.ShouldAddItemInResult(sourceItem.HierarchyIndex))
				{
					TAction<IList<TreeListSourceItem>> item2 = delegate(IList<TreeListSourceItem> parentsCollection)
					{
						int num2 = parentsCollection.IndexOf(sourceItem);
						foreach (TreeListSourceItem item3 in subChildItems)
						{
							parentsCollection.Insert(++num2, item3);
						}
					};
					list.Add(item2);
				}
				num++;
				this.TotalItemCount++;
			}
			List<TreeListSourceItem> sortedCollection = (List<TreeListSourceItem>)((list2.Count == 0) ? list2 : this.Sort(list2));
			list.ForEach(delegate(TAction<IList<TreeListSourceItem>> action)
			{
				action(sortedCollection);
			});
			return sortedCollection;
		}

		// Token: 0x0600C0A5 RID: 49317 RVA: 0x002ADEA4 File Offset: 0x002AC0A4
		public IEnumerable<TreeListSourceItem> FinalizeItemsState(IEnumerable<TreeListSourceItem> enumerable)
		{
			foreach (TreeListSourceItem item in enumerable)
			{
				item.ItemState.Siblings = this.HasSiblings(item, 0);
				yield return item;
			}
			yield break;
		}

		// Token: 0x17003E2C RID: 15916
		// (get) Token: 0x0600C0A6 RID: 49318 RVA: 0x002ADEC8 File Offset: 0x002AC0C8
		private Dictionary<TreeListHierarchyIndex, List<TreeListHierarchyIndex>> FooterItems
		{
			get
			{
				return this.TreeListEnumerable.OwnerTreeList.FooterItems;
			}
		}

		// Token: 0x0600C0A7 RID: 49319 RVA: 0x002ADEDC File Offset: 0x002AC0DC
		private void AppendRootItemsFooter(TreeListHierarchyIndex index, TreeListSourceItem item)
		{
			TreeListSourceItem parentItem = item.ParentItem;
			if (parentItem == null)
			{
				this.FooterItems[index].Add(new TreeListHierarchyIndex
				{
					NestedLevel = -1,
					LevelIndex = -1
				});
				return;
			}
			this.FooterItems[index].Add(parentItem.HierarchyIndex);
			this.AppendRootItemsFooter(index, parentItem);
		}

		// Token: 0x0600C0A8 RID: 49320 RVA: 0x002ADF3C File Offset: 0x002AC13C
		private void AppendChildItemsFooter(TreeListHierarchyIndex index, TreeListHierarchyIndex prevIndex, TreeListSourceItem item)
		{
			TreeListSourceItem parentItem = item.ParentItem;
			if (parentItem.HierarchyIndex.NestedLevel == prevIndex.NestedLevel)
			{
				this.FooterItems[index].Add(parentItem.HierarchyIndex);
				return;
			}
			this.FooterItems[index].Add(parentItem.HierarchyIndex);
			this.AppendChildItemsFooter(index, prevIndex, parentItem);
		}

		// Token: 0x0600C0A9 RID: 49321 RVA: 0x002ADF9C File Offset: 0x002AC19C
		internal void PrepareFooters(List<TreeListSourceItem> items)
		{
			TreeListSourceItem treeListSourceItem = null;
			int num = items.Count - 1;
			for (int i = num; i >= 0; i--)
			{
				TreeListSourceItem treeListSourceItem2 = items[i];
				if (treeListSourceItem == null)
				{
					treeListSourceItem = treeListSourceItem2;
					this.FooterItems.Add(treeListSourceItem2.HierarchyIndex, new List<TreeListHierarchyIndex>());
					this.AppendRootItemsFooter(treeListSourceItem2.HierarchyIndex, treeListSourceItem2);
				}
				else if (treeListSourceItem2.HierarchyIndex.NestedLevel <= treeListSourceItem.HierarchyIndex.NestedLevel)
				{
					treeListSourceItem = treeListSourceItem2;
				}
				else if (treeListSourceItem2.HierarchyIndex.NestedLevel > treeListSourceItem.HierarchyIndex.NestedLevel)
				{
					this.FooterItems.Add(treeListSourceItem2.HierarchyIndex, new List<TreeListHierarchyIndex>());
					this.AppendChildItemsFooter(treeListSourceItem2.HierarchyIndex, treeListSourceItem.HierarchyIndex, treeListSourceItem2);
					treeListSourceItem = treeListSourceItem2;
				}
			}
		}

		// Token: 0x0600C0AA RID: 49322 RVA: 0x002AE058 File Offset: 0x002AC258
		public IEnumerable<TreeListSourceItem> GetPage(IEnumerable<TreeListSourceItem> enumerable, int startIndex, int pageSize)
		{
			List<TreeListSourceItem> list = new List<TreeListSourceItem>();
			startIndex = Math.Max(startIndex, 0);
			int num = 0;
			foreach (TreeListSourceItem treeListSourceItem in enumerable)
			{
				if (num < startIndex)
				{
					num++;
				}
				else
				{
					treeListSourceItem.ItemState.Siblings = this.HasSiblings(treeListSourceItem, startIndex);
					list.Add(treeListSourceItem);
					num++;
					if (pageSize + startIndex == num)
					{
						break;
					}
				}
			}
			if (this.TreeListEnumerable.OwnerTreeList.ShowFooter)
			{
				this.PrepareFooters(list);
			}
			return list;
		}

		// Token: 0x0600C0AB RID: 49323 RVA: 0x002AE0F4 File Offset: 0x002AC2F4
		private TreeListSiblingState RetreieveRootItemSiblingState(TreeListSourceItem dataItem, int startIndex)
		{
			TreeListSiblingState result = default(TreeListSiblingState);
			if (dataItem.SiblingsCount == 1)
			{
				result.HasPrevPageSiblings = false;
				result.HasNextPageSiblings = false;
			}
			else
			{
				if (startIndex != 0 || this.reCalculatedIndexes[dataItem.HierarchyIndex].HierarchyIndex.LevelIndex != 0)
				{
					result.HasPrevPageSiblings = true;
				}
				result.HasNextPageSiblings = (this.reCalculatedIndexes[dataItem.HierarchyIndex].HierarchyIndex.LevelIndex < dataItem.SiblingsCount - 1);
			}
			return result;
		}

		// Token: 0x0600C0AC RID: 49324 RVA: 0x002AE180 File Offset: 0x002AC380
		protected List<TreeListSiblingState> HasSiblings(TreeListSourceItem dataItem, int startIndex)
		{
			List<TreeListSiblingState> list = new List<TreeListSiblingState>();
			if (this.reCalculatedIndexes[dataItem.HierarchyIndex].HierarchyIndex.NestedLevel == 0)
			{
				list.Add(this.RetreieveRootItemSiblingState(dataItem, startIndex));
			}
			else
			{
				Stack<TreeListSourceItem> stack = new Stack<TreeListSourceItem>(new TreeListSourceItem[]
				{
					dataItem
				});
				while (stack.Count > 0)
				{
					TreeListSourceItem treeListSourceItem = stack.Pop();
					if (treeListSourceItem == null)
					{
						break;
					}
					if (treeListSourceItem.HierarchyIndex.NestedLevel != -1)
					{
						stack.Push(treeListSourceItem.ParentItem);
						TreeListSiblingState item = default(TreeListSiblingState);
						if (this.reCalculatedIndexes[treeListSourceItem.HierarchyIndex].HierarchyIndex.NestedLevel != 0)
						{
							item.HasPrevPageSiblings = true;
							if (treeListSourceItem.SiblingsCount == 1)
							{
								item.HasNextPageSiblings = false;
							}
							else
							{
								bool flag = treeListSourceItem.SiblingsCount - 1 == this.reCalculatedIndexes[treeListSourceItem.HierarchyIndex].ItemIndex;
								item.HasNextPageSiblings = !flag;
							}
							list.Add(item);
						}
						else
						{
							list.Add(this.RetreieveRootItemSiblingState(treeListSourceItem, startIndex));
						}
					}
				}
				list.Reverse();
			}
			return list;
		}

		// Token: 0x0600C0AD RID: 49325 RVA: 0x002AE2AC File Offset: 0x002AC4AC
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
		internal string FormatExpression(List<string> types, string Expression, string UniqueName, string[] DataFields)
		{
			string result = string.Empty;
			try
			{
				List<string> list = new List<string>();
				int num = 0;
				foreach (string fieldName in DataFields)
				{
					string arg = TreeListEnumerableHelper.TransformDataFieldName(fieldName, this.ResolvedItemType);
					list.Add(string.Format("{0}({1})", types[num], arg));
					num++;
				}
				result = string.Format(Expression, list.ToArray());
			}
			catch (Exception)
			{
				throw new FormatException("Illegal Expression for column: " + UniqueName);
			}
			return result;
		}

		// Token: 0x0600C0AE RID: 49326 RVA: 0x002AE344 File Offset: 0x002AC544
		internal static string TransformDataFieldName(string fieldName, Type type)
		{
			if (type == typeof(DataRowView) || type == typeof(DataRow) || type.GetInterface("IDataRecord") != null)
			{
				fieldName = string.Format(CultureInfo.InvariantCulture, "iif(it[\"{0}\"] == Convert.DBNull, null, it[\"{0}\"])", new object[]
				{
					fieldName
				});
			}
			if (GridBaseDataList.IsBindableType(type))
			{
				fieldName = "it";
			}
			return fieldName;
		}

		// Token: 0x040032AE RID: 12974
		private TreeListSortExpressionCollection _sortExpressions;

		// Token: 0x040032AF RID: 12975
		internal static object firstDataItem;

		// Token: 0x040032B0 RID: 12976
		internal static Type resolvedItemType;

		// Token: 0x040032B1 RID: 12977
		private HashSet<Hashtable> ExpandedItemsDataKeyValuesHash;

		// Token: 0x040032B2 RID: 12978
		private TreeListDeleteItemsEnumerable _itemsToDelete;

		// Token: 0x040032B3 RID: 12979
		private bool isRecursilveDelete;

		// Token: 0x040032B4 RID: 12980
		private TreeListDeleteContext deleteContext;

		// Token: 0x040032B5 RID: 12981
		private List<TreeListSourceItem> _itemsToReorder;

		// Token: 0x040032B6 RID: 12982
		private TreeListReorderContext reorderContext;

		// Token: 0x040032B7 RID: 12983
		private Dictionary<TreeListHierarchyIndex, TreeListEnumerableHelper.CalculatedIndexes> reCalculatedIndexes = new Dictionary<TreeListHierarchyIndex, TreeListEnumerableHelper.CalculatedIndexes>();

		// Token: 0x040032B8 RID: 12984
		private Type ResolvedItemType;

		// Token: 0x040032B9 RID: 12985
		private Dictionary<string, Delegate> CachedCalculatedColumn;

		// Token: 0x040032BA RID: 12986
		private HashSet<TreeListHierarchyIndex> ExpandedItems;

		// Token: 0x040032BB RID: 12987
		private Dictionary<ArrayList, ArrayList> ItemsMap;

		// Token: 0x0200123B RID: 4667
		internal class ExpandedItemsDataKeyValuesHashEqualityComparer : IEqualityComparer<Hashtable>
		{
			// Token: 0x0600C0B0 RID: 49328 RVA: 0x002AE3B8 File Offset: 0x002AC5B8
			public bool Equals(Hashtable x, Hashtable y)
			{
				foreach (object key in x.Keys)
				{
					if (!y.ContainsKey(key) || !y[key].Equals(x[key]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600C0B1 RID: 49329 RVA: 0x002AE42C File Offset: 0x002AC62C
			public int GetHashCode(Hashtable obj)
			{
				if (obj == null)
				{
					return -1;
				}
				int num = 0;
				if (obj.Count > 0)
				{
					foreach (object obj2 in obj.Keys)
					{
						num ^= obj2.GetHashCode() + 1023 + obj[obj2].GetHashCode();
					}
				}
				return num;
			}
		}

		// Token: 0x0200123C RID: 4668
		private struct CalculatedIndexes
		{
			// Token: 0x17003E2D RID: 15917
			// (get) Token: 0x0600C0B3 RID: 49331 RVA: 0x002AE4B0 File Offset: 0x002AC6B0
			// (set) Token: 0x0600C0B4 RID: 49332 RVA: 0x002AE4B8 File Offset: 0x002AC6B8
			public TreeListHierarchyIndex HierarchyIndex { get; set; }

			// Token: 0x17003E2E RID: 15918
			// (get) Token: 0x0600C0B5 RID: 49333 RVA: 0x002AE4C1 File Offset: 0x002AC6C1
			// (set) Token: 0x0600C0B6 RID: 49334 RVA: 0x002AE4C9 File Offset: 0x002AC6C9
			public int ItemIndex { get; set; }
		}

		// Token: 0x0200123D RID: 4669
		private class DataItemKeysEqualityComparer : IEqualityComparer<ArrayList>
		{
			// Token: 0x0600C0B7 RID: 49335 RVA: 0x002AE4D4 File Offset: 0x002AC6D4
			public bool Equals(ArrayList x, ArrayList y)
			{
				if (x.Count == y.Count)
				{
					for (int i = 0; i < x.Count; i++)
					{
						if (!object.Equals(x[i], y[i]))
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x0600C0B8 RID: 49336 RVA: 0x002AE51C File Offset: 0x002AC71C
			public int GetHashCode(ArrayList obj)
			{
				if (obj != null)
				{
					int num = 7;
					foreach (object obj2 in obj)
					{
						num = num * 23 + ((obj2 != null) ? obj2.GetHashCode() : 0);
					}
					return num;
				}
				return 0;
			}
		}

		// Token: 0x0200123E RID: 4670
		public class TreeListDataItemEvaluator
		{
			// Token: 0x0600C0BA RID: 49338 RVA: 0x002AE588 File Offset: 0x002AC788
			public TreeListDataItemEvaluator()
			{
				this._propertyDescriptorCache = new Dictionary<string, PropertyDescriptor>();
				this._propertyTypeDefaultValueCache = new Dictionary<Type, object>();
			}

			// Token: 0x0600C0BB RID: 49339 RVA: 0x002AE5A8 File Offset: 0x002AC7A8
			public ArrayList ExtractKeyValues(object sourceItem, IList<string> keyNames)
			{
				ArrayList arrayList = new ArrayList();
				foreach (string text in keyNames)
				{
					if (text.Contains("."))
					{
						string[] array = text.Split(new string[]
						{
							"."
						}, StringSplitOptions.RemoveEmptyEntries);
						int num = 0;
						object obj = sourceItem;
						PropertyDescriptor propertyDescriptor = this.FindItemPoperty(obj, array[num]);
						while (propertyDescriptor != null && num < array.Length - 1)
						{
							obj = propertyDescriptor.GetValue(obj);
							propertyDescriptor = this.FindItemPoperty(obj, array[++num]);
						}
						arrayList.Add(propertyDescriptor.GetValue(obj));
					}
					else
					{
						PropertyDescriptor propertyDescriptor = this.FindItemPoperty(sourceItem, text);
						arrayList.Add(propertyDescriptor.GetValue(sourceItem));
					}
				}
				return arrayList;
			}

			// Token: 0x0600C0BC RID: 49340 RVA: 0x002AE68C File Offset: 0x002AC88C
			private PropertyDescriptor FindItemPoperty(object sourceItem, string property)
			{
				PropertyDescriptor propertyDescriptor = null;
				if (!this.TryGetProperty(property, out propertyDescriptor))
				{
					propertyDescriptor = TypeDescriptor.GetProperties(sourceItem).Find(property, false);
					if (propertyDescriptor == null)
					{
						throw new ArgumentException(string.Format("{0} property does not exist on object of type {1}.", property, sourceItem.GetType().FullName));
					}
					this.AddDescriptorToCache(property, propertyDescriptor);
				}
				return propertyDescriptor;
			}

			// Token: 0x0600C0BD RID: 49341 RVA: 0x002AE6DC File Offset: 0x002AC8DC
			public PropertyDescriptor FindProperty(string propertyName)
			{
				PropertyDescriptor result = null;
				this.TryGetProperty(propertyName, out result);
				return result;
			}

			// Token: 0x0600C0BE RID: 49342 RVA: 0x002AE6F8 File Offset: 0x002AC8F8
			public PropertyDescriptor FindProperty(object itemInstance, string propertyName)
			{
				PropertyDescriptor result = null;
				if (!this.TryGetProperty(propertyName, out result))
				{
					result = TypeDescriptor.GetProperties(itemInstance).Find(propertyName, false);
				}
				return result;
			}

			// Token: 0x0600C0BF RID: 49343 RVA: 0x002AE724 File Offset: 0x002AC924
			private bool TryGetProperty(string property, out PropertyDescriptor descriptor)
			{
				descriptor = null;
				if (!property.Contains("."))
				{
					this._propertyDescriptorCache.TryGetValue(property, out descriptor);
				}
				else
				{
					string[] array = property.Split(new string[]
					{
						"."
					}, StringSplitOptions.RemoveEmptyEntries);
					this._propertyDescriptorCache.TryGetValue(array[array.Length - 1], out descriptor);
				}
				return descriptor != null;
			}

			// Token: 0x0600C0C0 RID: 49344 RVA: 0x002AE785 File Offset: 0x002AC985
			private void AddDescriptorToCache(string property, PropertyDescriptor descriptor)
			{
				this._propertyDescriptorCache.Add(property, descriptor);
			}

			// Token: 0x0600C0C1 RID: 49345 RVA: 0x002AE794 File Offset: 0x002AC994
			public object TypeDefaultValue(object sourceItem, string propertyName)
			{
				return this.TypeDefaultValue(this.FindItemPoperty(sourceItem, propertyName));
			}

			// Token: 0x0600C0C2 RID: 49346 RVA: 0x002AE7A4 File Offset: 0x002AC9A4
			public object TypeDefaultValue(PropertyDescriptor descriptor)
			{
				object obj = null;
				if (descriptor.PropertyType.IsValueType && !this._propertyTypeDefaultValueCache.TryGetValue(descriptor.PropertyType, out obj))
				{
					obj = Activator.CreateInstance(descriptor.PropertyType);
					this._propertyTypeDefaultValueCache.Add(descriptor.PropertyType, obj);
				}
				return obj;
			}

			// Token: 0x0600C0C3 RID: 49347 RVA: 0x002AE7F4 File Offset: 0x002AC9F4
			public bool IsRootItem(object sourceItem, IList<string> keyProperties, IList<string> parentKeyProperties)
			{
				if (sourceItem == null)
				{
					throw new ArgumentNullException("sourceItem");
				}
				if (keyProperties == null || keyProperties.Count == 0)
				{
					throw new ArgumentNullException("keyProperties");
				}
				if (parentKeyProperties == null || parentKeyProperties.Count == 0)
				{
					throw new ArgumentNullException("parentKeyProperties");
				}
				bool result = false;
				ArrayList arrayList = this.ExtractKeyValues(sourceItem, keyProperties);
				ArrayList arrayList2 = this.ExtractKeyValues(sourceItem, parentKeyProperties);
				int num = -1;
				foreach (object obj in arrayList2)
				{
					num++;
					if (obj == null || obj is DBNull || (obj is string && string.IsNullOrEmpty(obj.ToString())))
					{
						result = true;
					}
					else if (object.Equals(obj, this.TypeDefaultValue(sourceItem, parentKeyProperties[num])))
					{
						result = true;
					}
					else
					{
						if (!keyProperties[num].Contains(".") || !object.Equals(obj, arrayList[num]))
						{
							result = false;
							break;
						}
						result = true;
					}
				}
				return result;
			}

			// Token: 0x0600C0C4 RID: 49348 RVA: 0x002AE90C File Offset: 0x002ACB0C
			public bool IsChildOf(object parentItem, object childItem, IList<string> keyNames, IList<string> parentKeyNames)
			{
				bool result = false;
				ArrayList arrayList = this.ExtractKeyValues(parentItem, keyNames);
				ArrayList arrayList2 = this.ExtractKeyValues(childItem, parentKeyNames);
				int num = 0;
				foreach (object objA in arrayList)
				{
					if (!object.Equals(objA, arrayList2[num]))
					{
						result = false;
						break;
					}
					result = true;
					num++;
				}
				return result;
			}

			// Token: 0x040032C8 RID: 13000
			private readonly Dictionary<string, PropertyDescriptor> _propertyDescriptorCache;

			// Token: 0x040032C9 RID: 13001
			private readonly Dictionary<Type, object> _propertyTypeDefaultValueCache;
		}
	}
}
