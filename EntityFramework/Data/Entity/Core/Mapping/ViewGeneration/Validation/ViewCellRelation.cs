using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200049A RID: 1178
	internal class ViewCellRelation : CellRelation
	{
		// Token: 0x06002B79 RID: 11129 RVA: 0x000D35EA File Offset: 0x000D17EA
		internal ViewCellRelation(Cell cell, List<ViewCellSlot> slots, int cellNumber) : base(cellNumber)
		{
			this.m_cell = cell;
			this.m_slots = slots;
			this.m_cell.CQuery.CreateBasicCellRelation(this);
			this.m_cell.SQuery.CreateBasicCellRelation(this);
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000D3623 File Offset: 0x000D1823
		internal Cell Cell
		{
			get
			{
				return this.m_cell;
			}
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000D362C File Offset: 0x000D182C
		internal ViewCellSlot LookupViewSlot(MemberProjectedSlot slot)
		{
			foreach (ViewCellSlot viewCellSlot in this.m_slots)
			{
				if (ProjectedSlot.EqualityComparer.Equals(slot, viewCellSlot.CSlot) || ProjectedSlot.EqualityComparer.Equals(slot, viewCellSlot.SSlot))
				{
					return viewCellSlot;
				}
			}
			return null;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000D36A8 File Offset: 0x000D18A8
		protected override int GetHash()
		{
			return this.m_cell.GetHashCode();
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000D36B5 File Offset: 0x000D18B5
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("ViewRel[");
			this.m_cell.ToCompactString(builder);
			builder.Append(']');
		}

		// Token: 0x0400100C RID: 4108
		private readonly Cell m_cell;

		// Token: 0x0400100D RID: 4109
		private readonly List<ViewCellSlot> m_slots;
	}
}
