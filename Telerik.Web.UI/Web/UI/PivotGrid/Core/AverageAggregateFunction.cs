using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C49 RID: 3145
	[DataContract]
	public sealed class AverageAggregateFunction : AggregateFunction
	{
		// Token: 0x170026B5 RID: 9909
		// (get) Token: 0x060076EF RID: 30447 RVA: 0x001B9E7A File Offset: 0x001B807A
		public override string DisplayName
		{
			get
			{
				return PivotLocalizationManager.Average;
			}
		}

		// Token: 0x060076F0 RID: 30448 RVA: 0x001B9E84 File Offset: 0x001B8084
		protected internal override AggregateValue CreateAggregate(Type dataType)
		{
			switch (PrecisionHelpers.GetPrecision(dataType))
			{
			case Precision.Int64:
			case Precision.Double:
				return new AverageAggregate();
			case Precision.Decimal:
				return new AverageDecimalAggregate();
			default:
				return AggregateValue.ErrorAggregateValue;
			}
		}

		// Token: 0x060076F1 RID: 30449 RVA: 0x001B9EC4 File Offset: 0x001B80C4
		public override string GetStringFormat(Type dataType, string format)
		{
			if (format != null)
			{
				return format;
			}
			switch (PrecisionHelpers.GetPrecision(dataType))
			{
			case Precision.Int64:
			case Precision.Decimal:
			case Precision.Double:
				return "0.00";
			default:
				return format;
			}
		}

		// Token: 0x060076F2 RID: 30450 RVA: 0x001B9EFC File Offset: 0x001B80FC
		public override int GetHashCode()
		{
			return 2;
		}

		// Token: 0x060076F3 RID: 30451 RVA: 0x001B9EFF File Offset: 0x001B80FF
		public override bool Equals(object obj)
		{
			return obj is AverageAggregateFunction;
		}

		// Token: 0x060076F4 RID: 30452 RVA: 0x001B9F0A File Offset: 0x001B810A
		public override string ToString()
		{
			return "Average";
		}

		// Token: 0x060076F5 RID: 30453 RVA: 0x001B9F11 File Offset: 0x001B8111
		protected override Cloneable CreateInstanceCore()
		{
			return new AverageAggregateFunction();
		}

		// Token: 0x060076F6 RID: 30454 RVA: 0x001B9F18 File Offset: 0x001B8118
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
