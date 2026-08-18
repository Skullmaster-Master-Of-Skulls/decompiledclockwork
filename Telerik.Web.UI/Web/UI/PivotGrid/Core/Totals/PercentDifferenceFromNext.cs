using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C68 RID: 3176
	[DataContract]
	public sealed class PercentDifferenceFromNext : PercentDifferenceFromBase
	{
		// Token: 0x060077A8 RID: 30632 RVA: 0x001BB345 File Offset: 0x001B9545
		internal override ComparedToItteration GetItteration()
		{
			return ComparedToItteration.Backward;
		}

		// Token: 0x060077A9 RID: 30633 RVA: 0x001BB348 File Offset: 0x001B9548
		protected override Cloneable CreateInstanceCore()
		{
			return new PercentDifferenceFromNext();
		}
	}
}
