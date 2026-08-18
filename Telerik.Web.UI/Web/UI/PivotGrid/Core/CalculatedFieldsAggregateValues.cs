using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000719 RID: 1817
	internal class CalculatedFieldsAggregateValues : IAggregateValues
	{
		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x000CAF2A File Offset: 0x000C912A
		// (set) Token: 0x06004081 RID: 16513 RVA: 0x000CAF32 File Offset: 0x000C9132
		public AggregateDescriptionInfo[] Infos { get; set; }

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06004082 RID: 16514 RVA: 0x000CAF3B File Offset: 0x000C913B
		// (set) Token: 0x06004083 RID: 16515 RVA: 0x000CAF43 File Offset: 0x000C9143
		public IDictionary<Coordinate, AggregateValue[]> Aggregates { get; set; }

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x000CAF4C File Offset: 0x000C914C
		// (set) Token: 0x06004085 RID: 16517 RVA: 0x000CAF54 File Offset: 0x000C9154
		public IDictionary<Coordinate, AggregateValue[]> Summaries { get; set; }

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000CAF5D File Offset: 0x000C915D
		// (set) Token: 0x06004087 RID: 16519 RVA: 0x000CAF65 File Offset: 0x000C9165
		public Coordinate Coordinate { get; set; }

		// Token: 0x06004088 RID: 16520 RVA: 0x000CAF70 File Offset: 0x000C9170
		public AggregateValue[] GetAggregateResults()
		{
			AggregateValue[] result;
			if (this.Aggregates.TryGetValue(this.Coordinate, out result))
			{
				return result;
			}
			if (this.Summaries.TryGetValue(this.Coordinate, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x000CAFAC File Offset: 0x000C91AC
		public AggregateValue GetAggregateValue(RequiredField calculatedFieldSettings)
		{
			AggregateValue[] aggregateResults = this.GetAggregateResults();
			int aggregateIndex = this.GetAggregateIndex(calculatedFieldSettings);
			if (aggregateIndex != -1 && aggregateResults != null)
			{
				return aggregateResults[aggregateIndex];
			}
			return null;
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x000CAFD4 File Offset: 0x000C91D4
		private int GetAggregateIndex(RequiredField calculatedFieldSettings)
		{
			for (int i = 0; i < this.Infos.Length; i++)
			{
				AggregateDescriptionInfo aggregateDescriptionInfo = this.Infos[i];
				if (object.Equals(aggregateDescriptionInfo.LocalCalculatedFieldSettings, calculatedFieldSettings))
				{
					return aggregateDescriptionInfo.OriginalIndex;
				}
			}
			return -1;
		}
	}
}
