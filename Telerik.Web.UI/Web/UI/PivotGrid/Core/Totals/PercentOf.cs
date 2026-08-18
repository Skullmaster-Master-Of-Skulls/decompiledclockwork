using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C6B RID: 3179
	[DataContract]
	public sealed class PercentOf : PercentOfBase
	{
		// Token: 0x170026CB RID: 9931
		// (get) Token: 0x060077B1 RID: 30641 RVA: 0x001BB3E9 File Offset: 0x001B95E9
		// (set) Token: 0x060077B2 RID: 30642 RVA: 0x001BB3F1 File Offset: 0x001B95F1
		[DataMember]
		public object GroupName { get; set; }

		// Token: 0x060077B3 RID: 30643 RVA: 0x001BB3FA File Offset: 0x001B95FA
		internal override object BaseGroupName()
		{
			return this.GroupName;
		}

		// Token: 0x060077B4 RID: 30644 RVA: 0x001BB402 File Offset: 0x001B9602
		internal override double? Accumulate(double? aggregate, double? current, int index, int count, int baseIndex)
		{
			return aggregate;
		}

		// Token: 0x060077B5 RID: 30645 RVA: 0x001BB408 File Offset: 0x001B9608
		internal override AggregateValue Format(double? comparison, double? current, int index, int count, int baseIndex)
		{
			if (comparison == null)
			{
				return null;
			}
			if (current != null)
			{
				return new ConstantValueAggregate(current.Value / comparison.Value);
			}
			return new ConstantValueAggregate(0.0 / comparison.Value);
		}

		// Token: 0x060077B6 RID: 30646 RVA: 0x001BB460 File Offset: 0x001B9660
		protected override void CloneCore(Cloneable source)
		{
			PercentOf percentOf = source as PercentOf;
			if (percentOf != null)
			{
				this.GroupName = percentOf.GroupName;
			}
			base.CloneCore(source);
		}

		// Token: 0x060077B7 RID: 30647 RVA: 0x001BB48A File Offset: 0x001B968A
		protected override Cloneable CreateInstanceCore()
		{
			return new PercentOf();
		}
	}
}
