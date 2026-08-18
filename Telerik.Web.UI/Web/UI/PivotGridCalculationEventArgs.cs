using System;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI
{
	// Token: 0x0200076D RID: 1901
	public class PivotGridCalculationEventArgs : EventArgs
	{
		// Token: 0x0600431B RID: 17179 RVA: 0x000D1C62 File Offset: 0x000CFE62
		public PivotGridCalculationEventArgs()
		{
			this.DataField = "";
			this.GroupName = "";
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x000D1C80 File Offset: 0x000CFE80
		// (set) Token: 0x0600431D RID: 17181 RVA: 0x000D1C88 File Offset: 0x000CFE88
		internal IAggregateSummaryValues AggregateSummaryValues { get; set; }

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x0600431E RID: 17182 RVA: 0x000D1C91 File Offset: 0x000CFE91
		// (set) Token: 0x0600431F RID: 17183 RVA: 0x000D1C99 File Offset: 0x000CFE99
		internal IAggregateValues AggregateValues { get; set; }

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06004320 RID: 17184 RVA: 0x000D1CA2 File Offset: 0x000CFEA2
		// (set) Token: 0x06004321 RID: 17185 RVA: 0x000D1CAA File Offset: 0x000CFEAA
		public AggregateValue CalculatedValue { get; set; }

		// Token: 0x06004322 RID: 17186 RVA: 0x000D1CB4 File Offset: 0x000CFEB4
		public AggregateValue GetAggregateSummaryValue(object groupName)
		{
			AggregateValue result;
			try
			{
				result = this.AggregateSummaryValues.GetAggregateValue(groupName);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x000D1CE8 File Offset: 0x000CFEE8
		public AggregateValue GetAggregateValue(string fieldName, object aggregateFunction = null)
		{
			AggregateValue result;
			try
			{
				if (aggregateFunction != null)
				{
					result = this.AggregateValues.GetAggregateValue(RequiredField.ForProperty(fieldName, aggregateFunction));
				}
				else
				{
					result = this.AggregateValues.GetAggregateValue(RequiredField.ForProperty(fieldName));
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06004324 RID: 17188 RVA: 0x000D1D38 File Offset: 0x000CFF38
		// (set) Token: 0x06004325 RID: 17189 RVA: 0x000D1D40 File Offset: 0x000CFF40
		public string DataField { get; internal set; }

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x000D1D49 File Offset: 0x000CFF49
		// (set) Token: 0x06004327 RID: 17191 RVA: 0x000D1D51 File Offset: 0x000CFF51
		public object GroupName { get; internal set; }
	}
}
