using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CF2 RID: 3314
	internal class AggregatesDictionary
	{
		// Token: 0x06007BD3 RID: 31699 RVA: 0x001C7A10 File Offset: 0x001C5C10
		public AggregatesDictionary(IEnumerable aggregateDescriptions)
		{
			this.dictionary = new Dictionary<Coordinate, AggregateValue[]>();
			this.aggregateDescriptions = new List<AggregateDescriptionBase>();
			foreach (object obj in aggregateDescriptions)
			{
				AggregateDescriptionBase aggregateDescriptionBase = obj as AggregateDescriptionBase;
				if (aggregateDescriptionBase != null)
				{
					this.aggregateDescriptions.Add(aggregateDescriptionBase.Clone() as AggregateDescriptionBase);
				}
			}
		}

		// Token: 0x06007BD4 RID: 31700 RVA: 0x001C7A94 File Offset: 0x001C5C94
		public Dictionary<Coordinate, AggregateValue[]> GetInternalDictionary()
		{
			return this.dictionary;
		}

		// Token: 0x06007BD5 RID: 31701 RVA: 0x001C7A9C File Offset: 0x001C5C9C
		internal AggregateValue[] GetAggregates(Coordinate coordinate)
		{
			if (this.dictionary.ContainsKey(coordinate))
			{
				return this.dictionary[coordinate];
			}
			int count = this.aggregateDescriptions.Count;
			AggregateValue[] array = new AggregateValue[count];
			this.dictionary.Add(coordinate, array);
			return array;
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x001C7AE8 File Offset: 0x001C5CE8
		internal void AddAggregateValue(Coordinate coordinate, int aggregateIndex, IOlapCell cell)
		{
			AggregateValue[] aggregates = this.GetAggregates(coordinate);
			if (cell.Value != null)
			{
				OlapAggregateValue olapAggregateValue = new OlapAggregateValue();
				aggregates[aggregateIndex] = olapAggregateValue;
				olapAggregateValue.AccumulateCore(cell.Value);
				olapAggregateValue.SetFormattedValue(cell.FormattedValue);
				return;
			}
			aggregates[aggregateIndex] = null;
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x001C7B2C File Offset: 0x001C5D2C
		internal void CreateAggregates(Coordinate coordinate, object[] aggregateValues)
		{
			int count = this.aggregateDescriptions.Count;
			AggregateValue[] array = new AggregateValue[count];
			for (int i = 0; i < count; i++)
			{
				object obj = aggregateValues[i];
				if (obj != null)
				{
					OlapAggregateValue olapAggregateValue = new OlapAggregateValue();
					olapAggregateValue.AccumulateCore(obj);
					array[i] = olapAggregateValue;
				}
			}
			this.dictionary.Add(coordinate, array);
		}

		// Token: 0x040021FC RID: 8700
		private Dictionary<Coordinate, AggregateValue[]> dictionary;

		// Token: 0x040021FD RID: 8701
		private IList<AggregateDescriptionBase> aggregateDescriptions;
	}
}
