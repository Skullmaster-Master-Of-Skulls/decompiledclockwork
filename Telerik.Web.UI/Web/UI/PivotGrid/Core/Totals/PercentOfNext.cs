using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C6F RID: 3183
	[DataContract]
	public sealed class PercentOfNext : PercentOfBase
	{
		// Token: 0x060077CB RID: 30667 RVA: 0x001BB5E9 File Offset: 0x001B97E9
		internal override ComparedToItteration GetItteration()
		{
			return ComparedToItteration.Backward;
		}

		// Token: 0x060077CC RID: 30668 RVA: 0x001BB5EC File Offset: 0x001B97EC
		protected override Cloneable CreateInstanceCore()
		{
			return new PercentOfNext();
		}
	}
}
