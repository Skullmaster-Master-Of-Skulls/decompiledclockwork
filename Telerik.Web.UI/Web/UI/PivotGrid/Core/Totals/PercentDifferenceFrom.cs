using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C67 RID: 3175
	[DataContract]
	public sealed class PercentDifferenceFrom : PercentDifferenceFromBase
	{
		// Token: 0x170026CA RID: 9930
		// (get) Token: 0x060077A0 RID: 30624 RVA: 0x001BB23E File Offset: 0x001B943E
		// (set) Token: 0x060077A1 RID: 30625 RVA: 0x001BB246 File Offset: 0x001B9446
		[DataMember]
		public object GroupName { get; set; }

		// Token: 0x060077A2 RID: 30626 RVA: 0x001BB24F File Offset: 0x001B944F
		internal override object BaseGroupName()
		{
			return this.GroupName;
		}

		// Token: 0x060077A3 RID: 30627 RVA: 0x001BB257 File Offset: 0x001B9457
		internal override double? Accumulate(double? aggregate, double? current, int index, int count, int baseIndex)
		{
			return aggregate;
		}

		// Token: 0x060077A4 RID: 30628 RVA: 0x001BB25C File Offset: 0x001B945C
		internal override AggregateValue Format(double? comparison, double? current, int index, int count, int baseIndex)
		{
			if (index == baseIndex)
			{
				return null;
			}
			if (current != null)
			{
				if (comparison != null && comparison.Value != 0.0)
				{
					return new ConstantValueAggregate((current.Value - comparison.Value) / comparison.Value);
				}
			}
			else if (comparison != null)
			{
				if (double.IsNaN(comparison.Value))
				{
					return new ConstantValueAggregate(double.NaN);
				}
				if (comparison.Value != 0.0)
				{
					return new ConstantValueAggregate(-1.0);
				}
			}
			return null;
		}

		// Token: 0x060077A5 RID: 30629 RVA: 0x001BB30C File Offset: 0x001B950C
		protected override void CloneCore(Cloneable source)
		{
			PercentDifferenceFrom percentDifferenceFrom = source as PercentDifferenceFrom;
			if (percentDifferenceFrom != null)
			{
				this.GroupName = percentDifferenceFrom.GroupName;
			}
			base.CloneCore(source);
		}

		// Token: 0x060077A6 RID: 30630 RVA: 0x001BB336 File Offset: 0x001B9536
		protected override Cloneable CreateInstanceCore()
		{
			return new PercentDifferenceFrom();
		}
	}
}
