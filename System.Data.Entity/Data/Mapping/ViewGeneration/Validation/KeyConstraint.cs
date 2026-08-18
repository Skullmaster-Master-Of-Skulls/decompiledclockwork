using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000284 RID: 644
	internal class KeyConstraint<TCellRelation, TSlot> : InternalBase where TCellRelation : CellRelation
	{
		// Token: 0x060026B6 RID: 9910 RVA: 0x000959CC File Offset: 0x00093BCC
		internal KeyConstraint(TCellRelation relation, IEnumerable<TSlot> keySlots, IEqualityComparer<TSlot> comparer)
		{
			this.m_relation = relation;
			this.m_keySlots = new Set<TSlot>(keySlots, comparer).MakeReadOnly();
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x000959ED File Offset: 0x00093BED
		protected TCellRelation CellRelation
		{
			get
			{
				return this.m_relation;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x000959F5 File Offset: 0x00093BF5
		protected Set<TSlot> KeySlots
		{
			get
			{
				return this.m_keySlots;
			}
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000959FD File Offset: 0x00093BFD
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "Key (V{0}) - ", new object[]
			{
				this.m_relation.CellNumber
			});
			StringUtil.ToSeparatedStringSorted(builder, this.KeySlots, ", ");
		}

		// Token: 0x040011E0 RID: 4576
		private TCellRelation m_relation;

		// Token: 0x040011E1 RID: 4577
		private Set<TSlot> m_keySlots;
	}
}
