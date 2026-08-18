using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C76 RID: 3190
	internal sealed class TotalValue
	{
		// Token: 0x060077EA RID: 30698 RVA: 0x001BB9C3 File Offset: 0x001B9BC3
		internal TotalValue(IAggregateResultProvider results, Coordinate groups, int aggregate)
		{
			this.results = results;
			this.aggregate = aggregate;
			this.unasigendValue = true;
			this.Groups = groups;
		}

		// Token: 0x170026D0 RID: 9936
		// (get) Token: 0x060077EB RID: 30699 RVA: 0x001BB9E7 File Offset: 0x001B9BE7
		// (set) Token: 0x060077EC RID: 30700 RVA: 0x001BB9EF File Offset: 0x001B9BEF
		[SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "value", Justification = "Design choice.")]
		public Coordinate Groups { get; private set; }

		// Token: 0x170026D1 RID: 9937
		// (get) Token: 0x060077ED RID: 30701 RVA: 0x001BB9F8 File Offset: 0x001B9BF8
		// (set) Token: 0x060077EE RID: 30702 RVA: 0x001BBA00 File Offset: 0x001B9C00
		[SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "value", Justification = "Design choice.")]
		public AggregateValue FormattedValue { get; set; }

		// Token: 0x170026D2 RID: 9938
		// (get) Token: 0x060077EF RID: 30703 RVA: 0x001BBA09 File Offset: 0x001B9C09
		public AggregateValue Value
		{
			get
			{
				if (this.unasigendValue)
				{
					this.value = this.results.GetAggregateResult(this.aggregate, this.Groups);
				}
				return this.value;
			}
		}

		// Token: 0x040020CE RID: 8398
		private IAggregateResultProvider results;

		// Token: 0x040020CF RID: 8399
		private int aggregate;

		// Token: 0x040020D0 RID: 8400
		private bool unasigendValue;

		// Token: 0x040020D1 RID: 8401
		private AggregateValue value;
	}
}
