using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C54 RID: 3156
	[DataContract]
	public sealed class StdDevAggregateFunction : StatisticalFormatAggregateFunction
	{
		// Token: 0x170026C2 RID: 9922
		// (get) Token: 0x0600773B RID: 30523 RVA: 0x001BA9E6 File Offset: 0x001B8BE6
		public override string DisplayName
		{
			get
			{
				return PivotLocalizationManager.StdDev;
			}
		}

		// Token: 0x0600773C RID: 30524 RVA: 0x001BA9F0 File Offset: 0x001B8BF0
		protected internal override AggregateValue CreateAggregate(Type dataType)
		{
			if (PrecisionHelpers.GetPrecision(dataType) == Precision.Unknown)
			{
				return AggregateValue.ErrorAggregateValue;
			}
			return new StdDevAggregate();
		}

		// Token: 0x0600773D RID: 30525 RVA: 0x001BAA12 File Offset: 0x001B8C12
		public override int GetHashCode()
		{
			return 6;
		}

		// Token: 0x0600773E RID: 30526 RVA: 0x001BAA15 File Offset: 0x001B8C15
		public override bool Equals(object obj)
		{
			return obj is StdDevAggregateFunction;
		}

		// Token: 0x0600773F RID: 30527 RVA: 0x001BAA20 File Offset: 0x001B8C20
		public override string ToString()
		{
			return "StdDev";
		}

		// Token: 0x06007740 RID: 30528 RVA: 0x001BAA27 File Offset: 0x001B8C27
		protected override Cloneable CreateInstanceCore()
		{
			return new StdDevAggregateFunction();
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x001BAA2E File Offset: 0x001B8C2E
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
