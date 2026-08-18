using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C69 RID: 3177
	[DataContract]
	public sealed class PercentDifferenceFromPrevious : PercentDifferenceFromBase
	{
		// Token: 0x060077AB RID: 30635 RVA: 0x001BB357 File Offset: 0x001B9557
		protected override Cloneable CreateInstanceCore()
		{
			return new PercentDifferenceFromPrevious();
		}
	}
}
