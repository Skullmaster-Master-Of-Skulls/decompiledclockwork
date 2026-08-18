using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C50 RID: 3152
	[DataContract]
	public sealed class ProductAggregateFunction : AggregateFunction
	{
		// Token: 0x170026B9 RID: 9913
		// (get) Token: 0x06007715 RID: 30485 RVA: 0x001BA2DE File Offset: 0x001B84DE
		public override string DisplayName
		{
			get
			{
				return PivotLocalizationManager.Product;
			}
		}

		// Token: 0x06007716 RID: 30486 RVA: 0x001BA2E8 File Offset: 0x001B84E8
		protected internal override AggregateValue CreateAggregate(IAggregateContext context)
		{
			if (context.HasCalculatedGroups)
			{
				return new ProductAggregate();
			}
			if (PrecisionHelpers.GetPrecision(context.DataType) == Precision.Unknown)
			{
				return AggregateValue.ErrorAggregateValue;
			}
			return new ProductAggregate();
		}

		// Token: 0x06007717 RID: 30487 RVA: 0x001BA320 File Offset: 0x001B8520
		public override string GetStringFormat(Type dataType, string format)
		{
			if (PrecisionHelpers.GetPrecision(dataType) == Precision.Unknown)
			{
				return format;
			}
			return "0.00 E+00";
		}

		// Token: 0x06007718 RID: 30488 RVA: 0x001BA33E File Offset: 0x001B853E
		public override int GetHashCode()
		{
			return 5;
		}

		// Token: 0x06007719 RID: 30489 RVA: 0x001BA341 File Offset: 0x001B8541
		public override bool Equals(object obj)
		{
			return obj is ProductAggregateFunction;
		}

		// Token: 0x0600771A RID: 30490 RVA: 0x001BA34C File Offset: 0x001B854C
		public override string ToString()
		{
			return "Product";
		}

		// Token: 0x0600771B RID: 30491 RVA: 0x001BA353 File Offset: 0x001B8553
		protected override Cloneable CreateInstanceCore()
		{
			return new ProductAggregateFunction();
		}

		// Token: 0x0600771C RID: 30492 RVA: 0x001BA35A File Offset: 0x001B855A
		protected override void CloneCore(Cloneable source)
		{
		}
	}
}
