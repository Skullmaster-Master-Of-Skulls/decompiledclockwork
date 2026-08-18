using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Layouts
{
	// Token: 0x02000CF1 RID: 3313
	internal class TabularLayout : BaseLayout
	{
		// Token: 0x06007BBB RID: 31675 RVA: 0x001C65DB File Offset: 0x001C47DB
		public TabularLayout(IHierarchyAdapter adapter)
		{
			if (adapter == null)
			{
				throw new ArgumentNullException("adapter", "Adapter cannot be null.");
			}
			this.adapter = adapter;
			this.collapsedSlotsTable = new IndexToValueTable<bool>();
			this.groupHeadersTable = new IndexToValueTable<TabularGroupInfo>();
		}

		// Token: 0x1700278C RID: 10124
		// (get) Token: 0x06007BBC RID: 31676 RVA: 0x001C6613 File Offset: 0x001C4813
		public override int VisibleLineCount
		{
			get
			{
				return this.visibleLineCount;
			}
		}

		// Token: 0x06007BBD RID: 31677 RVA: 0x001C67B0 File Offset: 0x001C49B0
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

		// Token: 0x06007BBE RID: 31678 RVA: 0x001C6A08 File Offset: 0x001C4C08
		public override IEnumerable<IList<ItemInfo>> GetLines(int line, bool forward)
		{
			if (this.VisibleLineCount != 0 && line >= 0 && line < this.VisibleLineCount)
			{
				int slot = this.GetVisibleSlot(line);
				IList<ItemInfo> results = this.GetItemInfosAtSlot(line, slot);
				yield return results;
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
						results = this.GetItemInfosAtSlot(line, slot);
						yield return results;
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
						results = this.GetItemInfosAtSlot(line, slot);
						yield return results;
					}
				}
			}
			yield break;
		}

		// Token: 0x06007BBF RID: 31679 RVA: 0x001C6A34 File Offset: 0x001C4C34
		public override void Expand(object item)
		{
			TabularGroupInfo groupInfo = this.GetGroupInfo(item);
			bool flag = this.IsGroupCollapsed(groupInfo);
			if (flag)
			{
				groupInfo.IsExpanded = true;
				if (groupInfo.IsVisible())
				{
					int num = 0;
					int num2 = 0;
					this.GetCollapseRange(groupInfo, out num, out num2);
					if (num2 > 0)
					{
						this.collapsedSlotsTable.RemoveValues(num, num2);
					}
					int num3 = num2;
					foreach (TabularGroupInfo groupInfo2 in this.CollapsedChildItems(groupInfo.Item))
					{
						this.GetCollapseRange(groupInfo2, out num, out num2);
						num3 -= num2;
						if (num2 > 0)
						{
							this.collapsedSlotsTable.AddValues(num, num2, true);
						}
					}
					this.visibleLineCount += num3;
					int visibleSlot = this.GetVisibleSlot(num);
					base.RaiseExpanded(new ExpandCollapseEventArgs(item, visibleSlot, num3));
				}
			}
		}

		// Token: 0x06007BC0 RID: 31680 RVA: 0x001C6B20 File Offset: 0x001C4D20
		public override void Collapse(object item)
		{
			TabularGroupInfo groupInfo = this.GetGroupInfo(item);
			if (groupInfo != null && groupInfo.IsExpanded && this.IsCollapsible(groupInfo))
			{
				groupInfo.IsExpanded = false;
				if (groupInfo.IsVisible())
				{
					int num = 0;
					int num2 = 0;
					this.GetCollapseRange(groupInfo, out num, out num2);
					int num3 = num2 - this.GetCollapsedSlotsCount(num, num + num2 - 1);
					if (num2 > 0)
					{
						this.collapsedSlotsTable.AddValues(num, num2, true);
						this.visibleLineCount -= num3;
					}
					int visibleSlot = this.GetVisibleSlot(num);
					base.RaiseCollapsed(new ExpandCollapseEventArgs(item, visibleSlot, num3));
				}
			}
		}

		// Token: 0x06007BC1 RID: 31681 RVA: 0x001C6BB0 File Offset: 0x001C4DB0
		public override bool IsCollapsed(object item)
		{
			TabularGroupInfo groupInfo = this.GetGroupInfo(item);
			return this.IsGroupCollapsed(groupInfo);
		}

		// Token: 0x06007BC2 RID: 31682 RVA: 0x001C6BCC File Offset: 0x001C4DCC
		protected override void SetItemsSourceOverride(IReadOnlyList<object> source)
		{
			this.collapsedSlotsTable.Clear();
			this.groupHeadersTable.Clear();
			if (this.itemInfoTable != null)
			{
				this.itemInfoTable.Clear();
			}
			int num = 0;
			int num2 = 0;
			if (base.GroupLevels > 1)
			{
				bool flag = false;
				foreach (object item in source)
				{
					bool flag2 = false;
					int num3 = this.CountAndPopulateTables(item, num, 0, base.GroupLevels, null, flag, out flag2, ref num2);
					flag = (flag || num3 > 1 || this.adapter.GetItems(item).Any<object>());
					num += num3;
					num2++;
				}
			}
			if (this.groupHeadersTable.IsEmpty)
			{
				num = source.Count;
			}
			this.totalCount = (this.visibleLineCount = num);
		}

		// Token: 0x06007BC3 RID: 31683 RVA: 0x001C6CB0 File Offset: 0x001C4EB0
		private int GetPreviousVisibleSlot(int slot)
		{
			return this.collapsedSlotsTable.GetPreviousGap(slot);
		}

		// Token: 0x06007BC4 RID: 31684 RVA: 0x001C6CBE File Offset: 0x001C4EBE
		private int GetNextVisibleSlot(int slot)
		{
			return this.collapsedSlotsTable.GetNextGap(slot);
		}

		// Token: 0x06007BC5 RID: 31685 RVA: 0x001C6D14 File Offset: 0x001C4F14
		private IList<ItemInfo> GetItemInfosAtSlot(int visibleLine, int slot)
		{
			List<ItemInfo> list = new List<ItemInfo>();
			TabularGroupInfo tabularGroupInfo;
			int num;
			if (!this.groupHeadersTable.TryGetValue(slot, out tabularGroupInfo, out num) && base.ItemsSource != null && this.itemInfoTable != null)
			{
				object key = base.ItemsSource[slot];
				this.itemInfoTable.TryGetValue(key, out tabularGroupInfo);
			}
			if (tabularGroupInfo != null)
			{
				TabularGroupInfo tabularGroupInfo2 = tabularGroupInfo;
				while (tabularGroupInfo2 != null)
				{
					ItemInfo item = this.GenerateItemInfo(slot, tabularGroupInfo2);
					tabularGroupInfo2 = tabularGroupInfo2.Parent;
					list.Insert(0, item);
				}
			}
			else if (base.ItemsSource != null)
			{
				ItemInfo itemInfo = default(ItemInfo);
				itemInfo.IsDisplayed = true;
				itemInfo.Id = slot;
				itemInfo.Item = base.ItemsSource[slot];
				itemInfo.ItemType = BaseLayout.GetItemType(itemInfo.Item);
				itemInfo.Level = 0;
				itemInfo.Slot = slot;
				itemInfo.IsCollapsible = false;
				itemInfo.IsCollapsed = false;
				itemInfo.IsSummaryVisible = false;
				itemInfo.LayoutInfo = this.GenerateLayoutInfo(itemInfo, null);
				list.Add(itemInfo);
			}
			ItemInfo item2 = list[list.Count - 1];
			object obj = item2.Item;
			if (this.adapter.GetItems(obj).Any<object>())
			{
				int num2 = (num == -1) ? 0 : (slot - num);
				int id = item2.Id + num2 + 1;
				obj = this.adapter.GetItemAt(obj, num2);
				TabularGroupInfo groupInfo = this.GetGroupInfo(obj);
				item2 = this.GenerateItemInfo(id, slot, item2.Level + 1, obj, groupInfo, item2.IsCollapsed);
				list.Add(item2);
			}
			while (obj != null && this.adapter.GetItems(obj).Any<object>())
			{
				obj = this.adapter.GetItemAt(obj, 0);
				TabularGroupInfo groupInfo2 = this.GetGroupInfo(obj);
				item2 = this.GenerateItemInfo(item2.Id + 1, slot, item2.Level + 1, obj, groupInfo2, item2.IsCollapsed);
				list.Add(item2);
			}
			int num3 = 0;
			while (num3 < list.Count - 1 && (!this.IsCollapsed(list[num3].Item) || (num3 + 1 < list.Count && list[num3 + 1].IsSummaryVisible)))
			{
				num3++;
			}
			list = list.GetRange(0, num3 + 1);
			ItemInfo itemInfo2 = list[list.Count - 1];
			if (itemInfo2.IsCollapsed)
			{
				int num4 = visibleLine - itemInfo2.LayoutInfo.Line;
				int num5 = base.ShowAggregateValuesInline ? (base.AggregatesLevel - 1) : base.AggregatesLevel;
				int num6 = -1;
				if (itemInfo2.Level < num5)
				{
					switch (base.TotalsPosition)
					{
					case TotalsPosition.Last:
					{
						int num7 = this.adapter.GetItems(itemInfo2.Item).Count<object>();
						num6 = num7 - base.TotalsCount + num4;
						break;
					}
					case TotalsPosition.First:
						num6 = num4;
						break;
					}
				}
				if (num6 >= 0)
				{
					object itemAt = this.adapter.GetItemAt(itemInfo2.Item, num6);
					TabularGroupInfo groupInfo3 = this.GetGroupInfo(itemAt);
					ItemInfo item3 = this.GenerateItemInfo(slot, groupInfo3);
					item3.IsSummaryVisible = true;
					item3.LayoutInfo.Level = base.AggregatesLevel;
					list.Add(item3);
				}
			}
			return (from i in list
			where i.LayoutInfo.Line <= visibleLine && i.LayoutInfo.Line + i.LayoutInfo.LineSpan - 1 >= visibleLine
			select i).ToList<ItemInfo>();
		}

		// Token: 0x06007BC6 RID: 31686 RVA: 0x001C7080 File Offset: 0x001C5280
		private ItemInfo GenerateItemInfo(int slot, TabularGroupInfo groupInfo)
		{
			ItemInfo itemInfo = default(ItemInfo);
			itemInfo.Id = groupInfo.Index;
			itemInfo.Item = groupInfo.Item;
			itemInfo.Level = groupInfo.Level;
			itemInfo.Slot = groupInfo.Line;
			itemInfo.IsDisplayed = (groupInfo.Line >= slot);
			itemInfo.IsCollapsible = this.IsCollapsible(groupInfo);
			itemInfo.IsCollapsed = this.IsGroupCollapsed(groupInfo);
			itemInfo.ItemType = BaseLayout.GetItemType(itemInfo.Item);
			itemInfo.IsSummaryVisible = (itemInfo.ItemType == GroupType.Subtotal && groupInfo.Parent != null && this.IsCollapsed(groupInfo.Parent.Item));
			itemInfo.LayoutInfo = this.GenerateLayoutInfo(itemInfo, groupInfo);
			return itemInfo;
		}

		// Token: 0x06007BC7 RID: 31687 RVA: 0x001C7148 File Offset: 0x001C5348
		private bool IsGroupCollapsed(TabularGroupInfo groupInfo)
		{
			return !this.generateAll && groupInfo != null && !groupInfo.IsExpanded;
		}

		// Token: 0x06007BC8 RID: 31688 RVA: 0x001C7160 File Offset: 0x001C5360
		private LayoutInfo GenerateLayoutInfo(ItemInfo itemInfo, TabularGroupInfo groupInfo)
		{
			LayoutInfo result = default(LayoutInfo);
			result.Indent = 0;
			result.Line = itemInfo.Slot - this.GetCollapsedSlotsCount(0, itemInfo.Slot);
			result.LineSpan = ((groupInfo == null) ? 1 : Math.Max(1, this.GetVisibleSlotSpan(itemInfo, this.GetGroupInfo(itemInfo.Item)) - this.GetCollapsedSlotsCount(groupInfo.Line, groupInfo.LastSubItemSlot)));
			result.Level = this.GetLayoutLevel(itemInfo, groupInfo);
			if (itemInfo.IsCollapsed && base.TotalsCount > 1)
			{
				result.LevelSpan = Math.Max(1, base.AggregatesLevel - itemInfo.Level);
				result.SpansThroughCells = false;
			}
			else if (itemInfo.ItemType == GroupType.GrandTotal || itemInfo.ItemType == GroupType.Subtotal)
			{
				result.LevelSpan = 1;
				result.SpansThroughCells = true;
			}
			else
			{
				result.LevelSpan = 1;
				result.SpansThroughCells = false;
			}
			return result;
		}

		// Token: 0x06007BC9 RID: 31689 RVA: 0x001C7254 File Offset: 0x001C5454
		private TabularGroupInfo GetGroupInfo(object item)
		{
			TabularGroupInfo result;
			if (this.itemInfoTable != null && this.itemInfoTable.TryGetValue(item, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06007BCA RID: 31690 RVA: 0x001C727C File Offset: 0x001C547C
		private void GetCollapseRange(TabularGroupInfo groupInfo, out int slot, out int slotSpan)
		{
			int line = groupInfo.Line;
			int level = groupInfo.Level;
			slot = line + 1;
			slotSpan = groupInfo.GetLineSpan() - 1;
			int num = base.ShowAggregateValuesInline ? (base.AggregatesLevel - 1) : base.AggregatesLevel;
			if (base.TotalsCount > 1 && level < num)
			{
				switch (base.TotalsPosition)
				{
				case TotalsPosition.Last:
					slotSpan -= base.TotalsCount - 1;
					return;
				case TotalsPosition.First:
				case TotalsPosition.Inline:
					slot += base.TotalsCount - 1;
					slotSpan -= base.TotalsCount - 1;
					break;
				case TotalsPosition.None:
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06007BCB RID: 31691 RVA: 0x001C7312 File Offset: 0x001C5512
		private int GetCollapsedSlotsCount(int startSlot, int endSlot)
		{
			if (this.generateAll)
			{
				return 0;
			}
			return this.collapsedSlotsTable.GetIndexCount(startSlot, endSlot);
		}

		// Token: 0x06007BCC RID: 31692 RVA: 0x001C732C File Offset: 0x001C552C
		private bool IsCollapsible(TabularGroupInfo groupInfo)
		{
			if (groupInfo == null)
			{
				return false;
			}
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

		// Token: 0x06007BCD RID: 31693 RVA: 0x001C739C File Offset: 0x001C559C
		private int GetVisibleSlotSpan(ItemInfo itemInfo, TabularGroupInfo groupInfo)
		{
			object item = itemInfo.Item;
			bool isCollapsed = itemInfo.IsCollapsed;
			int level = itemInfo.Level;
			int num = (groupInfo != null) ? (groupInfo.LastSubItemSlot - groupInfo.Line) : 0;
			int num2 = num + 1;
			if (!isCollapsed && base.TotalsPosition == TotalsPosition.Last && this.adapter.GetItems(item).Any<object>())
			{
				if (level < base.AggregatesLevel - 1)
				{
					num2 -= base.TotalsCount;
				}
				else if (level == base.AggregatesLevel - 1 && level != base.GroupLevels - 2)
				{
					num2 -= (base.ShowAggregateValuesInline ? 0 : base.TotalsCount);
				}
				else if (level == base.AggregatesLevel && base.TotalsCount <= 1)
				{
					num2--;
				}
				else if (level > base.AggregatesLevel)
				{
					num2--;
				}
			}
			return Math.Max(1, num2);
		}

		// Token: 0x06007BCE RID: 31694 RVA: 0x001C7475 File Offset: 0x001C5675
		private int GetLayoutLevel(ItemInfo itemInfo, TabularGroupInfo groupInfo)
		{
			if (itemInfo.ItemType != GroupType.Subtotal || base.TotalsPosition != TotalsPosition.Last)
			{
				return itemInfo.Level;
			}
			if (groupInfo != null && this.IsGroupCollapsed(groupInfo.Parent))
			{
				return base.AggregatesLevel;
			}
			return itemInfo.Level - 1;
		}

		// Token: 0x06007BCF RID: 31695 RVA: 0x001C74B4 File Offset: 0x001C56B4
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Not a real issue.")]
		private int CountAndPopulateTables(object item, int rootSlot, int level, int levels, TabularGroupInfo parent, bool shouldIndexItem, out bool isItemIndexed, ref int index)
		{
			int index2 = index;
			int num = 0;
			isItemIndexed = false;
			TabularGroupInfo tabularGroupInfo = null;
			if (level < levels - 2)
			{
				IEnumerable<object> items = this.adapter.GetItems(item);
				bool flag = false;
				bool flag2 = true;
				foreach (object item2 in items)
				{
					index++;
					if (tabularGroupInfo == null)
					{
						tabularGroupInfo = new TabularGroupInfo(item, parent, true, level, rootSlot, index2, rootSlot + num - 1);
					}
					bool flag3 = false;
					int num2 = this.CountAndPopulateTables(item2, rootSlot + num, level + 1, levels, tabularGroupInfo, flag, out flag3, ref index);
					if (flag2)
					{
						isItemIndexed = flag3;
					}
					flag = (flag || num2 > 1 || this.adapter.GetItems(item2).Any<object>());
					num += num2;
					flag2 = false;
				}
				num = Math.Max(1, num);
			}
			else if (level == levels - 2)
			{
				int num3 = this.adapter.GetItems(item).Count<object>();
				index += num3;
				num += num3;
			}
			shouldIndexItem = ((shouldIndexItem || num > 1) && !isItemIndexed);
			if (this.adapter.GetItems(item).Any<object>() || shouldIndexItem)
			{
				int lastSubItemSlot = rootSlot + Math.Max(1, num) - 1;
				if (tabularGroupInfo == null)
				{
					tabularGroupInfo = new TabularGroupInfo(item, parent, true, level, rootSlot, index2, lastSubItemSlot);
				}
				else
				{
					tabularGroupInfo.LastSubItemSlot = lastSubItemSlot;
				}
				if (this.itemInfoTable == null)
				{
					this.itemInfoTable = new Dictionary<object, TabularGroupInfo>();
				}
				this.itemInfoTable.Add(item, tabularGroupInfo);
			}
			if (shouldIndexItem)
			{
				isItemIndexed = true;
				index += num + 1;
				this.groupHeadersTable.AddValue(rootSlot, tabularGroupInfo);
			}
			return Math.Max(1, num);
		}

		// Token: 0x06007BD0 RID: 31696 RVA: 0x001C765C File Offset: 0x001C585C
		private int GetVisibleSlot(int index)
		{
			return this.collapsedSlotsTable.CountNextNotIncludedIndexes(0, index);
		}

		// Token: 0x06007BD1 RID: 31697 RVA: 0x001C7958 File Offset: 0x001C5B58
		private IEnumerable<TabularGroupInfo> CollapsedChildItems(object item)
		{
			TabularGroupInfo groupInfo = this.GetGroupInfo(item);
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
						foreach (TabularGroupInfo collapsedChildGroup in this.CollapsedChildItems(childGroup))
						{
							yield return collapsedChildGroup;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06007BD2 RID: 31698 RVA: 0x001C797C File Offset: 0x001C5B7C
		private ItemInfo GenerateItemInfo(int id, int slot, int level, object child, TabularGroupInfo parentGroupInfo, bool parentIsCollapsed)
		{
			ItemInfo itemInfo = default(ItemInfo);
			itemInfo.Id = id;
			itemInfo.Item = child;
			itemInfo.Level = level;
			itemInfo.Slot = slot;
			itemInfo.ItemType = BaseLayout.GetItemType(child);
			itemInfo.IsDisplayed = true;
			itemInfo.IsCollapsed = this.IsCollapsed(child);
			itemInfo.IsCollapsible = this.IsCollapsible(parentGroupInfo);
			itemInfo.IsSummaryVisible = (itemInfo.ItemType == GroupType.Subtotal && parentIsCollapsed);
			itemInfo.LayoutInfo = this.GenerateLayoutInfo(itemInfo, parentGroupInfo);
			return itemInfo;
		}

		// Token: 0x040021F5 RID: 8693
		private int totalCount;

		// Token: 0x040021F6 RID: 8694
		private int visibleLineCount;

		// Token: 0x040021F7 RID: 8695
		private bool generateAll;

		// Token: 0x040021F8 RID: 8696
		private IHierarchyAdapter adapter;

		// Token: 0x040021F9 RID: 8697
		private Dictionary<object, TabularGroupInfo> itemInfoTable;

		// Token: 0x040021FA RID: 8698
		private IndexToValueTable<bool> collapsedSlotsTable;

		// Token: 0x040021FB RID: 8699
		private IndexToValueTable<TabularGroupInfo> groupHeadersTable;
	}
}
