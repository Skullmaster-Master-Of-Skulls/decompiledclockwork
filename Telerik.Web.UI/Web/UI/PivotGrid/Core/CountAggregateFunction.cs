using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C4A RID: 3146
	[DataContract]
	public sealed class CountAggregateFunction : AggregateFunction
	{
		// Token: 0x170026B6 RID: 9910
		// (get) Token: 0x060076F8 RID: 30456 RVA: 0x001B9F22 File Offset: 0x001B8122
		public override string DisplayName
		{
			get
			{
				return PivotLocalizationManager.Count;
			}
		}

		// Token: 0x060076F9 RID: 30457 RVA: 0x001B9F29 File Offset: 0x001B8129
		protected internal override AggregateValue CreateAggregate(IAggregateContext context)
		{
			if (context.HasCalculatedGroups)
			{
				return new CountDoubleAggregate();
			}
			return new CountAggregate();
		}

		// Token: 0x060076FA RID: 30458 RVA: 0x001B9F3E File Offset: 0x001B813E
		public override string GetStringFormat(Type dataType, string format)
		{
			return "G";
		}

		// Token: 0x060076FB RID: 30459 RVA: 0x001B9F45 File Offset: 0x001B8145
		public override int GetHashCode()
		{
			return 1;
		}

		// Token: 0x060076FC RID: 30460 RVA: 0x001B9F48 File Offset: 0x001B8148
		public override bool Equals(object obj)
		{
			return obj is CountAggregateFunction;
		}

		// Token: 0x060076FD RID: 30461 RVA: 0x001B9F53 File Offset: 0x001B8153
		public override string ToString()
		{
			return "Count";
		}

		// Token: 0x060076FE RID: 30462 RVA: 0x001B9F5A File Offset: 0x001B815A
		protected override Cloneable CreateInstanceCore()
		{
			return new CountAggregateFunction();
		}

		// Token: 0x060076FF RID: 30463 RVA: 0x001B9F61 File Offset: 0x001B8161
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
