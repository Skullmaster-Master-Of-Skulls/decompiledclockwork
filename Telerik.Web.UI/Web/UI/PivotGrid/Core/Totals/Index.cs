using System;
using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C65 RID: 3173
	[DataContract]
	public sealed class Index : SingleTotalFormat
	{
		// Token: 0x06007795 RID: 30613 RVA: 0x001BB059 File Offset: 0x001B9259
		public override string GetStringFormat(Type dataType, string stringFormat)
		{
			return "0.0000";
		}

		// Token: 0x06007796 RID: 30614 RVA: 0x001BB060 File Offset: 0x001B9260
		internal override AggregateValue FormatValue(Coordinate groups, IAggregateResultProvider results, int aggregate)
		{
			Coordinate root = results.Root;
			Coordinate groups2 = groups;
			Coordinate groups3 = new Coordinate(root.RowGroup, groups2.ColumnGroup);
			Coordinate groups4 = new Coordinate(groups2.RowGroup, root.ColumnGroup);
			AggregateValue aggregateResult = results.GetAggregateResult(aggregate, groups2);
			AggregateValue aggregateResult2 = results.GetAggregateResult(aggregate, root);
			AggregateValue aggregateResult3 = results.GetAggregateResult(aggregate, groups3);
			AggregateValue aggregateResult4 = results.GetAggregateResult(aggregate, groups4);
			double num = (aggregateResult == null) ? 0.0 : Convert.ToDouble(aggregateResult.GetValue(), CultureInfo.InvariantCulture);
			double num2 = (aggregateResult2 == null) ? 0.0 : Convert.ToDouble(aggregateResult2.GetValue(), CultureInfo.InvariantCulture);
			double num3 = (aggregateResult3 == null) ? 0.0 : Convert.ToDouble(aggregateResult3.GetValue(), CultureInfo.InvariantCulture);
			double num4 = (aggregateResult4 == null) ? 0.0 : Convert.ToDouble(aggregateResult4.GetValue(), CultureInfo.InvariantCulture);
			return new ConstantValueAggregate(num * num2 / (num3 * num4));
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x001BB165 File Offset: 0x001B9365
		protected override Cloneable CreateInstanceCore()
		{
			return new Index();
		}

		// Token: 0x06007798 RID: 30616 RVA: 0x001BB16C File Offset: 0x001B936C
		protected override void CloneCore(Cloneable source)
		{
		}

		// Token: 0x06007799 RID: 30617 RVA: 0x001BB16E File Offset: 0x001B936E
		public override bool Equals(object obj)
		{
			return obj is Index;
		}

		// Token: 0x0600779A RID: 30618 RVA: 0x001BB179 File Offset: 0x001B9379
		public override int GetHashCode()
		{
			return 647;
		}

		// Token: 0x0600779B RID: 30619 RVA: 0x001BB180 File Offset: 0x001B9380
		public override string ToString()
		{
			return "Index";
		}
	}
}
