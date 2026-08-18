using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000713 RID: 1811
	internal class OlapAggregateResultProvider : IAggregateResultProvider
	{
		// Token: 0x06004057 RID: 16471 RVA: 0x000CAC59 File Offset: 0x000C8E59
		internal OlapAggregateResultProvider(Coordinate mainCoordinate, IDictionary<Coordinate, AggregateValue[]> finalAggregates)
		{
			this.mainCoordinate = mainCoordinate;
			this.finalAggregates = finalAggregates;
		}

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x000CAC6F File Offset: 0x000C8E6F
		public Coordinate Root
		{
			get
			{
				return this.mainCoordinate;
			}
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x000CAC78 File Offset: 0x000C8E78
		public AggregateValue GetAggregateResult(int aggregateIndex, Coordinate groups)
		{
			AggregateValue[] array;
			if (this.finalAggregates.TryGetValue(groups, out array))
			{
				AggregateValue aggregateValue = array[aggregateIndex];
				if (aggregateValue != null)
				{
					return aggregateValue;
				}
			}
			return null;
		}

		// Token: 0x0400110D RID: 4365
		private readonly Coordinate mainCoordinate;

		// Token: 0x0400110E RID: 4366
		private IDictionary<Coordinate, AggregateValue[]> finalAggregates;
	}
}
