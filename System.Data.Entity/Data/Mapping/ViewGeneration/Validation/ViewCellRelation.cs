using System;
using System.Collections.Generic;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000286 RID: 646
	internal class ViewCellRelation : CellRelation
	{
		// Token: 0x060026BF RID: 9919 RVA: 0x00095ADA File Offset: 0x00093CDA
		internal ViewCellRelation(Cell cell, List<ViewCellSlot> slots, int cellNumber) : base(cellNumber)
		{
			this.m_cell = cell;
			this.m_slots = slots;
			this.m_cell.CQuery.CreateBasicCellRelation(this);
			this.m_cell.SQuery.CreateBasicCellRelation(this);
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x00095B13 File Offset: 0x00093D13
		internal Cell Cell
		{
			get
			{
				return this.m_cell;
			}
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x00095B1C File Offset: 0x00093D1C
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

		// Token: 0x060026C2 RID: 9922 RVA: 0x00095B98 File Offset: 0x00093D98
		protected override int GetHash()
		{
			return this.m_cell.GetHashCode();
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x00095BA5 File Offset: 0x00093DA5
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("ViewRel[");
			this.m_cell.ToCompactString(builder);
			builder.Append(']');
		}

		// Token: 0x040011E3 RID: 4579
		private Cell m_cell;

		// Token: 0x040011E4 RID: 4580
		private List<ViewCellSlot> m_slots;
	}
}
