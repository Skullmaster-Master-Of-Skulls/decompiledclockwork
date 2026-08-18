using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C4C RID: 3148
	[DataContract]
	public sealed class MaxAggregateFunction : NumericFormatAggregateFunction
	{
		// Token: 0x170026B7 RID: 9911
		// (get) Token: 0x06007703 RID: 30467 RVA: 0x001B9FBA File Offset: 0x001B81BA
		public override string DisplayName
		{
			get
			{
				return PivotLocalizationManager.Max;
			}
		}

		// Token: 0x06007704 RID: 30468 RVA: 0x001B9FC4 File Offset: 0x001B81C4
		protected internal override AggregateValue CreateAggregate(IAggregateContext context)
		{
			if (context.HasCalculatedGroups)
			{
				return new MaxAggregate();
			}
			switch (PrecisionHelpers.GetPrecision(context.DataType))
			{
			case Precision.Int64:
				return new MaxIntAggregate();
			case Precision.Decimal:
				return new MaxDecimalAggregate();
			case Precision.Double:
				return new MaxAggregate();
			default:
				return AggregateValue.ErrorAggregateValue;
			}
		}

		// Token: 0x06007705 RID: 30469 RVA: 0x001BA01A File Offset: 0x001B821A
		public override int GetHashCode()
		{
			return 3;
		}

		// Token: 0x06007706 RID: 30470 RVA: 0x001BA01D File Offset: 0x001B821D
		public override bool Equals(object obj)
		{
			return obj is MaxAggregateFunction;
		}

		// Token: 0x06007707 RID: 30471 RVA: 0x001BA028 File Offset: 0x001B8228
		public override string ToString()
		{
			return "Max";
		}

		// Token: 0x06007708 RID: 30472 RVA: 0x001BA02F File Offset: 0x001B822F
		protected override Cloneable CreateInstanceCore()
		{
			return new MaxAggregateFunction();
		}

		// Token: 0x06007709 RID: 30473 RVA: 0x001BA036 File Offset: 0x001B8236
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
