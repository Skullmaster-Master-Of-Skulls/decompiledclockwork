using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C4B RID: 3147
	[DataContract]
	public abstract class NumericFormatAggregateFunction : AggregateFunction
	{
		// Token: 0x06007701 RID: 30465 RVA: 0x001B9F6C File Offset: 0x001B816C
		public override string GetStringFormat(Type dataType, string format)
		{
			if (format == null)
			{
				switch (PrecisionHelpers.GetPrecision(dataType))
				{
				case Precision.Int64:
					return "G";
				case Precision.Decimal:
					return "0.00";
				case Precision.Double:
					return "0.00";
				}
				return format;
			}
			return format;
		}
	}
}
