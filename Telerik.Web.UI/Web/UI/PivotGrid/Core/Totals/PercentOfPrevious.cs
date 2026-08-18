using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C70 RID: 3184
	[DataContract]
	public sealed class PercentOfPrevious : PercentOfBase
	{
		// Token: 0x060077CE RID: 30670 RVA: 0x001BB5FB File Offset: 0x001B97FB
		protected override Cloneable CreateInstanceCore()
		{
			return new PercentOfPrevious();
		}
	}
}
