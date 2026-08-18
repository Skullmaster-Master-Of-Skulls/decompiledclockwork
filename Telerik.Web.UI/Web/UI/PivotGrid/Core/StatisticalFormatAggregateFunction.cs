using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C53 RID: 3155
	[DataContract]
	public abstract class StatisticalFormatAggregateFunction : AggregateFunction
	{
		// Token: 0x06007739 RID: 30521 RVA: 0x001BA9BD File Offset: 0x001B8BBD
		internal StatisticalFormatAggregateFunction()
		{
		}

		// Token: 0x0600773A RID: 30522 RVA: 0x001BA9C8 File Offset: 0x001B8BC8
		public override string GetStringFormat(Type dataType, string format)
		{
			if (PrecisionHelpers.GetPrecision(dataType) == Precision.Unknown)
			{
				return format;
			}
			return "0.00";
		}
	}
}
