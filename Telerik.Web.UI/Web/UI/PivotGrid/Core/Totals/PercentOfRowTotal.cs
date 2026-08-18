using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C71 RID: 3185
	[DataContract]
	public sealed class PercentOfRowTotal : PercentOfAncestor
	{
		// Token: 0x170026CD RID: 9933
		// (get) Token: 0x060077D0 RID: 30672 RVA: 0x001BB60A File Offset: 0x001B980A
		// (set) Token: 0x060077D1 RID: 30673 RVA: 0x001BB612 File Offset: 0x001B9812
		internal int Level { get; set; }

		// Token: 0x060077D2 RID: 30674 RVA: 0x001BB61C File Offset: 0x001B981C
		internal override Coordinate OfGroup(Coordinate valueGroups, Coordinate rootGroups)
		{
			if (this.Level == 1 && valueGroups.ColumnGroup.Parent != null)
			{
				return new Coordinate(valueGroups.RowGroup, valueGroups.ColumnGroup.Parent);
			}
			return new Coordinate(valueGroups.RowGroup, rootGroups.ColumnGroup);
		}

		// Token: 0x060077D3 RID: 30675 RVA: 0x001BB66C File Offset: 0x001B986C
		protected override Cloneable CreateInstanceCore()
		{
			return new PercentOfRowTotal
			{
				Level = this.Level
			};
		}

		// Token: 0x060077D4 RID: 30676 RVA: 0x001BB68C File Offset: 0x001B988C
		public override bool Equals(object obj)
		{
			return obj is PercentOfRowTotal;
		}

		// Token: 0x060077D5 RID: 30677 RVA: 0x001BB697 File Offset: 0x001B9897
		public override int GetHashCode()
		{
			return 646;
		}
	}
}
