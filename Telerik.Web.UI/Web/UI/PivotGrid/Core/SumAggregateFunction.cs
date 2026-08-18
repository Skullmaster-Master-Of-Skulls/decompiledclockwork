using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C56 RID: 3158
	[DataContract]
	public sealed class SumAggregateFunction : NumericFormatAggregateFunction
	{
		// Token: 0x170026C4 RID: 9924
		// (get) Token: 0x0600774B RID: 30539 RVA: 0x001BAA6C File Offset: 0x001B8C6C
		public override string DisplayName
		{
			get
			{
				return PivotLocalizationManager.Sum;
			}
		}

		// Token: 0x0600774C RID: 30540 RVA: 0x001BAA74 File Offset: 0x001B8C74
		protected internal override AggregateValue CreateAggregate(IAggregateContext context)
		{
			if (context.HasCalculatedGroups)
			{
				return new SumAggregate();
			}
			switch (PrecisionHelpers.GetPrecision(context.DataType))
			{
			case Precision.Int64:
				return new SumIntAggregate();
			case Precision.Decimal:
				return new SumDecimalAggregate();
			case Precision.Double:
				return new SumAggregate();
			default:
				return AggregateValue.ErrorAggregateValue;
			}
		}

		// Token: 0x0600774D RID: 30541 RVA: 0x001BAACA File Offset: 0x001B8CCA
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600774E RID: 30542 RVA: 0x001BAACD File Offset: 0x001B8CCD
		public override bool Equals(object obj)
		{
			return obj is SumAggregateFunction;
		}

		// Token: 0x0600774F RID: 30543 RVA: 0x001BAAD8 File Offset: 0x001B8CD8
		public override string ToString()
		{
			return "Sum";
		}

		// Token: 0x06007750 RID: 30544 RVA: 0x001BAADF File Offset: 0x001B8CDF
		protected override Cloneable CreateInstanceCore()
		{
			return new SumAggregateFunction();
		}

		// Token: 0x06007751 RID: 30545 RVA: 0x001BAAE6 File Offset: 0x001B8CE6
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
