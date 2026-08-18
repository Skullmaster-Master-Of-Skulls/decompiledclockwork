using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000491 RID: 1169
	internal class BasicKeyConstraint : KeyConstraint<BasicCellRelation, MemberProjectedSlot>
	{
		// Token: 0x06002B2D RID: 11053 RVA: 0x000D0C3A File Offset: 0x000CEE3A
		internal BasicKeyConstraint(BasicCellRelation relation, IEnumerable<MemberProjectedSlot> keySlots) : base(relation, keySlots, ProjectedSlot.EqualityComparer)
		{
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000D0C4C File Offset: 0x000CEE4C
		internal ViewKeyConstraint Propagate()
		{
			ViewCellRelation viewCellRelation = base.CellRelation.ViewCellRelation;
			List<ViewCellSlot> list = new List<ViewCellSlot>();
			foreach (MemberProjectedSlot slot in base.KeySlots)
			{
				ViewCellSlot viewCellSlot = viewCellRelation.LookupViewSlot(slot);
				if (viewCellSlot == null)
				{
					return null;
				}
				list.Add(viewCellSlot);
			}
			return new ViewKeyConstraint(viewCellRelation, list);
		}
	}
}
