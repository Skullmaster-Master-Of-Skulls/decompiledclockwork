using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Layouts
{
	// Token: 0x02000CE9 RID: 3305
	internal abstract class BaseLayout
	{
		// Token: 0x1400012F RID: 303
		// (add) Token: 0x06007B5D RID: 31581 RVA: 0x001C527C File Offset: 0x001C347C
		// (remove) Token: 0x06007B5E RID: 31582 RVA: 0x001C52B4 File Offset: 0x001C34B4
		public event EventHandler<ExpandCollapseEventArgs> Collapsed;

		// Token: 0x14000130 RID: 304
		// (add) Token: 0x06007B5F RID: 31583 RVA: 0x001C52EC File Offset: 0x001C34EC
		// (remove) Token: 0x06007B60 RID: 31584 RVA: 0x001C5324 File Offset: 0x001C3524
		public event EventHandler<ExpandCollapseEventArgs> Expanded;

		// Token: 0x17002774 RID: 10100
		// (get) Token: 0x06007B61 RID: 31585 RVA: 0x001C5359 File Offset: 0x001C3559
		// (set) Token: 0x06007B62 RID: 31586 RVA: 0x001C5361 File Offset: 0x001C3561
		protected internal int TotalsCount { get; private set; }

		// Token: 0x17002775 RID: 10101
		// (get) Token: 0x06007B63 RID: 31587 RVA: 0x001C536A File Offset: 0x001C356A
		// (set) Token: 0x06007B64 RID: 31588 RVA: 0x001C5372 File Offset: 0x001C3572
		protected internal int AggregatesLevel { get; private set; }

		// Token: 0x17002776 RID: 10102
		// (get) Token: 0x06007B65 RID: 31589 RVA: 0x001C537B File Offset: 0x001C357B
		// (set) Token: 0x06007B66 RID: 31590 RVA: 0x001C5383 File Offset: 0x001C3583
		protected internal TotalsPosition TotalsPosition { get; private set; }

		// Token: 0x17002777 RID: 10103
		// (get) Token: 0x06007B67 RID: 31591 RVA: 0x001C538C File Offset: 0x001C358C
		// (set) Token: 0x06007B68 RID: 31592 RVA: 0x001C5394 File Offset: 0x001C3594
		protected internal bool ShowAggregateValuesInline { get; private set; }

		// Token: 0x17002778 RID: 10104
		// (get) Token: 0x06007B69 RID: 31593 RVA: 0x001C539D File Offset: 0x001C359D
		// (set) Token: 0x06007B6A RID: 31594 RVA: 0x001C53A5 File Offset: 0x001C35A5
		protected internal IReadOnlyList<object> ItemsSource { get; private set; }

		// Token: 0x17002779 RID: 10105
		// (get) Token: 0x06007B6B RID: 31595 RVA: 0x001C53AE File Offset: 0x001C35AE
		// (set) Token: 0x06007B6C RID: 31596 RVA: 0x001C53B6 File Offset: 0x001C35B6
		protected internal int GroupLevels { get; private set; }

		// Token: 0x1700277A RID: 10106
		// (get) Token: 0x06007B6D RID: 31597
		public abstract int VisibleLineCount { get; }

		// Token: 0x06007B6E RID: 31598 RVA: 0x001C53BF File Offset: 0x001C35BF
		public void SetSource(IEnumerable source)
		{
			this.SetSource(source, int.MaxValue, TotalsPosition.None, -1, 0, false);
		}

		// Token: 0x06007B6F RID: 31599 RVA: 0x001C53D4 File Offset: 0x001C35D4
		public void SetSource(IEnumerable source, int groupLevels, TotalsPosition totalsPosition, int aggregatesLevel, int totalsCount, bool showAggregateValuesInline)
		{
			if ((aggregatesLevel <= -1 || aggregatesLevel >= groupLevels) && totalsCount > 1)
			{
				aggregatesLevel = Math.Max(0, groupLevels - 1);
			}
			else if (totalsCount <= 1)
			{
				aggregatesLevel = 0;
			}
			this.GroupLevels = groupLevels;
			this.TotalsPosition = totalsPosition;
			this.AggregatesLevel = aggregatesLevel;
			this.TotalsCount = totalsCount;
			this.ShowAggregateValuesInline = showAggregateValuesInline;
			this.SetItemsSource(source);
			this.SetItemsSourceOverride(this.ItemsSource);
		}

		// Token: 0x06007B70 RID: 31600
		public abstract bool IsCollapsed(object item);

		// Token: 0x06007B71 RID: 31601
		public abstract void Expand(object item);

		// Token: 0x06007B72 RID: 31602
		public abstract void Collapse(object item);

		// Token: 0x06007B73 RID: 31603
		public abstract IEnumerable<IList<ItemInfo>> GetLines(int start, bool forward);

		// Token: 0x06007B74 RID: 31604
		public abstract IEnumerable<IList<ItemInfo>> GetAllLines();

		// Token: 0x06007B75 RID: 31605 RVA: 0x001C543E File Offset: 0x001C363E
		protected void RaiseCollapsed(ExpandCollapseEventArgs e)
		{
			if (this.Collapsed != null)
			{
				this.Collapsed(this, e);
			}
		}

		// Token: 0x06007B76 RID: 31606 RVA: 0x001C5455 File Offset: 0x001C3655
		protected void RaiseExpanded(ExpandCollapseEventArgs e)
		{
			if (this.Expanded != null)
			{
				this.Expanded(this, e);
			}
		}

		// Token: 0x06007B77 RID: 31607
		protected abstract void SetItemsSourceOverride(IReadOnlyList<object> source);

		// Token: 0x06007B78 RID: 31608 RVA: 0x001C546C File Offset: 0x001C366C
		private void SetItemsSource(IEnumerable source)
		{
			IReadOnlyList<object> readOnlyList = source as IReadOnlyList<object>;
			if (readOnlyList != null)
			{
				this.ItemsSource = readOnlyList;
				return;
			}
			if (source == null)
			{
				this.ItemsSource = new ReadOnlyList<object, object>(new List<object>());
				return;
			}
			IList<object> list = new List<object>();
			foreach (object item in source)
			{
				list.Add(item);
			}
			this.ItemsSource = new ReadOnlyList<object, object>(list);
		}

		// Token: 0x06007B79 RID: 31609 RVA: 0x001C54F8 File Offset: 0x001C36F8
		internal static GroupType GetItemType(object item)
		{
			IGroup group = item as IGroup;
			if (group != null)
			{
				return group.Type;
			}
			return GroupType.BottomLevel;
		}
	}
}
