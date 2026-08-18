using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C5F RID: 3167
	[DataContract]
	public sealed class DifferenceFrom : DifferenceFromBase
	{
		// Token: 0x170026C9 RID: 9929
		// (get) Token: 0x06007780 RID: 30592 RVA: 0x001BAE8F File Offset: 0x001B908F
		// (set) Token: 0x06007781 RID: 30593 RVA: 0x001BAE97 File Offset: 0x001B9097
		[DataMember]
		public object GroupName { get; set; }

		// Token: 0x06007782 RID: 30594 RVA: 0x001BAEA0 File Offset: 0x001B90A0
		internal override object BaseGroupName()
		{
			return this.GroupName;
		}

		// Token: 0x06007783 RID: 30595 RVA: 0x001BAEA8 File Offset: 0x001B90A8
		internal override AggregateValue Format(double? comparison, double? current, int index, int count, int baseIndex)
		{
			if (index == baseIndex)
			{
				return null;
			}
			if (comparison != null)
			{
				if (current != null)
				{
					return new ConstantValueAggregate(current.Value - comparison.Value);
				}
				return new ConstantValueAggregate(-comparison.Value);
			}
			else
			{
				if (current != null)
				{
					return new ConstantValueAggregate(current);
				}
				return new ConstantValueAggregate(0.0);
			}
		}

		// Token: 0x06007784 RID: 30596 RVA: 0x001BAF23 File Offset: 0x001B9123
		internal override double? Accumulate(double? aggregate, double? current, int index, int count, int baseIndex)
		{
			return aggregate;
		}

		// Token: 0x06007785 RID: 30597 RVA: 0x001BAF28 File Offset: 0x001B9128
		protected override void CloneCore(Cloneable source)
		{
			DifferenceFrom differenceFrom = source as DifferenceFrom;
			if (differenceFrom != null)
			{
				this.GroupName = differenceFrom.GroupName;
			}
			base.CloneCore(source);
		}

		// Token: 0x06007786 RID: 30598 RVA: 0x001BAF52 File Offset: 0x001B9152
		protected override Cloneable CreateInstanceCore()
		{
			return new DifferenceFrom();
		}
	}
}
