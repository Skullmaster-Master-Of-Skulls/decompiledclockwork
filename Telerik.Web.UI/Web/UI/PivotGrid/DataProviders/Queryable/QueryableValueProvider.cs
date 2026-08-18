using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Queryable;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Queryable
{
	// Token: 0x02000D71 RID: 3441
	internal class QueryableValueProvider : IValueProvider
	{
		// Token: 0x0600808A RID: 32906 RVA: 0x001D678C File Offset: 0x001D498C
		public QueryableValueProvider(QueryableGroupingInfo gi)
		{
			this.groupingInfo = gi;
		}

		// Token: 0x0600808B RID: 32907 RVA: 0x001D68D8 File Offset: 0x001D4AD8
		IEnumerable IValueProvider.GetRowGroupNames(object item)
		{
			for (int level = 0; level < this.groupingInfo.RowGroupDescriptions.Count; level++)
			{
				QueryableGroupDescription descriptor = this.groupingInfo.RowGroupDescriptions[level];
				yield return this.GroupNameFromItem(descriptor, item);
			}
			yield break;
		}

		// Token: 0x0600808C RID: 32908 RVA: 0x001D6A38 File Offset: 0x001D4C38
		IEnumerable IValueProvider.GetColumnGroupNames(object item)
		{
			for (int level = 0; level < this.groupingInfo.ColumnGroupDescriptions.Count; level++)
			{
				QueryableGroupDescription descriptor = this.groupingInfo.ColumnGroupDescriptions[level];
				yield return this.GroupNameFromItem(descriptor, item);
			}
			yield break;
		}

		// Token: 0x0600808D RID: 32909 RVA: 0x001D6A5C File Offset: 0x001D4C5C
		private object GroupNameFromItem(QueryableGroupDescription descriptor, object item)
		{
			GroupDescriptionInformation groupDescriptionInformation = this.groupingInfo.DescriptorInfoMappings[descriptor];
			Func<object, object> groupingTypePropertyAccess = groupDescriptionInformation.GroupingTypePropertyAccess;
			object key = (item as PivotResultItem).Key;
			return groupingTypePropertyAccess(key);
		}

		// Token: 0x0600808E RID: 32910 RVA: 0x001D6A98 File Offset: 0x001D4C98
		object IValueProvider.GetAggregateValue(int index, object item)
		{
			AggregateDescriptionInformation aggregateDescriptionInformation = this.groupingInfo.AggregateDescriptorInfoList[index];
			Func<object, object> aggregateTypePropertyAccess = aggregateDescriptionInformation.AggregateTypePropertyAccess;
			object aggregates = (item as PivotResultItem).Aggregates;
			return aggregateTypePropertyAccess(aggregates);
		}

		// Token: 0x0600808F RID: 32911 RVA: 0x001D6AD3 File Offset: 0x001D4CD3
		AggregateValue IValueProvider.CreateAggregateValue(int index, bool hasCalculatedGroups)
		{
			return this.groupingInfo.AggregateDescriptions[index].CreateAggregate();
		}

		// Token: 0x06008090 RID: 32912 RVA: 0x001D6AEB File Offset: 0x001D4CEB
		string IValueProvider.GetAggregateStringFormat(int aggregateDescriptionIndex)
		{
			return this.groupingInfo.AggregateDescriptions[aggregateDescriptionIndex].GetEffectiveFormat();
		}

		// Token: 0x06008091 RID: 32913 RVA: 0x001D6B03 File Offset: 0x001D4D03
		int IValueProvider.GetFiltersCount()
		{
			return 0;
		}

		// Token: 0x06008092 RID: 32914 RVA: 0x001D6B06 File Offset: 0x001D4D06
		object[] IValueProvider.GetFilterItems(object item)
		{
			return new object[0];
		}

		// Token: 0x06008093 RID: 32915 RVA: 0x001D6B0E File Offset: 0x001D4D0E
		bool IValueProvider.PassesFilter(object[] filterItems)
		{
			return true;
		}

		// Token: 0x06008094 RID: 32916 RVA: 0x001D6B11 File Offset: 0x001D4D11
		IEnumerable<CalculatedItem> IValueProvider.GetRowCalculatedItems(int level)
		{
			return this.groupingInfo.RowGroupDescriptions[level].CalculatedItems;
		}

		// Token: 0x06008095 RID: 32917 RVA: 0x001D6B29 File Offset: 0x001D4D29
		IEnumerable<CalculatedItem> IValueProvider.GetColumnCalculatedItems(int level)
		{
			return this.groupingInfo.ColumnGroupDescriptions[level].CalculatedItems;
		}

		// Token: 0x0400235D RID: 9053
		private QueryableGroupingInfo groupingInfo;
	}
}
