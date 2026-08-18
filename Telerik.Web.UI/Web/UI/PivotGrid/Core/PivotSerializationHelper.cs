using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Groups;
using Telerik.Web.UI.PivotGrid.Core.Olap;
using Telerik.Web.UI.PivotGrid.Core.Totals;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x0200071C RID: 1820
	public static class PivotSerializationHelper
	{
		// Token: 0x060040A7 RID: 16551 RVA: 0x000CB1E4 File Offset: 0x000C93E4
		internal static SortOrder GridSortOrderToCoreSortOrder(PivotGridSortOrder order)
		{
			switch (order)
			{
			case PivotGridSortOrder.Ascending:
				return SortOrder.Ascending;
			case PivotGridSortOrder.Descending:
				return SortOrder.Descending;
			default:
				return SortOrder.None;
			}
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x000CBEC4 File Offset: 0x000CA0C4
		public static IEnumerable<Type> KnownTypes
		{
			get
			{
				yield return typeof(PropertyFilterDescription);
				yield return typeof(PropertyGroupDescription);
				yield return typeof(PropertyAggregateDescription);
				yield return typeof(CalculatedAggregateDescription);
				yield return typeof(FilterDescription);
				yield return typeof(DoubleGroupDescription);
				yield return typeof(DateTimeGroupDescription);
				yield return typeof(GroupDescription);
				yield return typeof(GroupComparer);
				yield return typeof(GroupNameComparer);
				yield return typeof(GrandTotalComparer);
				yield return typeof(OlapGroupComparer);
				yield return typeof(StringComparer);
				yield return typeof(TotalFormat);
				yield return typeof(Condition);
				yield return typeof(SetCondition);
				yield return typeof(IntervalCondition);
				yield return typeof(ComparisonCondition);
				yield return typeof(LocalCondition);
				yield return typeof(TextCondition);
				yield return typeof(ItemsFilterCondition);
				yield return typeof(SettingsNode);
				yield return typeof(GroupsCountFilter);
				yield return typeof(GroupsPercentFilter);
				yield return typeof(GroupsSumFilter);
				yield return typeof(LabelGroupFilter);
				yield return typeof(ValueGroupFilter);
				yield return typeof(CountAggregateFunction);
				yield return typeof(AverageAggregateFunction);
				yield return typeof(MaxAggregateFunction);
				yield return typeof(MinAggregateFunction);
				yield return typeof(NumericFormatAggregateFunction);
				yield return typeof(ProductAggregateFunction);
				yield return typeof(StatisticalFormatAggregateFunction);
				yield return typeof(StdDevAggregateFunction);
				yield return typeof(StdDevPAggregateFunction);
				yield return typeof(SumAggregateFunction);
				yield return typeof(VarAggregateFunction);
				yield return typeof(VarPAggregateFunction);
				yield return typeof(PercentOfColumnTotal);
				yield return typeof(PercentOfGrandTotal);
				yield return typeof(RankTotals);
				yield return typeof(ComparedTo);
				yield return typeof(DifferenceFrom);
				yield return typeof(DifferenceFromNext);
				yield return typeof(DifferenceFromPrevious);
				yield return typeof(Index);
				yield return typeof(PercentDifferenceFrom);
				yield return typeof(PercentDifferenceFromNext);
				yield return typeof(PercentDifferenceFromPrevious);
				yield return typeof(PercentOf);
				yield return typeof(PercentOfAncestor);
				yield return typeof(PercentOfNext);
				yield return typeof(PercentOfPrevious);
				yield return typeof(PercentOfRowTotal);
				yield return typeof(PercentRunningTotalsIn);
				yield return typeof(RunningTotalsIn);
				yield return typeof(SiblingTotalsFormat);
				yield return typeof(SingleTotalFormat);
				yield return typeof(OlapAggregateDescription);
				yield return typeof(OlapFilterDescription);
				yield return typeof(OlapGroupDescription);
				yield return typeof(OlapLevelFilterDescription);
				yield return typeof(OlapLevelGroupDescription);
				yield return typeof(OlapCondition);
				yield return typeof(OlapComparisonCondition);
				yield return typeof(OlapSetCondition);
				yield return typeof(OlapTextCondition);
				yield return typeof(OlapIntervalCondition);
				yield return typeof(OlapItemsFilterCondition);
				yield return typeof(OlapValueGroupFilter);
				yield return typeof(OlapLabelGroupFilter);
				yield return typeof(OlapGroupName);
				yield return typeof(DayGroup);
				yield return typeof(HourGroup);
				yield return typeof(MinuteGroup);
				yield return typeof(MonthGroup);
				yield return typeof(QuarterGroup);
				yield return typeof(SecondGroup);
				yield return typeof(WeekGroup);
				yield return typeof(YearGroup);
				yield return typeof(DoubleGroup);
				yield return typeof(MemberDistinctValue);
				yield return typeof(DateTimeOffset);
				yield break;
			}
		}
	}
}
