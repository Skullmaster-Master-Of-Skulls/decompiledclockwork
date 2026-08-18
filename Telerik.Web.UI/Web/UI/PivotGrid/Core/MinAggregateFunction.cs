using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C4D RID: 3149
	[DataContract]
	public sealed class MinAggregateFunction : NumericFormatAggregateFunction
	{
		// Token: 0x170026B8 RID: 9912
		// (get) Token: 0x0600770B RID: 30475 RVA: 0x001BA040 File Offset: 0x001B8240
		public override string DisplayName
		{
			get
			{
				return PivotLocalizationManager.Min;
			}
		}

		// Token: 0x0600770C RID: 30476 RVA: 0x001BA048 File Offset: 0x001B8248
		protected internal override AggregateValue CreateAggregate(IAggregateContext context)
		{
			if (context.HasCalculatedGroups)
			{
				return new MinAggregate();
			}
			switch (PrecisionHelpers.GetPrecision(context.DataType))
			{
			case Precision.Int64:
				return new MinIntAggregate();
			case Precision.Decimal:
				return new MinDecimalAggregate();
			case Precision.Double:
				return new MinAggregate();
			default:
				return AggregateValue.ErrorAggregateValue;
			}
		}

		// Token: 0x0600770D RID: 30477 RVA: 0x001BA09E File Offset: 0x001B829E
		public override int GetHashCode()
		{
			return 4;
		}

		// Token: 0x0600770E RID: 30478 RVA: 0x001BA0A1 File Offset: 0x001B82A1
		public override bool Equals(object obj)
		{
			return obj is MinAggregateFunction;
		}

		// Token: 0x0600770F RID: 30479 RVA: 0x001BA0AC File Offset: 0x001B82AC
		public override string ToString()
		{
			return "Min";
		}

		// Token: 0x06007710 RID: 30480 RVA: 0x001BA0B3 File Offset: 0x001B82B3
		protected override Cloneable CreateInstanceCore()
		{
			return new MinAggregateFunction();
		}

		// Token: 0x06007711 RID: 30481 RVA: 0x001BA0BA File Offset: 0x001B82BA
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
