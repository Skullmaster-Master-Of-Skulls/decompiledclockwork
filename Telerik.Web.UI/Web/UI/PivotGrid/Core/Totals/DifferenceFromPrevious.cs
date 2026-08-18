using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C61 RID: 3169
	[DataContract]
	public sealed class DifferenceFromPrevious : DifferenceFromBase
	{
		// Token: 0x0600778B RID: 30603 RVA: 0x001BAF73 File Offset: 0x001B9173
		protected override Cloneable CreateInstanceCore()
		{
			return new DifferenceFromPrevious();
		}
	}
}
