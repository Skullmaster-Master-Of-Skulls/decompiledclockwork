using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C6D RID: 3181
	[DataContract]
	public sealed class PercentOfColumnTotal : PercentOfAncestor
	{
		// Token: 0x170026CC RID: 9932
		// (get) Token: 0x060077BE RID: 30654 RVA: 0x001BB522 File Offset: 0x001B9722
		// (set) Token: 0x060077BF RID: 30655 RVA: 0x001BB52A File Offset: 0x001B972A
		internal int Level { get; set; }

		// Token: 0x060077C0 RID: 30656 RVA: 0x001BB534 File Offset: 0x001B9734
		internal override Coordinate OfGroup(Coordinate valueGroups, Coordinate rootGroups)
		{
			if (this.Level == 1 && valueGroups.RowGroup.Parent != null)
			{
				return new Coordinate(valueGroups.RowGroup.Parent, valueGroups.ColumnGroup);
			}
			return new Coordinate(rootGroups.RowGroup, valueGroups.ColumnGroup);
		}

		// Token: 0x060077C1 RID: 30657 RVA: 0x001BB584 File Offset: 0x001B9784
		protected override Cloneable CreateInstanceCore()
		{
			return new PercentOfColumnTotal
			{
				Level = this.Level
			};
		}

		// Token: 0x060077C2 RID: 30658 RVA: 0x001BB5A4 File Offset: 0x001B97A4
		public override bool Equals(object obj)
		{
			return obj is PercentOfColumnTotal;
		}

		// Token: 0x060077C3 RID: 30659 RVA: 0x001BB5AF File Offset: 0x001B97AF
		public override int GetHashCode()
		{
			return 647;
		}
	}
}
