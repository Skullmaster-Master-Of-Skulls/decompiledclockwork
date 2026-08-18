using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.Engine;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D10 RID: 3344
	internal class OlapPivotResults : IPivotResults, IAggregateResultProvider
	{
		// Token: 0x06007C9D RID: 31901 RVA: 0x001C9E28 File Offset: 0x001C8028
		internal OlapPivotResults(PivotResultsProcessingState state)
		{
			this.state = state;
			this.uniqueKeys = state.UniqueGroupKeys;
		}

		// Token: 0x170027BE RID: 10174
		// (get) Token: 0x06007C9E RID: 31902 RVA: 0x001C9E43 File Offset: 0x001C8043
		public IReadOnlyList<GroupDescription> RowGroupDescriptions
		{
			get
			{
				return this.state.RowGroupDescriptions;
			}
		}

		// Token: 0x170027BF RID: 10175
		// (get) Token: 0x06007C9F RID: 31903 RVA: 0x001C9E50 File Offset: 0x001C8050
		public IReadOnlyList<GroupDescription> ColumnGroupDescriptions
		{
			get
			{
				return this.state.ColumnGroupDescriptions;
			}
		}

		// Token: 0x170027C0 RID: 10176
		// (get) Token: 0x06007CA0 RID: 31904 RVA: 0x001C9E5D File Offset: 0x001C805D
		public IReadOnlyList<IAggregateDescription> AggregateDescriptions
		{
			get
			{
				return this.state.AggregateDescriptions;
			}
		}

		// Token: 0x170027C1 RID: 10177
		// (get) Token: 0x06007CA1 RID: 31905 RVA: 0x001C9E6A File Offset: 0x001C806A
		public IReadOnlyList<FilterDescription> FilterDescriptions
		{
			get
			{
				return this.state.FilterDescriptions;
			}
		}

		// Token: 0x170027C2 RID: 10178
		// (get) Token: 0x06007CA2 RID: 31906 RVA: 0x001C9E77 File Offset: 0x001C8077
		public Coordinate Root
		{
			get
			{
				return this.state.AggregatesProvider.Root;
			}
		}

		// Token: 0x06007CA3 RID: 31907 RVA: 0x001C9E8C File Offset: 0x001C808C
		public IEnumerable<object> GetUniqueKeys(PivotAxis axis, int groupDescriptionIndex)
		{
			if (this.uniqueKeys == null)
			{
				return null;
			}
			if (axis == PivotAxis.Rows && this.uniqueKeys.Count >= 1)
			{
				List<HashSet<object>> list = this.uniqueKeys[0];
				if (groupDescriptionIndex >= 0 && groupDescriptionIndex < list.Count)
				{
					return list[groupDescriptionIndex];
				}
			}
			else if (axis == PivotAxis.Columns && this.uniqueKeys.Count >= 2)
			{
				List<HashSet<object>> list2 = this.uniqueKeys[1];
				if (groupDescriptionIndex >= 0 && groupDescriptionIndex < list2.Count)
				{
					return list2[groupDescriptionIndex];
				}
			}
			return null;
		}

		// Token: 0x06007CA4 RID: 31908 RVA: 0x001C9F0B File Offset: 0x001C810B
		public IEnumerable<object> GetUniqueFilterItems(int filterIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007CA5 RID: 31909 RVA: 0x001C9F12 File Offset: 0x001C8112
		public AggregateValue GetAggregateResult(int aggregateIndex, IGroup row, IGroup column)
		{
			return this.GetAggregateResult(aggregateIndex, new Coordinate(row, column));
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x001C9F24 File Offset: 0x001C8124
		public AggregateValue GetAggregateResult(int aggregateIndex, Coordinate groups)
		{
			if (aggregateIndex >= 0 && aggregateIndex < this.AggregateDescriptions.Count)
			{
				IAggregateDescription aggregateDescription = this.AggregateDescriptions[aggregateIndex];
				if (aggregateDescription.TotalFormat == null)
				{
					return this.state.AggregatesProvider.GetAggregateResult(aggregateIndex, groups);
				}
				AggregateValue[] array;
				if (this.state.FormatTotals.TryGetValue(groups, out array))
				{
					AggregateValue aggregateValue = array[aggregateIndex];
					if (aggregateValue != null)
					{
						return aggregateValue;
					}
				}
			}
			return null;
		}

		// Token: 0x04002221 RID: 8737
		private List<List<HashSet<object>>> uniqueKeys;

		// Token: 0x04002222 RID: 8738
		private PivotResultsProcessingState state;
	}
}
