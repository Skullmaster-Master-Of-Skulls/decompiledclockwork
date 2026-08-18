using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Layouts
{
	// Token: 0x02000CEA RID: 3306
	internal class CompactLayout : BaseLayout
	{
		// Token: 0x06007B7B RID: 31611 RVA: 0x001C551F File Offset: 0x001C371F
		public CompactLayout(IHierarchyAdapter adapter)
		{
			if (adapter == null)
			{
				throw new ArgumentNullException("adapter", "Adapter cannot be null.");
			}
			this.adapter = adapter;
			this.collapsedSlotsTable = new IndexToValueTable<bool>();
			this.groupHeadersTable = new IndexToValueTable<GroupInfo>();
		}

		// Token: 0x1700277B RID: 10107
		// (get) Token: 0x06007B7C RID: 31612 RVA: 0x001C5557 File Offset: 0x001C3757
		public override int VisibleLineCount
		{
			get
			{
				return this.visibleLineCount;
			}
		}

		// Token: 0x06007B7D RID: 31613 RVA: 0x001C56F4 File Offset: 0x001C38F4
		public override IEnumerable<IList<ItemInfo>> GetAllLines()
		{
			this.generateAll = true;
			for (int slot = 0; slot < this.totalCount; slot++)
			{
				yield return this.GetItemInfosAtSlot(slot, slot);
			}
			yield break;
			yield break;
		}

		// Token: 0x06007B7E RID: 31614 RVA: 0x001C5928 File Offset: 0x001C3B28
		public override IEnumerable<IList<ItemInfo>> GetLines(int line, bool forward)
		{
			if (this.VisibleLineCount != 0 && line >= 0 && line < this.VisibleLineCount)
			{
				int slot = this.GetVisibleSlot(line);
				yield return this.GetItemInfosAtSlot(line, slot);
				if (forward)
				{
					for (;;)
					{
						slot = this.GetNextVisibleSlot(slot);
						line++;
						if (slot >= this.totalCount)
						{
							break;
						}
						yield return this.GetItemInfosAtSlot(line, slot);
					}
				}
				else
				{
					for (;;)
					{
						slot = this.GetPreviousVisibleSlot(slot);
						line--;
						if (slot < 0)
						{
							break;
						}
						yield return this.GetItemInfosAtSlot(line, slot);
					}
				}
			}
			yield break;
		}

		// Token: 0x06007B7F RID: 31615 RVA: 0x001C5954 File Offset: 0x001C3B54
		public override void Expand(object item)
		{
			GroupInfo groupInfo = this.GetGroupInfo(item);
			bool flag = this.IsGroupCollapsed(groupInfo);
			if (flag)
			{
				groupInfo.IsExpanded = true;
				if (groupInfo.IsVisible())
				{
					int num = 0;
					int num2 = 0;
					this.GetCollapseRange(groupInfo, out num, out num2);
					this.collapsedSlotsTable.RemoveValues(num, num2);
					int num3 = num2;
					foreach (GroupInfo groupInfo2 in this.CollapsedChildItems(groupInfo.Item))
					{
						this.GetCollapseRange(groupInfo2, out num, out num2);
						num3 -= num2;
						this.collapsedSlotsTable.AddValues(num, num2, true);
					}
					this.visibleLineCount += num3;
					int visibleSlot = this.GetVisibleSlot(num);
					base.RaiseExpanded(new ExpandCollapseEventArgs(item, visibleSlot, num3));
				}
			}
		}

		// Token: 0x06007B80 RID: 31616 RVA: 0x001C5A38 File Offset: 0x001C3C38
		public override void Collapse(object item)
		{
			GroupInfo groupInfo = this.GetGroupInfo(item);
			if (groupInfo != null && groupInfo.IsExpanded && this.IsCollapsible(groupInfo))
			{
				groupInfo.IsExpanded = false;
				if (groupInfo.IsVisible())
				{
					int num = 0;
					int num2 = 0;
					this.GetCollapseRange(groupInfo, out num, out num2);
					int num3 = num2 - this.GetCollapsedSlotsCount(num, num + num2 - 1);
					this.collapsedSlotsTable.AddValues(num, num2, true);
					this.visibleLineCount -= num3;
					int visibleSlot = this.GetVisibleSlot(num);
					base.RaiseCollapsed(new ExpandCollapseEventArgs(item, visibleSlot, num3));
				}
			}
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x001C5AC4 File Offset: 0x001C3CC4
		public override bool IsCollapsed(object item)
		{
			GroupInfo groupInfo = this.GetGroupInfo(item);
			return this.IsGroupCollapsed(groupInfo);
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x001C5AE0 File Offset: 0x001C3CE0
		internal virtual int GetLayoutLevel(ItemInfo itemInfo, GroupInfo parentGroupInfo)
		{
			return 0;
		}

		// Token: 0x06007B83 RID: 31619 RVA: 0x001C5AE3 File Offset: 0x001C3CE3
		internal virtual int GetIndent(ItemInfo itemInfo, GroupInfo parentGroupInfo)
		{
			if (itemInfo.ItemType != GroupType.Subtotal)
			{
				return itemInfo.Level;
			}
			if (this.IsGroupCollapsed(parentGroupInfo))
			{
				return base.AggregatesLevel;
			}
			return itemInfo.Level - 1;
		}

		// Token: 0x06007B84 RID: 31620 RVA: 0x001C5B10 File Offset: 0x001C3D10
		protected override void SetItemsSourceOverride(IReadOnlyList<object> source)
		{
			this.collapsedSlotsTable.Clear();
			this.groupHeadersTable.Clear();
			if (this.itemInfoTable != null)
			{
				this.itemInfoTable.Clear();
			}
			int groupLevels = base.GroupLevels;
			int num = 0;
			bool flag = false;
			foreach (object item in source)
			{
				int num2 = this.CountAndPopulateTables(item, num, 0, groupLevels, null, flag);
				flag = (flag || num2 > 1);
				num += num2;
			}
			if (this.groupHeadersTable.IsEmpty)
			{
				num = source.Count;
			}
			this.totalCount = (this.visibleLineCount = num);
		}

		// Token: 0x06007B85 RID: 31621 RVA: 0x001C5BD0 File Offset: 0x001C3DD0
		private IList<ItemInfo> GetItemInfosAtSlot(int visibleLine, int slot)
		{
			List<ItemInfo> list = new List<ItemInfo>();
			ItemInfo itemInfo = default(ItemInfo);
			itemInfo.IsDisplayed = true;
			GroupInfo groupInfo;
			int num;
			if (this.groupHeadersTable.TryGetValue(slot, out groupInfo, out num))
			{
				if (num != slot)
				{
					int num2 = slot - num - 1;
					itemInfo.Id = groupInfo.Index + num2 + 1;
					itemInfo.Item = this.adapter.GetItemAt(groupInfo.Item, num2);
					itemInfo.Level = groupInfo.Level + 1;
					itemInfo.Slot = itemInfo.Id;
					itemInfo.IsCollapsible = false;
					itemInfo.IsCollapsed = false;
					itemInfo.ItemType = BaseLayout.GetItemType(itemInfo.Item);
					itemInfo.IsSummaryVisible = (itemInfo.ItemType == GroupType.Subtotal && this.IsCollapsed(groupInfo.Item));
					itemInfo.LayoutInfo = this.GenerateLayoutInfo(itemInfo, groupInfo, visibleLine);
				}
				else
				{
					itemInfo.Id = groupInfo.Index;
					itemInfo.Item = groupInfo.Item;
					itemInfo.Level = groupInfo.Level;
					itemInfo.Slot = itemInfo.Id;
					itemInfo.IsCollapsible = this.IsCollapsible(groupInfo);
					itemInfo.IsCollapsed = this.IsGroupCollapsed(groupInfo);
					itemInfo.ItemType = BaseLayout.GetItemType(itemInfo.Item);
					itemInfo.IsSummaryVisible = (itemInfo.ItemType == GroupType.Subtotal && groupInfo.Parent != null && this.IsCollapsed(groupInfo.Parent.Item));
					itemInfo.LayoutInfo = this.GenerateLayoutInfo(itemInfo, groupInfo.Parent, visibleLine);
				}
				list.Add(itemInfo);
			}
			else if (base.ItemsSource != null)
			{
				itemInfo.Item = base.ItemsSource[visibleLine];
				itemInfo.Level = 0;
				itemInfo.Id = slot;
				itemInfo.Slot = itemInfo.Id;
				itemInfo.IsCollapsible = false;
				itemInfo.IsCollapsed = false;
				itemInfo.ItemType = BaseLayout.GetItemType(itemInfo.Item);
				itemInfo.IsSummaryVisible = false;
				itemInfo.LayoutInfo = this.GenerateLayoutInfo(itemInfo, null, visibleLine);
				list.Add(itemInfo);
			}
			return list;
		}

		// Token: 0x06007B86 RID: 31622 RVA: 0x001C5DE2 File Offset: 0x001C3FE2
		private bool IsGroupCollapsed(GroupInfo groupInfo)
		{
			return !this.generateAll && groupInfo != null && !groupInfo.IsExpanded;
		}

		// Token: 0x06007B87 RID: 31623 RVA: 0x001C5DFC File Offset: 0x001C3FFC
		private bool IsCollapsible(GroupInfo groupInfo)
		{
			int level = groupInfo.Level;
			bool flag = this.adapter.GetItems(groupInfo.Item).Any<object>();
			if (base.TotalsCount <= 1)
			{
				return flag;
			}
			if (base.AggregatesLevel >= base.GroupLevels - 1)
			{
				return flag && level < base.GroupLevels - 2;
			}
			return flag && level != base.AggregatesLevel;
		}

		// Token: 0x06007B88 RID: 31624 RVA: 0x001C5E64 File Offset: 0x001C4064
		private LayoutInfo GenerateLayoutInfo(ItemInfo itemInfo, GroupInfo parentGroupInfo, int visibleLine)
		{
			return new LayoutInfo
			{
				Indent = this.GetIndent(itemInfo, parentGroupInfo),
				Line = visibleLine,
				LineSpan = 1,
				LevelSpan = 1,
				SpansThroughCells = true,
				Level = this.GetLayoutLevel(itemInfo, parentGroupInfo)
			};
		}

		// Token: 0x06007B89 RID: 31625 RVA: 0x001C5EB8 File Offset: 0x001C40B8
		private int GetPreviousVisibleSlot(int slot)
		{
			return this.collapsedSlotsTable.GetPreviousGap(slot);
		}

		// Token: 0x06007B8A RID: 31626 RVA: 0x001C5EC6 File Offset: 0x001C40C6
		private int GetNextVisibleSlot(int slot)
		{
			return this.collapsedSlotsTable.GetNextGap(slot);
		}

		// Token: 0x06007B8B RID: 31627 RVA: 0x001C5ED4 File Offset: 0x001C40D4
		private GroupInfo GetGroupInfo(object item)
		{
			GroupInfo result;
			if (this.itemInfoTable != null && this.itemInfoTable.TryGetValue(item, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06007B8C RID: 31628 RVA: 0x001C5EFC File Offset: 0x001C40FC
		private int GetVisibleSlot(int index)
		{
			return this.collapsedSlotsTable.CountNextNotIncludedIndexes(0, index);
		}

		// Token: 0x06007B8D RID: 31629 RVA: 0x001C5F0C File Offset: 0x001C410C
		private void GetCollapseRange(GroupInfo groupInfo, out int slot, out int slotSpan)
		{
			int index = groupInfo.Index;
			int level = groupInfo.Level;
			slot = index + 1;
			slotSpan = groupInfo.GetLineSpan() - 1;
			int num = base.ShowAggregateValuesInline ? (base.AggregatesLevel - 1) : base.AggregatesLevel;
			if (base.TotalsCount > 1 && level < num)
			{
				switch (base.TotalsPosition)
				{
				case TotalsPosition.Last:
					slotSpan -= base.TotalsCount;
					return;
				case TotalsPosition.First:
				case TotalsPosition.Inline:
					slot += base.TotalsCount;
					slotSpan -= base.TotalsCount;
					break;
				case TotalsPosition.None:
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06007B8E RID: 31630 RVA: 0x001C5F9C File Offset: 0x001C419C
		private int GetCollapsedSlotsCount(int startSlot, int endSlot)
		{
			return this.collapsedSlotsTable.GetIndexCount(startSlot, endSlot);
		}

		// Token: 0x06007B8F RID: 31631 RVA: 0x001C5FAC File Offset: 0x001C41AC
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Not a real issue.")]
		private int CountAndPopulateTables(object item, int rootSlot, int level, int levels, GroupInfo parent, bool shouldIndexItem)
		{
			int num = 1;
			IEnumerable<object> items = this.adapter.GetItems(item);
			GroupInfo groupInfo = null;
			if (level < levels - 1)
			{
				bool flag = false;
				foreach (object item2 in items)
				{
					if (groupInfo == null)
					{
						groupInfo = new GroupInfo(item, parent, true, level, rootSlot, rootSlot + num - 1);
					}
					int num2 = this.CountAndPopulateTables(item2, rootSlot + num, level + 1, levels, groupInfo, flag);
					flag = (flag || num2 > 1);
					num += num2;
				}
			}
			shouldIndexItem = (shouldIndexItem || num > 1);
			if (shouldIndexItem)
			{
				if (groupInfo == null)
				{
					groupInfo = new GroupInfo(item, parent, true, level, rootSlot, rootSlot + num - 1);
				}
				else
				{
					groupInfo.LastSubItemSlot = rootSlot + num - 1;
				}
				this.groupHeadersTable.AddValue(rootSlot, groupInfo);
				if (this.itemInfoTable == null)
				{
					this.itemInfoTable = new Dictionary<object, GroupInfo>();
				}
				this.itemInfoTable.Add(item, groupInfo);
			}
			return num;
		}

		// Token: 0x06007B90 RID: 31632 RVA: 0x001C6394 File Offset: 0x001C4594
		private IEnumerable<GroupInfo> CollapsedChildItems(object item)
		{
			GroupInfo groupInfo = this.GetGroupInfo(item);
			if (groupInfo != null)
			{
				if (!groupInfo.IsExpanded)
				{
					yield return groupInfo;
				}
				else if (this.adapter.GetItems(item).Any<object>() && groupInfo.Level < base.GroupLevels - 2)
				{
					IEnumerable<object> items = this.adapter.GetItems(item);
					foreach (object childGroup in items)
					{
						foreach (GroupInfo collapsedChildGroup in this.CollapsedChildItems(childGroup))
						{
							yield return collapsedChildGroup;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x040021CE RID: 8654
		private int totalCount;

		// Token: 0x040021CF RID: 8655
		private int visibleLineCount;

		// Token: 0x040021D0 RID: 8656
		private bool generateAll;

		// Token: 0x040021D1 RID: 8657
		private Dictionary<object, GroupInfo> itemInfoTable;

		// Token: 0x040021D2 RID: 8658
		private IndexToValueTable<bool> collapsedSlotsTable;

		// Token: 0x040021D3 RID: 8659
		private IndexToValueTable<GroupInfo> groupHeadersTable;

		// Token: 0x040021D4 RID: 8660
		private IHierarchyAdapter adapter;
	}
}
