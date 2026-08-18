using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C60 RID: 3168
	[DataContract]
	public sealed class DifferenceFromNext : DifferenceFromBase
	{
		// Token: 0x06007788 RID: 30600 RVA: 0x001BAF61 File Offset: 0x001B9161
		internal override ComparedToItteration GetItteration()
		{
			return ComparedToItteration.Backward;
		}

		// Token: 0x06007789 RID: 30601 RVA: 0x001BAF64 File Offset: 0x001B9164
		protected override Cloneable CreateInstanceCore()
		{
			return new DifferenceFromNext();
		}
	}
}
