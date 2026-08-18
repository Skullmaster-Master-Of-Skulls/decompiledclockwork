using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x02000CCC RID: 3276
	[DataContract]
	public abstract class SortedGroupsFilter : SiblingGroupsFilter, ITopGroupsFilter
	{
		// Token: 0x17002755 RID: 10069
		// (get) Token: 0x06007A85 RID: 31365 RVA: 0x001C1D50 File Offset: 0x001BFF50
		// (set) Token: 0x06007A86 RID: 31366 RVA: 0x001C1D58 File Offset: 0x001BFF58
		[DataMember]
		public int AggregateIndex
		{
			get
			{
				return this.aggregateIndex;
			}
			set
			{
				if (this.aggregateIndex != value)
				{
					this.aggregateIndex = value;
					base.OnPropertyChanged("AggregateIndex");
				}
			}
		}

		// Token: 0x17002756 RID: 10070
		// (get) Token: 0x06007A87 RID: 31367 RVA: 0x001C1D75 File Offset: 0x001BFF75
		// (set) Token: 0x06007A88 RID: 31368 RVA: 0x001C1D7D File Offset: 0x001BFF7D
		[DataMember]
		public SortedListSelection Selection
		{
			get
			{
				return this.selection;
			}
			set
			{
				if (this.selection != value)
				{
					this.selection = value;
					base.OnPropertyChanged("Selection");
				}
			}
		}

		// Token: 0x17002757 RID: 10071
		// (get) Token: 0x06007A89 RID: 31369 RVA: 0x001C1D9A File Offset: 0x001BFF9A
		// (set) Token: 0x06007A8A RID: 31370 RVA: 0x001C1DA2 File Offset: 0x001BFFA2
		[DataMember]
		public IComparer<AggregateValue> Comparer
		{
			get
			{
				return this.comparer;
			}
			set
			{
				if (this.comparer != value)
				{
					this.comparer = value;
					base.OnPropertyChanged("Comparer");
				}
			}
		}

		// Token: 0x06007A8B RID: 31371 RVA: 0x001C1DBF File Offset: 0x001BFFBF
		internal IComparer<AggregateValue> GetComparerOrDefault()
		{
			if (this.comparer == null)
			{
				return new DefaultAggregateValueComparer();
			}
			return this.comparer;
		}

		// Token: 0x06007A8C RID: 31372 RVA: 0x001C1DD8 File Offset: 0x001BFFD8
		protected internal sealed override ICollection<IGroup> Filter(IReadOnlyList<IGroup> groups, IAggregateResultProvider results, PivotAxis axis, int level)
		{
			List<SortedGroupsFilter.GroupAndGrandTotal> list = new List<SortedGroupsFilter.GroupAndGrandTotal>(groups.Count);
			IGroup group = (axis == PivotAxis.Rows) ? results.Root.ColumnGroup : results.Root.RowGroup;
			int num = this.AggregateIndex;
			AggregateValue aggregateValue;
			for (int i = 0; i < groups.Count; i++)
			{
				IGroup group2 = groups[i];
				Coordinate groups2 = (axis == PivotAxis.Rows) ? new Coordinate(group2, group) : new Coordinate(group, group2);
				aggregateValue = results.GetAggregateResult(num, groups2);
				list.Add(new SortedGroupsFilter.GroupAndGrandTotal(group2, aggregateValue));
			}
			list.Sort(new SortedGroupsFilter.GroupAndGrandTotalComparer(this.GetComparerOrDefault(), this.Selection));
			aggregateValue = null;
			if (groups.Count > 0)
			{
				IGroup parent = groups[0].Parent;
				Coordinate groups2 = (axis == PivotAxis.Rows) ? new Coordinate(parent, group) : new Coordinate(group, parent);
				aggregateValue = results.GetAggregateResult(num, groups2);
			}
			return this.SelectGroups(list, aggregateValue);
		}

		// Token: 0x06007A8D RID: 31373 RVA: 0x001C1EC4 File Offset: 0x001C00C4
		protected override void CloneCore(Cloneable source)
		{
			SortedGroupsFilter sortedGroupsFilter = source as SortedGroupsFilter;
			if (sortedGroupsFilter != null)
			{
				this.AggregateIndex = sortedGroupsFilter.AggregateIndex;
				this.Comparer = sortedGroupsFilter.Comparer;
				this.Selection = sortedGroupsFilter.Selection;
			}
		}

		// Token: 0x06007A8E RID: 31374
		internal abstract ICollection<IGroup> SelectGroups(IList<SortedGroupsFilter.GroupAndGrandTotal> list, AggregateValue total);

		// Token: 0x06007A8F RID: 31375 RVA: 0x001C1F00 File Offset: 0x001C0100
		internal static bool TryGetDouble(AggregateValue aggregateValue, out double doubleValue)
		{
			if (aggregateValue == null)
			{
				doubleValue = 0.0;
				return false;
			}
			object value = aggregateValue.GetValue();
			if (value is AggregateError)
			{
				doubleValue = 0.0;
				return false;
			}
			doubleValue = Convert.ToDouble(value, CultureInfo.InvariantCulture);
			return true;
		}

		// Token: 0x06007A90 RID: 31376 RVA: 0x001C1F48 File Offset: 0x001C0148
		internal override bool TrackDescriptions(IDescriptionIndexMap map)
		{
			bool flag = base.TrackDescriptions(map);
			AggregateMapResult aggregateMapResult = DescriptionIndexMapExtensions.MapAggregate(map, this.AggregateIndex);
			this.AggregateIndex = aggregateMapResult.Index;
			return flag && aggregateMapResult.Success;
		}

		// Token: 0x0400218D RID: 8589
		private int aggregateIndex;

		// Token: 0x0400218E RID: 8590
		private SortedListSelection selection;

		// Token: 0x0400218F RID: 8591
		private IComparer<AggregateValue> comparer;

		// Token: 0x02000CCD RID: 3277
		internal struct GroupAndGrandTotal
		{
			// Token: 0x06007A92 RID: 31378 RVA: 0x001C1F8B File Offset: 0x001C018B
			public GroupAndGrandTotal(IGroup group, AggregateValue grandTotal)
			{
				this.Group = group;
				this.GrandTotal = grandTotal;
			}

			// Token: 0x04002190 RID: 8592
			public IGroup Group;

			// Token: 0x04002191 RID: 8593
			public AggregateValue GrandTotal;
		}

		// Token: 0x02000CCE RID: 3278
		private class GroupAndGrandTotalComparer : IComparer<SortedGroupsFilter.GroupAndGrandTotal>
		{
			// Token: 0x06007A93 RID: 31379 RVA: 0x001C1F9B File Offset: 0x001C019B
			public GroupAndGrandTotalComparer(IComparer<AggregateValue> totalComparer, SortedListSelection selection)
			{
				this.comaprer = totalComparer;
				this.sortOrder = ((selection == SortedListSelection.Top) ? -1 : 1);
			}

			// Token: 0x06007A94 RID: 31380 RVA: 0x001C1FB7 File Offset: 0x001C01B7
			public int Compare(SortedGroupsFilter.GroupAndGrandTotal x, SortedGroupsFilter.GroupAndGrandTotal y)
			{
				return this.comaprer.Compare(x.GrandTotal, y.GrandTotal) * this.sortOrder;
			}

			// Token: 0x04002192 RID: 8594
			private IComparer<AggregateValue> comaprer;

			// Token: 0x04002193 RID: 8595
			private int sortOrder;
		}
	}
}
