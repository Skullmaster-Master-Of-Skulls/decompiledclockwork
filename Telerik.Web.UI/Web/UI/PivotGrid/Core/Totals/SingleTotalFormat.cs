using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C64 RID: 3172
	[DataContract]
	public abstract class SingleTotalFormat : TotalFormat
	{
		// Token: 0x06007793 RID: 30611 RVA: 0x001BB051 File Offset: 0x001B9251
		internal SingleTotalFormat()
		{
		}

		// Token: 0x06007794 RID: 30612
		internal abstract AggregateValue FormatValue(Coordinate groups, IAggregateResultProvider results, int aggregateIndex);
	}
}
