using System;
using System.Collections.Generic;
using System.Data.Mapping.ViewGeneration.Structures;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000280 RID: 640
	internal class BasicKeyConstraint : KeyConstraint<BasicCellRelation, MemberProjectedSlot>
	{
		// Token: 0x06002698 RID: 9880 RVA: 0x00094580 File Offset: 0x00092780
		internal BasicKeyConstraint(BasicCellRelation relation, IEnumerable<MemberProjectedSlot> keySlots) : base(relation, keySlots, ProjectedSlot.EqualityComparer)
		{
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x00094590 File Offset: 0x00092790
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
