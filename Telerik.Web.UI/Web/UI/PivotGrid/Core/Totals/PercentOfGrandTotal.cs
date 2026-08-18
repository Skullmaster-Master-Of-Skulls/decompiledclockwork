using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C6E RID: 3182
	[DataContract]
	public sealed class PercentOfGrandTotal : PercentOfAncestor
	{
		// Token: 0x060077C5 RID: 30661 RVA: 0x001BB5BE File Offset: 0x001B97BE
		internal override Coordinate OfGroup(Coordinate valueGroups, Coordinate rootGroups)
		{
			return rootGroups;
		}

		// Token: 0x060077C6 RID: 30662 RVA: 0x001BB5C1 File Offset: 0x001B97C1
		protected override Cloneable CreateInstanceCore()
		{
			return new PercentOfGrandTotal();
		}

		// Token: 0x060077C7 RID: 30663 RVA: 0x001BB5C8 File Offset: 0x001B97C8
		public override bool Equals(object obj)
		{
			return obj is PercentOfGrandTotal;
		}

		// Token: 0x060077C8 RID: 30664 RVA: 0x001BB5D3 File Offset: 0x001B97D3
		public override int GetHashCode()
		{
			return 645;
		}

		// Token: 0x060077C9 RID: 30665 RVA: 0x001BB5DA File Offset: 0x001B97DA
		public override string ToString()
		{
			return "% of Grand Total";
		}
	}
}
